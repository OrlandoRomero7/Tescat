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

            var existingUser = await context.Users.FindAsync(updateUser.IdUser);
            //if (existingUser == null) throw new ArgumentException("User not found");

            // Si el IdUser ha sido modificado.
            if (existingUser == null)
            {
                User newUser = new User
                {
                    IdUser = updateUser.IdUser,
                    Name = updateUser.Name,
                    Area = updateUser.Area,
                    Position = updateUser.Position,
                    EntryDate = updateUser.EntryDate,
                    Tel = updateUser.Tel,
                    WebPrivileges = updateUser.WebPrivileges,
                    LastModif = updateUser.LastModif,
                    Dept = updateUser.Dept,
                    Office = updateUser.Office,
                    LastWorkingDate = updateUser.LastWorkingDate,
                    TelKey = updateUser.TelKey,
                    Cel = updateUser.Cel
                };


                await InsertUser(newUser);
               
                var pcDb = await context.Pcs.FirstOrDefaultAsync(p => p.IdUser == IdOld);
                var emailDb = await context.Emails.FirstOrDefaultAsync(p => p.IdUser == IdOld);

                if (userCredential == null) Console.WriteLine("El id old no tiene relacion userCredential");
                else
                {
                    userCredential.IdUser = newUser.IdUser;
                    context.Entry(userCredential).State = EntityState.Modified;
                }
                
                if (pcDb == null) Console.WriteLine("El id old no tiene relacion pcDb");
                else
                {
                    pcDb.IdUser = newUser.IdUser;
                    context.Entry(pcDb).State = EntityState.Modified;
                }
                if (emailDb == null) Console.WriteLine("El id old no tiene relacion en emailDb");
                else
                {
                    emailDb.IdUser = newUser.IdUser;
                    context.Entry(emailDb).State = EntityState.Modified;
                }

            }
            else
            {
                context.Entry(existingUser).CurrentValues.SetValues(updateUser);
                Console.WriteLine(userCredential.IdUser);
                //if(userCredential.IdUser!=0)
                //{
                //    context.Entry(userCredential).State = EntityState.Modified;
                //}
            }

            return await context.SaveChangesAsync() > 0;
        }

    }
}
