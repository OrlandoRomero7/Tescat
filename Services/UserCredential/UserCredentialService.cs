using Microsoft.EntityFrameworkCore;
using Tescat.Models;

namespace Tescat.Services.UserCredentials
{
    public class UserCredentialService : IUserCredentialService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        public UserCredentialService(IDbContextFactory<TescatDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
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
        public async Task<bool> UpdateUserCredentials(UserCredential userCredential)
        {
            if (userCredential == null) throw new ArgumentNullException(nameof(userCredential));

            using var context = _contextFactory.CreateDbContext();
            context.Entry(userCredential).State = EntityState.Modified;
            return await context.SaveChangesAsync() > 0;
        }





    }
}
