using MongoDB.Driver;
using api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Services
{
    public class NoteService : INoteService
    {
        private readonly IMongoCollection<Note> _notes;

        public NoteService(NoteContext noteContext)
        {
            _notes = noteContext.Notes;
        }

        public async Task<IEnumerable<Note>> GetAllNotesByUser(string userId)
        {
            return await _notes.Find(note => note.UserId == userId).ToListAsync();
        }

        public async Task<Note> GetNoteById(string id)
        {
            return await _notes.Find(note => note.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Note note)
        {
            await _notes.InsertOneAsync(note);
        }

        public async Task<bool> Update(Note note)
        {
            var result = await _notes.ReplaceOneAsync(n => n.Id == note.Id, note);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _notes.DeleteOneAsync(note => note.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
