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

        public async Task<UserCredential> DeleteUserCredentials(int userID)
        {
            using var context = _contextFactory.CreateDbContext();
            var userCredentialDb = await context.UserCredentials.FindAsync(userID);
            if (userCredentialDb == null) return null;

            context.UserCredentials.Remove(userCredentialDb);
            await context.SaveChangesAsync();
            return userCredentialDb;
        }       
        

        

    }
}
