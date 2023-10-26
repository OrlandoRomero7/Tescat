using Tescat.Models;

namespace Tescat.Services.Cpus
{
    public interface ICpuService
    {
        public Task<List<Cpu>> GetCpusWithoutIdPC();

        public Task<Cpu> GetCpu(Guid guid);
        public Task<Cpu> GetCpuWithPcId(Guid guid);

        public Task<Cpu> InsertCpu(Cpu cpu);

        public Task<Cpu> UpdateCpu(Cpu cpu, Guid IdPc);

        public Task<Cpu> DeleteCpu(Guid cpuGuid);

        public Task<Cpu> UpdateCpuForStock(Cpu cpu);
    }
}
