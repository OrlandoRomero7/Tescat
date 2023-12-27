using Tescat.Models;

namespace Tescat.Services.Users
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserId(int userID);
        public Task<User> InsertUser(User user);
        //public Task<bool> UpdateUser(int IdOld,User updateUser, UserCredential userCredential, Email userEmail);
        public Task<User> UpdateUser(User user);
        public Task<User> DeleteUser(int userID);

        public Task<List<User>> GetUsersWithoutPC();

        public Task<bool> ExistingUser(int userID);
    }
}
