using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Tracing;
using Tescat.Models;

namespace Tescat.Services.Pcs
{
    public class PcService : IPcService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;

        public PcService(IDbContextFactory<TescatDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }
        public async Task<List<Pc>> GetAllPc()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Pcs.ToListAsync();
        }

        public async Task<Pc> GetPcId(Guid IdPc)
        {
            using var context = _contextFactory.CreateDbContext();
            var pcDb = await context.Pcs.FindAsync(IdPc);
            if (pcDb != null)
            {
                return pcDb;
            }
            else
            {
                return null;
            }
        }

        public async Task<Guid> GetPcForAssingComponent(int IdUser)
        {
            using var context = _contextFactory.CreateDbContext();
            var pcDb = await context.Pcs.Where(c => c.IdUser == IdUser).FirstAsync();
            if (pcDb != null)
            {
                return pcDb.IdPc;
            }
            else
            {
                return Guid.Empty;
            }

        }
        //private List<Pc> pcDb = new List<Pc>();
        public async Task<List<Pc>> GetNumberPc(int IdUser)
        {

            using var context = _contextFactory.CreateDbContext();
            var pcDb = await context.Pcs.Where(pc => pc.IdUser == IdUser).ToListAsync();
            return pcDb.Count > 0 ? pcDb : new List<Pc>();

        }

        public async Task<Pc> GetPcWithIdUSer(int IdUSer)
        {
            using var context = _contextFactory.CreateDbContext();
            var pcDb = await context.Pcs.FindAsync(IdUSer);
            if (pcDb != null)
            {
                return pcDb;
            }
            else
            {
                return null;
            }
        }

        public async Task<Pc> InsertPc(Pc pc)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Pcs.Add(pc);
            await context.SaveChangesAsync();
            return pc;
        }

        public async Task<Pc> UpdatePc(Pc pc)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(pc).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return pc;

        }
        //Revisar esta funcion
        public async Task<Pc> QuitPc(Pc pc)
        {
            using var context = _contextFactory.CreateDbContext();

            // Attach the entity without marking it as modified
            context.Pcs.Attach(pc);

            // Mark only the ID_USER property as modified
            context.Entry(pc).Property(p => p.IdUser).IsModified = true;

            await context.SaveChangesAsync();
            return pc;
        }
        public async Task<Pc> QuitPcFromUserDetails(int IdUser)
        {
            using var context = _contextFactory.CreateDbContext();
            var pcDb = await context.Pcs.FirstOrDefaultAsync(c => c.IdUser == IdUser);
            if(pcDb!=null)
            {
                pcDb.IdUser = null;
                await context.SaveChangesAsync();
            }
            return pcDb;
        }

        public Task<Pc> DeletePc(Guid IdPc)
        {
            throw new NotImplementedException();
        }

    }
}

