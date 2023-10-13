using Tescat.Models;

namespace Tescat.Services.Cpus
{
    public interface ICpuService
    {
        public Task<List<Cpu>> GetAllCpus();
        public Task<Cpu> GetCpuWithPcId(Guid guid);

        public Task<Cpu> InsertCpu(Cpu cpu);

        public Task<Cpu> UpdateCpu(Cpu cpu);

        public Task<Cpu> DeleteCpu(Guid cpuGuid);
    }
}
