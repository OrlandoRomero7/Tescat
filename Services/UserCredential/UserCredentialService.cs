using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Tescat.Models;
using Tescat.Services.Notification;

namespace Tescat.Services.UserCredentials
{
    public class UserCredentialService : IUserCredentialService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        private readonly NotificationService _notificationService;

        public UserCredentialService(IDbContextFactory<TescatDbContext> contextFactory, NotificationService notificationService)
        {
            _contextFactory = contextFactory;
            _notificationService = notificationService;
        }
        public async Task<UserCredential> GetUserCredentials(int userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var userCredentialDb = await context.UserCredentials.FirstOrDefaultAsync(p => p.IdUser == userId);
            if (userCredentialDb != null)
            {
               return userCredentialDb;

            }
            else
            {
                return new UserCredential();
            }
            //return await _contextFactory.UserCredentialService.FirstOrDefaultAsync(p => p.IdUser == userId);

        }
        public async Task<UserCredential> InsertUserCredentials(UserCredential userCredential)
        {
            using var context = _contextFactory.CreateDbContext();
            context.UserCredentials.Add(userCredential);
            await context.SaveChangesAsync();
            return userCredential;
        }

        public async Task<UserCredential> DeleteUserCredentials(int userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var userCredentialDb = await context.UserCredentials.FirstOrDefaultAsync(p => p.IdUser == userId);
            if (userCredentialDb == null) return null;

            context.UserCredentials.Remove(userCredentialDb);
            await context.SaveChangesAsync();
            return userCredentialDb;
        }
        /*
        public async Task<bool> UpdateUserCredentials (int userId)
        {
            if (userId == 0) throw new ArgumentNullException(nameof(userId));

            using var context = _contextFactory.CreateDbContext();
            var userCredentialDb = await context.UserCredentials.FirstOrDefaultAsync(p => p.IdUser == userId);
            context.Entry(userCredentialDb).State = EntityState.Modified;
            return await context.SaveChangesAsync() > 0;
        }
        */
        public async Task<bool> UpdateUserCredentials(int IdUser,UserCredential userCredential)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                if (userCredential.IdUser != null)
                {
                    context.Entry(userCredential).State = EntityState.Modified;
                }
                else
                {
                    userCredential.IdUser = IdUser;
                    context.UserCredentials.Add(userCredential);
                }
                await context.SaveChangesAsync();
                _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se actualizo información del credenciales.");
                return true;
            }
            catch
            {
                _notificationService.Notify(NotificationSeverity.Error, "Error", "No se pudo actualizar información de credenciales.");
                return false;
            }


            
        }





    }
}
