using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        public Task<MemoryRam> DeleteMemoryRam(Guid storageGuid)
        {
            throw new NotImplementedException();
        }

        public Task<List<MemoryRam>> GetAllMemoryRams()
        {
            throw new NotImplementedException();
        }

        public async Task<MemoryRam> GetMemoryRamWithPcId(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var memoryDb = await context.MemoryRams
                                 .FirstOrDefaultAsync(memory => memory.IdPc == guid);
            if (memoryDb != null)
            {
                return memoryDb;
            }
            else
            {
                return new MemoryRam();
            }
        }

        public async Task<MemoryRam> InsertMemoryRam(MemoryRam memory)
        {
            using var context = _contextFactory.CreateDbContext();
            context.MemoryRams.Add(memory);
            await context.SaveChangesAsync();
            return memory;
        }

        public async Task<MemoryRam> UpdateMemoryRam(MemoryRam memory, Guid IdPc)
        {
            using var context = _contextFactory.CreateDbContext();
            bool ramExists = context.MemoryRams.Any(c => c.IdPc == IdPc);
            if (ramExists)
            {
                context.Entry(memory).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return memory;
            }
            else
            {
                memory.IdPc = IdPc;
                return await InsertMemoryRam(memory);
            }
            
        }
    }
}
