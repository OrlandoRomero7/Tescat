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

        public Task<User> InsertUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(int userID, User updateUser)
        {
            throw new NotImplementedException();
        }
    }
}
