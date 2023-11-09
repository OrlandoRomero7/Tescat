using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using Tescat.Models;

namespace Tescat.Services.MemoryRams
{
    public class MemoryRamService : IMemoryRamService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;

        public MemoryRamService(IDbContextFactory<TescatDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }
        public async Task<MemoryRam> DeleteMemoryRam(Guid storageGuid)
        {
            using var context = _contextFactory.CreateDbContext();
            var memoryRamDb = await context.MemoryRams.FindAsync(storageGuid);
            context.MemoryRams.Remove(memoryRamDb);
            await context.SaveChangesAsync();
            return memoryRamDb;
        }

        public Task<List<MemoryRam>> GetAllMemoryRams()
        {
            throw new NotImplementedException();
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

            return updatedRams;
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
