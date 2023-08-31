using Microsoft.EntityFrameworkCore;
using Tescat.Models;

namespace Tescat.Services.UserCredentials
{
    public class UserCredentialService : IUserCredentialService
    {
        private readonly TescatDbContext _context;
        public UserCredentialService(TescatDbContext context)
        {
            _context = context;
        }
        public async Task<UserCredential> GetUserCredentials(int userId)
        {
            return await _context.UserCredentials.FirstOrDefaultAsync(p => p.IdUser == userId);

        }
        
    }
}
