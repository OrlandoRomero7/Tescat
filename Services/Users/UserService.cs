using Microsoft.EntityFrameworkCore;
using Tescat.Models;
using Tescat.Services.Emails;
using Tescat.Services.UserCredentials;
using System.IO;
using System.Linq;


namespace Tescat.Services.Users
{
    public class UserService : IUserService
    {

        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        private readonly IUserCredentialService _userCredentialService;
        private readonly IEmailService _userEmailService;
        private readonly IConfiguration _config;

        public UserService(IDbContextFactory<TescatDbContext> contextFactory, IUserCredentialService userCredentialService, IEmailService userEmailService, IConfiguration config)
        {
            _contextFactory = contextFactory;
            _userCredentialService = userCredentialService;
            _userEmailService = userEmailService;
            _config = config;

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
        
        public async Task<bool> UpdateUser(int IdOld,User updateUser, UserCredential userCredential, Email userEmail)
        {
            if (updateUser == null) throw new ArgumentNullException(nameof(updateUser));

            using var context = _contextFactory.CreateDbContext();

            if (IdOld != updateUser.IdUser)
            {

                //Cambiar el nombre de la imagen cuando el idUser cambia
                var cambio = await CambiarNombre(IdOld, updateUser);
                updateUser.IMAGE_NAME = cambio;
                //----------------------------------

                await InsertUser(updateUser);
                
                var pcDb = await context.Pcs.FirstOrDefaultAsync(p => p.IdUser == IdOld);

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
                if (userEmail == null) Console.WriteLine("El id old no tiene relacion en emailDb");
                else
                {
                    userEmail.IdUser = updateUser.IdUser;
                    context.Entry(userEmail).State = EntityState.Modified;
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
                if(userEmail.IdUser == null)
                {
                    userEmail.IdUser = updateUser.IdUser;
                    await _userEmailService.InsertUserEmail(userEmail);
                }
                else
                {
                    context.Entry(userEmail).State = EntityState.Modified;
                }
                
            }

            return await context.SaveChangesAsync() > 0;
        }
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
