using Microsoft.EntityFrameworkCore;
using Tescat.Models;
using Tescat.Services.Emails;
using Tescat.Services.UserCredentials;
using System.IO;
using System.Linq;
using Radzen;
using Tescat.Services.Notification;


namespace Tescat.Services.Users
{
    public class UserService : IUserService
    {

        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        private readonly IUserCredentialService _userCredentialService;
        private readonly IEmailService _userEmailService;
        private readonly IConfiguration _config;
        private readonly NotificationService _notificationService;

        public UserService(IDbContextFactory<TescatDbContext> contextFactory, IUserCredentialService userCredentialService, IEmailService userEmailService, IConfiguration config, NotificationService notificationService)
        {
            _contextFactory = contextFactory;
            _userCredentialService = userCredentialService;
            _userEmailService = userEmailService;
            _config = config;
            _notificationService = notificationService;

        }
        public async Task<bool> ExistingUser(int userID)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingUser = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.IdUser == userID);

            return existingUser != null;
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

        public async Task<List<User>> GetAllUsers()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Users.ToListAsync();
        }

        public async Task<List<User>> GetUsersWithoutPC()
        {
            using var context = _contextFactory.CreateDbContext();

            var usersWithoutPC = await context.Users
                .Where(u => u.Pcs == null || !u.Pcs.Any()) // Filtrar usuarios sin relación con PC
                .ToListAsync();

            return usersWithoutPC;
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
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Users.Add(user);
                await context.SaveChangesAsync();
                _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se creo el usuario.");
            }
            catch
            {
                _notificationService.Notify(NotificationSeverity.Success, "Error", "Ocurrio un error al crear al usuario");
            }
            return user;

        }
        public async Task<User> UpdateUser(User updateUser)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Entry(updateUser).State = EntityState.Modified;
                await context.SaveChangesAsync();
                _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se creo actualizo informacion del usuario.");
            }
            catch
            {
                _notificationService.Notify(NotificationSeverity.Success, "Error", "Ocurrio un error al actualizar el usuario");
            }
            
            return updateUser;
        }

        //public async Task<User> UpdateUser(int IdOld, User updateUser)
        //{
        //    try
        //    {
        //        using var context = _contextFactory.CreateDbContext();

        //        var userEmailDb = await context.Emails.FirstOrDefaultAsync(p => p.IdUser == IdOld);
        //        var userCredentialDb = await context.UserCredentials.FirstOrDefaultAsync(p => p.IdUser == IdOld);
        //        var pcDb = await context.Pcs.FirstOrDefaultAsync(p => p.IdUser == IdOld);

        //        //Si el id fue actualizado entra
        //        if (IdOld != updateUser.IdUser)
        //        {

        //            //Cambiar el nombre de la imagen cuando el idUser cambia
        //            var cambio = await CambiarNombre(IdOld, updateUser);
        //            updateUser.IMAGE_NAME = cambio;
        //            //----------------------------------

        //            await InsertUser(updateUser);


        //            if (userCredentialDb != null && userCredentialDb.IdUser != null)
        //            {
        //                userCredentialDb.IdUser = updateUser.IdUser;
        //                context.Entry(userCredentialDb).State = EntityState.Modified;
        //            }
        //            if (pcDb != null && pcDb.IdUser != null)
        //            {
        //                pcDb.IdUser = updateUser.IdUser;
        //                context.Entry(pcDb).State = EntityState.Modified;
        //            }
        //            if (userEmailDb != null && userEmailDb.IdUser != null)
        //            {
        //                userEmailDb.IdUser = updateUser.IdUser;
        //                context.Entry(userEmailDb).State = EntityState.Modified;
        //            }
        //        }
        //        else
        //        {
        //            context.Entry(updateUser).State = EntityState.Modified;

        //            if (userCredentialDb.IdUser == null)
        //            {
        //                userCredentialDb.IdUser = updateUser.IdUser;
        //                await _userCredentialService.InsertUserCredentials(userCredentialDb);

        //            }
        //            else
        //            {
        //                context.Entry(userCredentialDb).State = EntityState.Modified;
        //            }
        //            if (userEmailDb.IdUser == null)
        //            {
        //                userEmailDb.IdUser = updateUser.IdUser;
        //                await _userEmailService.InsertUserEmail(userEmailDb);
        //            }
        //            else
        //            {
        //                context.Entry(userEmailDb).State = EntityState.Modified;
        //            }
        //        }
        //        _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se actualizo información del usuario.");
        //        await context.SaveChangesAsync();
        //    }
        //    catch
        //    {
        //        _notificationService.Notify(NotificationSeverity.Error, "Error", "No se pudo actualizar información del usuario.");
        //    }
        //    return updateUser;
        //}

        //public async Task<bool> UpdateUser(int IdOld, User updateUser, UserCredential userCredential, Email userEmail)
        //{
        //    if (updateUser == null) throw new ArgumentNullException(nameof(updateUser));

        //    try
        //    {
        //        using var context = _contextFactory.CreateDbContext();

        //        if (IdOld != updateUser.IdUser)
        //        {

        //            //Cambiar el nombre de la imagen cuando el idUser cambia
        //            var cambio = await CambiarNombre(IdOld, updateUser);
        //            updateUser.IMAGE_NAME = cambio;
        //            //----------------------------------

        //            await InsertUser(updateUser);

        //            var pcDb = await context.Pcs.FirstOrDefaultAsync(p => p.IdUser == IdOld);

        //            if (userCredential.IdUser == null) Console.WriteLine("El id old no tiene relacion userCredential");
        //            else
        //            {
        //                userCredential.IdUser = updateUser.IdUser;
        //                context.Entry(userCredential).State = EntityState.Modified;
        //            }
        //            //tal vez ocupe arreglar estos if para que sean igual al de arriba que entre al IdUser
        //            if (pcDb == null) Console.WriteLine("El id old no tiene relacion pcDb");
        //            else
        //            {
        //                pcDb.IdUser = updateUser.IdUser;
        //                context.Entry(pcDb).State = EntityState.Modified;
        //            }
        //            if (userEmail == null) Console.WriteLine("El id old no tiene relacion en emailDb");
        //            else
        //            {
        //                userEmail.IdUser = updateUser.IdUser;
        //                context.Entry(userEmail).State = EntityState.Modified;
        //            }
        //        }
        //        else
        //        {
        //            context.Entry(updateUser).State = EntityState.Modified;

        //            if (userCredential.IdUser == null)
        //            {
        //                userCredential.IdUser = updateUser.IdUser;
        //                await _userCredentialService.InsertUserCredentials(userCredential);

        //            }
        //            else
        //            {
        //                context.Entry(userCredential).State = EntityState.Modified;
        //            }
        //            if (userEmail.IdUser == null)
        //            {
        //                userEmail.IdUser = updateUser.IdUser;
        //                await _userEmailService.InsertUserEmail(userEmail);
        //            }
        //            else
        //            {
        //                context.Entry(userEmail).State = EntityState.Modified;
        //            }

        //        }
        //        _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se actualizo información del usuario.");
        //        return await context.SaveChangesAsync() > 0;
        //    }
        //    catch
        //    {
        //        _notificationService.Notify(NotificationSeverity.Error, "Error", "No se pudo actualizar información del usuario.");
        //        return false;
        //    }


        //}
        public Task<string> CambiarNombre(int IdOld, User updateUser)
        {
            string fileName = IdOld.ToString();
            string newFileName = updateUser.IdUser.ToString();
            string directoryPath = _config.GetValue<string>("FileStorage")!;
            string[] validImageExtensions = new[] { ".png", ".jpg", ".jpeg", ".webp", ".tiff" };

            // Buscar todos los archivos que coincidan con el nombre sin importar la extensión
            var matchingFiles = Directory.GetFiles(directoryPath, $"{fileName}.*")
                                         .Where(file => validImageExtensions.Contains(Path.GetExtension(file).ToLower()))
                                         .ToList();

            if (matchingFiles.Any())
            {
                string existingFilePath = matchingFiles.First();
                string newFileNameWithExtension = $"{newFileName}{Path.GetExtension(existingFilePath)}";
                string newFilePath = Path.Combine(directoryPath, newFileNameWithExtension);

                // Cambiar el nombre del archivo
                try
                {
                    File.Move(existingFilePath, newFilePath);
                    Console.WriteLine($"El archivo ha sido renombrado a {newFileNameWithExtension}.");
                    return Task.FromResult(newFileNameWithExtension); // Retornar el nombre nuevo
                }
                catch (IOException ex)
                {
                    // Manejo de error en caso de que no se pueda mover/renombrar el archivo
                    Console.WriteLine($"Error al intentar renombrar el archivo: {ex.Message}");
                    return Task.FromResult(string.Empty);
                }
            }
            else
            {
                Console.WriteLine($"No se encontró un archivo de imagen con nombre {fileName} en el directorio.");
                return Task.FromResult(string.Empty); // Retornar string vacío si no se encuentra el archivo
            }
        }

    }


}
