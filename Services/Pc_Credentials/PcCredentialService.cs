using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Tescat.Models;

namespace Tescat.Services.Pc_Credentials
{
    public class PcCredentialService : IPcCredentialService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        public PcCredentialService(IDbContextFactory<TescatDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Task<PcCredential> DeletePcCredential(Guid IdPc)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsCredential(Guid IdPc)
        {
            using var context = _contextFactory.CreateDbContext();
            var credential = await context.PcCredentials.FindAsync(IdPc);

            return credential != null; // Retorna true si se encuentra, false si no se encuentra
        }

        public Task<List<PcCredential>> GetAllPcCredentials()
        {
            throw new NotImplementedException();
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

        public Task<PcCredential> QuitPcCredential(PcCredential pcCredential)
        {
            throw new NotImplementedException();
        }

        public async Task<PcCredential> UpdatePcCredential(PcCredential pcCredential)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(pcCredential).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return pcCredential;
        }
    }
}
