using Tescat.Models;

namespace Tescat.Services.UserCredentials
{
    public interface IUserCredentialService
    {
        public Task<UserCredential> GetUserCredentials(int userId);
    }
}
