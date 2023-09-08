using Microsoft.EntityFrameworkCore;
using Tescat.Models;

namespace Tescat.Services.Users
{
    public class UserService : IUserService
    {

        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        public UserService(IDbContextFactory<TescatDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<User> DeleteUser(int userID)
        {
            using var context = _contextFactory.CreateDbContext();
            var user = await context.Users.FindAsync(userID);
            if (user == null) return null;

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }

        public Task<List<User>> GetAllUsers()
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Users.ToListAsync();
        }

        public async Task<User> GetUserId(int userID)
        {
            using var context = _contextFactory.CreateDbContext();
            var userdb = await context.Users.FindAsync(userID);
            /*
            var userdb = await context.Users
                          .Include(u => u.UserCredential)
                          .SingleOrDefaultAsync(u => u.IdUser == userID);*/
            if (userdb!=null)
            {
                //userdb.UserCredential ??= new UserCredential();
                return userdb;
            }
            else
            {
                /*
                var newUser = new User
                {
                    UserCredential = new UserCredential()
                };
                */
                return new User();
                
            }

        }

        public async Task<User> InsertUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            using var context = _contextFactory.CreateDbContext();
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUser(User updateUser)
        {
            if (updateUser == null) throw new ArgumentNullException(nameof(updateUser));

            using var context = _contextFactory.CreateDbContext();
            context.Entry(updateUser).State = EntityState.Modified;
            return await context.SaveChangesAsync() > 0;

        }
    }
}
