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

        public async Task<List<Cpu>> GetCpusWithoutIdPC()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Cpus.Where(c => c.IdPc == null).ToListAsync();
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
                return new Cpu();
            }
        }
        public async Task<Cpu> GetCpu(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var cpuDb = await context.Cpus
                                 .FirstOrDefaultAsync(cpu => cpu.IdCpu == guid);
            if (cpuDb != null)
            {
                return cpuDb;
            }
            else
            {
                return new Cpu();
            }
        }

        public async Task<Cpu> InsertCpu(Cpu cpu)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Cpus.Add(cpu);
            await context.SaveChangesAsync();
            return cpu;
        }

        public async Task<Cpu> UpdateCpu(Cpu cpu, Guid IdPc)
        {
            using var context = _contextFactory.CreateDbContext();
            bool existeCpu = context.Cpus.Any(c => c.IdPc == IdPc);
            if (existeCpu)
            {
                context.Entry(cpu).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return cpu;
            }
            else
            {
                cpu.IdPc = IdPc;
                return await InsertCpu(cpu);
            }


                
        }
    }
}
