using Microsoft.EntityFrameworkCore;
using Tescat.Models;
using Tescat.Services.UserCredentials;

namespace Tescat.Services.Users
{
    public class UserService : IUserService
    {

        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        private readonly IUserCredentialService _userCredentialService;
        public UserService(IDbContextFactory<TescatDbContext> contextFactory, IUserCredentialService userCredentialService)
        {
            _contextFactory = contextFactory;
            _userCredentialService = userCredentialService;
        }
        public async Task<User> DeleteUser(int userID)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var user = await context.Users.FindAsync(userID);
                if (user == null) return null;

                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            
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
            
            if (userdb != null)
            {
                return userdb;
            }
            else
            {
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
        
        public async Task<bool> UpdateUser(int IdOld,User updateUser, UserCredential userCredential)
        {
            if (updateUser == null) throw new ArgumentNullException(nameof(updateUser));

            using var context = _contextFactory.CreateDbContext();

            if (IdOld != updateUser.IdUser)
            {
                await InsertUser(updateUser);
                var pcDb = await context.Pcs.FirstOrDefaultAsync(p => p.IdUser == IdOld);
                var emailDb = await context.Emails.FirstOrDefaultAsync(p => p.IdUser == IdOld);

                if (userCredential.IdUser == null) Console.WriteLine("El id old no tiene relacion userCredential");
                else
                {
                    userCredential.IdUser = updateUser.IdUser;
                    context.Entry(userCredential).State = EntityState.Modified;
                }
                //tal vez ocupe arreglar estos if para que sean igual al de arriba que entre al IdUser
                if (pcDb == null) Console.WriteLine("El id old no tiene relacion pcDb");
                else
                {
                    pcDb.IdUser = updateUser.IdUser;
                    context.Entry(pcDb).State = EntityState.Modified;
                }
                if (emailDb == null) Console.WriteLine("El id old no tiene relacion en emailDb");
                else
                {
                    emailDb.IdUser = updateUser.IdUser;
                    context.Entry(emailDb).State = EntityState.Modified;
                }
            }
            else
            {
                context.Entry(updateUser).State = EntityState.Modified;
                
                if(userCredential.IdUser==null)
                {
                    userCredential.IdUser = updateUser.IdUser;
                    await _userCredentialService.InsertUserCredentials(userCredential);                    
                    
                }
                else
                {
                    context.Entry(userCredential).State = EntityState.Modified;
                }
            }

            return await context.SaveChangesAsync() > 0;
        }

    }
}
