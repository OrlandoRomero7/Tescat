using Tescat.Models;

namespace Tescat.Services.Users
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserId(int userID);
        public Task<User> InsertUser(User user);
        public Task<User> UpdateUser(int userID, User updateUser);
        public Task<User> DeleteUser(int userID);
    }
}
