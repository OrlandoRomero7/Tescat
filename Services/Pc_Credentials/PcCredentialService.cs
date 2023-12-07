using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Tescat.Models;
using Tescat.Services.Notification;

namespace Tescat.Services.Pc_Credentials
{
    public class PcCredentialService : IPcCredentialService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        private readonly NotificationService _notificationService;
        public PcCredentialService(IDbContextFactory<TescatDbContext> contextFactory, NotificationService notificationService)
        {
            _contextFactory = contextFactory;
            _notificationService = notificationService;

        }

        public async Task<bool> ExistsCredential(Guid IdPc)
        {
            using var context = _contextFactory.CreateDbContext();
            var credential = await context.PcCredentials.FindAsync(IdPc);

            return credential != null; // Retorna true si se encuentra, false si no se encuentra
        }

        public async Task<PcCredential> GetPcCredentialId(Guid IdPc)
        {
            using var context = _contextFactory.CreateDbContext();
            var pcCredentialDb = await context.PcCredentials.FindAsync(IdPc);
            //return pcCredentialDb;
            if (pcCredentialDb != null)
            {
                return pcCredentialDb;
            }
            else
            {
                return new PcCredential();
            }
        }

        public async Task<PcCredential> InsertPcCredential(PcCredential pcCredential)
        {
            using var context = _contextFactory.CreateDbContext();
            context.PcCredentials.Add(pcCredential);
            await context.SaveChangesAsync();
            return pcCredential;
        }

        public async Task<PcCredential> UpdatePcCredential(PcCredential pcCredential)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Entry(pcCredential).State = EntityState.Modified;
                await context.SaveChangesAsync();
                _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se actualizo credenciales de pc.");
                return pcCredential;
            }
            catch{
                _notificationService.Notify(NotificationSeverity.Error, "Error", "No se pudo actualizar credenciales de pc.");
                return pcCredential;
            }
        }
        public Task<PcCredential> QuitPcCredential(PcCredential pcCredential)
        {
            throw new NotImplementedException();
        }
        public Task<List<PcCredential>> GetAllPcCredentials()
        {
            throw new NotImplementedException();
        }
        public Task<PcCredential> DeletePcCredential(Guid IdPc)
        {
            throw new NotImplementedException();
        }
    }
}
