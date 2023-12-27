using Tescat.Models;

namespace Tescat.Services.Emails
{
    public interface IEmailService
    {
        public Task<Email> GetEmails(int userId);

        public Task<Email> InsertUserEmail(Email userEmail);
        public Task<Email> QuitEmailFromUserDetails(int IdUser);

        public Task<bool> UpdateUserEmail(int IdUser,Email userEmail);
    }
}
