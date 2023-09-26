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
        
    }
}
