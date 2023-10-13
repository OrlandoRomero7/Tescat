using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Tescat.Models;

namespace Tescat.Services.Cpus
{
    public class CpuService : ICpuService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;

        public CpuService(IDbContextFactory<TescatDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }
        public Task<Cpu> DeleteCpu(Guid cpuGuid)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cpu>> GetAllCpus()
        {
            throw new NotImplementedException();
        }

        public async Task<Cpu> GetCpuWithPcId(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var cpuDb = await context.Cpus
                                 .FirstOrDefaultAsync(cpu => cpu.IdPc == guid);
            if (cpuDb != null)
            {
                return cpuDb;
            }
            else
            {
                return null;
            }
        }

        public Task<Cpu> InsertCpu(Cpu cpu)
        {
            throw new NotImplementedException();
        }

        public async Task<Cpu> UpdateCpu(Cpu cpu)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(cpu).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return cpu;
        }
    }
}
