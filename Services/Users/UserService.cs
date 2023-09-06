using Microsoft.EntityFrameworkCore;
using Tescat.Models;

namespace Tescat.Services.Users
{
    public class UserService : IUserService
    {
        private readonly TescatDbContext _context;
        public UserService(TescatDbContext context)
        {
            _context = context;
        }
        public Task<User> DeleteUser(int userID)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsers()
        {
            return _context.Users.ToListAsync();
        }

        public async Task<User> GetUserId(int userID)
        {
            var user = await _context.Users.FindAsync(userID);
            if (user == null)
            {
                return new User();
            }
            else
            {
                return user;
            }
        }

        public async Task<User> InsertUser(User user)
        {
            if(user != null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            else
            {
                return new User();
            }
        }

        public Task<User> UpdateUser(int userID, User updateUser)
        {
            throw new NotImplementedException();
        }
    }
}
