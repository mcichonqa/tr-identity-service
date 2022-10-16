using IdentityService.Entity;
using IdentityService.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityContext _identityContext;

        public UserRepository(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task CreateUser(User user)
        {
            await _identityContext.Users.AddAsync(user);
            await _identityContext.SaveChangesAsync();
        }

        public async Task DeleteUser(string username)
        {
            var user = _identityContext.Users.FirstOrDefault(u => u.Username == username);

            if (user is null)
                throw new Exception($"User named {username} doesn't exist and cant be deleted.");

            _identityContext.Users.Remove(user);
            await _identityContext.SaveChangesAsync();

        }

        public async Task<User> GetUser(string username)
        {
            return await _identityContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _identityContext.Users.ToListAsync();
        }
    }
}