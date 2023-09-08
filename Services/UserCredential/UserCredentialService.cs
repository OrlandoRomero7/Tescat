using Microsoft.EntityFrameworkCore;
using Tescat.Models;

namespace Tescat.Services.UserCredentials
{
    public class UserCredentialService : IUserCredentialService
    {
        private readonly IDbContextFactory<TescatDbContext>  _contextFactory;
        public UserCredentialService(IDbContextFactory<TescatDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<UserCredential> GetUserCredentials(int userId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.UserCredentials.FirstOrDefaultAsync(p => p.IdUser == userId);
            //return await _contextFactory.UserCredentialService.FirstOrDefaultAsync(p => p.IdUser == userId);

        }
        
    }
}
