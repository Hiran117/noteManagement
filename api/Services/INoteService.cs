using api.Models;

namespace api.Services
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetAllNotesByUser(string userId);
        Task<Note> GetNoteById(string id);
        Task Create(Note note);
        Task<bool> Update(Note note);
        Task<bool> Delete(string id);
    }
}
