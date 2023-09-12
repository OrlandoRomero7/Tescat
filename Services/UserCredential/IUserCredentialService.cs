using Tescat.Models;

namespace Tescat.Services.UserCredentials
{
    public interface IUserCredentialService
    {
        public Task<UserCredential> GetUserCredentials(int userId);

        public Task<UserCredential> InsertUserCredentials(UserCredential userCredential);

        public Task<UserCredential> DeleteUserCredentials(int userId);

        public Task<bool> UpdateUserCredentials(UserCredential userCredential);
    }
}
