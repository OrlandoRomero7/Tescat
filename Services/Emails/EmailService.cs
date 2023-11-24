using Microsoft.EntityFrameworkCore;
using Tescat.Models;

namespace Tescat.Services.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;

        public EmailService(IDbContextFactory<TescatDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Email> GetEmails(int userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var userEmailsDb = await context.Emails.FirstOrDefaultAsync(p => p.IdUser == userId);
            if (userEmailsDb != null)
            {
                return userEmailsDb;

            }
            else
            {
                return new Email();
            }
        }
        public async Task<Email> InsertUserEmail(Email userEmail)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Emails.Add(userEmail);
            await context.SaveChangesAsync();
            return userEmail;
        }
        public async Task<Email> QuitEmailFromUserDetails(int IdUser)
        {
            using var context = _contextFactory.CreateDbContext();
            var emailDb = await context.Emails.FirstOrDefaultAsync(c => c.IdUser == IdUser);
            if (emailDb != null)
            {
                emailDb.IdUser = null;
                await context.SaveChangesAsync();
            }
            return emailDb;
        }

    }
}
