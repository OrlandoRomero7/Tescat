using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Radzen;
using System;
using Tescat.Models;
using Tescat.Models.ExclusiveForApplication;
using Tescat.Services.Notification;

namespace Tescat.Services.MemoryRams
{
    public class MemoryRamService : IMemoryRamService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        private readonly NotificationService _notificationService;

        public MemoryRamService(IDbContextFactory<TescatDbContext> dbContextFactory, NotificationService notificationService)
        {
            _contextFactory = dbContextFactory;
            _notificationService = notificationService;
        }
        public async Task<MemoryRam> DeleteMemoryRam(Guid storageGuid)
        {
            using var context = _contextFactory.CreateDbContext();
            var memoryRamDb = await context.MemoryRams.FindAsync(storageGuid);
            context.MemoryRams.Remove(memoryRamDb);
            await context.SaveChangesAsync();
            return memoryRamDb;
        }

        public async Task<List<PcWithTotalRam>> GetMemoryRamsFromHomePage()
        {
            using var context = _contextFactory.CreateDbContext();

            var pcsWithTotalRam = await context.Pcs
                .Where(pc => pc.MemoryRams.Any(ram => ram.Size != null && ram.Size > 0) && pc.MemoryRams.Sum(ram => ram.Size ?? 0) <= 8)
                .Select(pc => new PcWithTotalRam
                {
                    Pc = pc,
                    TotalRamCapacity = pc.MemoryRams.Sum(ram => ram.Size ?? 0)
                })
                .OrderBy(pcWithRam => pcWithRam.TotalRamCapacity)
                .ToListAsync();

            return pcsWithTotalRam;
        }


        public async Task<MemoryRam> GetMemoryRam(Guid guid)
        {

            using var context = _contextFactory.CreateDbContext();
            var memoryDb = await context.MemoryRams
                                 .FirstOrDefaultAsync(memory => memory.IdRam == guid);
            if (memoryDb != null)
            {
                return memoryDb;
            }
            else
            {
                return new MemoryRam();
            }
        }

        //public async Task<MemoryRam> GetMemoryRamWithPcId(Guid guid)
        //{
        //    using var context = _contextFactory.CreateDbContext();
        //    var memoryDb = await context.MemoryRams
        //                         .FirstOrDefaultAsync(memory => memory.IdPc == guid);
        //    if (memoryDb != null)
        //    {
        //        return memoryDb;
        //    }
        //    else
        //    {
        //        return new MemoryRam();
        //    }
        //}
        public async Task<List<MemoryRam>> GetMemoryRamWithPcId(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var ramDbList = await context.MemoryRams
                                 .Where(ram => ram.IdPc == guid).ToListAsync();
            return ramDbList;
        }


        public async Task<List<MemoryRam>> GetMemoryRamsWithoutIdPC()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.MemoryRams.Where(m => m.IdPc == null).ToListAsync();
        }

        public async Task<MemoryRam> InsertMemoryRam(MemoryRam memory)
        {
            using var context = _contextFactory.CreateDbContext();
            context.MemoryRams.Add(memory);
            await context.SaveChangesAsync();
            return memory;
        }

        //public async Task<MemoryRam> UpdateMemoryRam(MemoryRam memory, Guid IdPc)
        //{
        //    using var context = _contextFactory.CreateDbContext();
        //    bool ramExists = context.MemoryRams.Any(c => c.IdPc == IdPc);
        //    if (ramExists)
        //    {
        //        context.Entry(memory).State = EntityState.Modified;
        //        await context.SaveChangesAsync();
        //        return memory;
        //    }
        //    else
        //    {
        //        memory.IdPc = IdPc;
        //        return await InsertMemoryRam(memory);
        //    }

        //}
        public async Task<List<MemoryRam>> UpdateMemoryRam(List<MemoryRam> updatedRams, Guid IdPc)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                foreach (var updatedRam in updatedRams)
                {
                    if (updatedRam.IdRam != Guid.Empty)
                    {

                        context.Entry(updatedRam).State = EntityState.Modified;
                    }
                    else
                    {
                        updatedRam.IdPc = IdPc;
                        context.MemoryRams.Add(updatedRam);
                        // context.Entry(updatedStorage).State = EntityState.Added;
                    }

                }
                await context.SaveChangesAsync();
                //Antes era verificaba igual a GuidEmpty pero lo cambie a null
                updatedRams.RemoveAll(s => s.IdPc == null);
                _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se actualizo memoria ram.");

                return updatedRams;
            }
            catch
            {
                _notificationService.Notify(NotificationSeverity.Error, "Error", "No se pudo actualizar memoria ram.");
                return updatedRams;
            }
        }
        public async Task<MemoryRam> UpdateMemoryRamForStock(MemoryRam memory)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(memory).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return memory;
        }

        
    }


}
