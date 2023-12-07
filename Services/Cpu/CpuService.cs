using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Radzen;
using Tescat.Models;
using Tescat.Services.Notification;

namespace Tescat.Services.Cpus
{
    public class CpuService : ICpuService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        private readonly NotificationService _notificationService;

        public CpuService(IDbContextFactory<TescatDbContext> dbContextFactory,
            NotificationService notificationService)
        {
            _contextFactory = dbContextFactory;
            _notificationService = notificationService;
        }
        public async Task<Cpu> DeleteCpu(Guid cpuGuid)
        {
            using var context = _contextFactory.CreateDbContext();
            var cpudDb = await context.Cpus.FindAsync(cpuGuid);
            context.Cpus.Remove(cpudDb);
            await context.SaveChangesAsync();
            return cpudDb;
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
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Cpus.Add(cpu);
                await context.SaveChangesAsync();
                return cpu;
            }
            catch
            {
                return cpu;
            }
            
        }

        public async Task<Cpu> UpdateCpu(Cpu cpu, Guid IdPc)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                bool existeCpu = context.Cpus.Any(c => c.IdPc == IdPc);
                if (existeCpu)
                {
                    context.Entry(cpu).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    
                }
                else
                {
                    cpu.IdPc = IdPc;
                    context.Cpus.Add(cpu);
                    await context.SaveChangesAsync();
                }
                _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se actualizo procesador.");
                return cpu;
            }
            catch
            {
                _notificationService.Notify(NotificationSeverity.Error, "Error", "No se pudo actualizar procesador.");
                return cpu;
            }
            
        }
        public async Task<Cpu> UpdateCpuForStock(Cpu cpu)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(cpu).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return cpu;
        }
    }
}
