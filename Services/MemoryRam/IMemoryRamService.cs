using Tescat.Models;

namespace Tescat.Services.MemoryRams
{
    public interface IMemoryRamService
    {
        public Task<List<MemoryRam>> GetAllMemoryRams();
        public Task<MemoryRam> GetMemoryRamWithPcId(Guid guid);

        public Task<MemoryRam> InsertMemoryRam(MemoryRam memory);

        public Task<MemoryRam> UpdateMemoryRam(MemoryRam memory,Guid IdPC);

        public Task<MemoryRam> DeleteMemoryRam(Guid memoryGuid);
    }
}
