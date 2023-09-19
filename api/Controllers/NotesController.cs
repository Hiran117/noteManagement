using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteById(string id)
        {
            var note = await _noteService.GetNoteById(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

[HttpPost("{User}")]
public async Task<IActionResult> Create([FromBody] Note note)
{
    if (!User.Identity.IsAuthenticated)
        return Unauthorized("Invalid token.");

    // Extracting user's ID from the "nameid" claim
    var userIdClaim = User.FindFirst("nameid");
    if (userIdClaim == null)
        return Unauthorized("User ID claim not found in token.");

    var userId = userIdClaim.Value;
    if (string.IsNullOrEmpty(userId))
        return Unauthorized("User ID not found in token.");

    note.UserId = userId;
    await _noteService.Create(note);
    return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
}


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Note note)
        {
            var existingNote = await _noteService.GetNoteById(id);
            if (existingNote == null)
                return NotFound();

            await _noteService.Update(note);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingNote = await _noteService.GetNoteById(id);
            if (existingNote == null)
                return NotFound();

            await _noteService.Delete(id);
            return NoContent();
        }
    }
}
