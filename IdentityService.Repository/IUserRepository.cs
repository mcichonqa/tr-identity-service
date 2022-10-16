using IdentityService.Entity.Models;

namespace IdentityService.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUser(string username);
        Task<List<User>> GetUsers();
        Task CreateUser(User user);
        Task DeleteUser(string username);
    }
}