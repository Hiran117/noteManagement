using api.Models;

namespace api.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByUsername(string username);
        Task<User> GetById(string id);
        Task<User> Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(string id);
    }
}
