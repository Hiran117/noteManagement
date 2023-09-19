using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Services;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;  
using System; // Make sure you have this for the Exception handling

namespace api.Controllers
{
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public UsersController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration; 
    }      

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO loginDTO)
        {
            var user = await _userService.Authenticate(loginDTO.Username, loginDTO.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSecret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                Id = user.Id,
                Username = user.Username,
                Token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userService.GetByUsername(registerDTO.Username);
            if(existingUser != null)
            {
                return BadRequest(new { message = "Username already exists" });
            }

            try
            {
                var newUser = new User
                {
                    Username = registerDTO.Username,
                    Email = registerDTO.Email,
                    Name = registerDTO.Name,
                    Password = registerDTO.Password
                };

                var createdUser = await _userService.Create(newUser, registerDTO.Password);

                if (createdUser == null)
                {
                    return BadRequest(new { message = "Error occurred while creating user" });
                }

                createdUser.Password = null;
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
