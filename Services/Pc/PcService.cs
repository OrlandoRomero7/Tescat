using Microsoft.EntityFrameworkCore;
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

        public Task<Pc> InsertPc(Pc pc)
        {
            throw new NotImplementedException();
        }

        public async Task<Pc> UpdatePc(Pc pc)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(pc).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return pc;

        }

        public Task<Pc> DeletePc(Guid IdPc)
        {
            throw new NotImplementedException();
        }
    }
}

