using MongoDB.Driver;
using api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(NoteContext noteContext)
        {
            _users = noteContext.Users;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _users.Find(x => x.Username == username).FirstOrDefaultAsync();

            if (user == null)
                return null;

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if(!isValidPassword)
                return null;

            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<User> GetById(string id)
        {
            return await _users.Find<User>(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> Create(User user, string password)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _users.Find<User>(user => user.Username == username).FirstOrDefaultAsync();
        }

        public void Update(User userParam, string password = null)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
