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
                return null;
            }
        }

        public Task<MemoryRam> InsertMemoryRam(MemoryRam memory)
        {
            throw new NotImplementedException();
        }

        public async Task<MemoryRam> UpdateMemoryRam(MemoryRam memory)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(memory).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return memory;
        }
    }
}
