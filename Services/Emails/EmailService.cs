using Microsoft.EntityFrameworkCore;
using Radzen;
using Tescat.Models;
using Tescat.Services.Notification;

namespace Tescat.Services.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        private readonly NotificationService _notificationService;

        public EmailService(IDbContextFactory<TescatDbContext> contextFactory, NotificationService notificationService)
        {
            _contextFactory = contextFactory;
            _notificationService = notificationService;
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

        public async Task<bool> UpdateUserEmail(int IdUser,Email userEmail)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                if(userEmail.IdUser != null)
                {
                    context.Entry(userEmail).State = EntityState.Modified;
                }
                else
                {
                    userEmail.IdUser = IdUser;
                    context.Add(userEmail);
                }
                await context.SaveChangesAsync();
                _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se actualizo información del correo.");
                return true;
            }
            catch
            {
                _notificationService.Notify(NotificationSeverity.Error, "Error", "No se pudo actualizar información del correo.");
                return false;
            }

        }
    }
}
