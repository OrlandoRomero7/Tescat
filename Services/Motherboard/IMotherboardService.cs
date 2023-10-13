using Tescat.Models;

namespace Tescat.Services.Motherboards
{
    public interface IMotherboardService
    {
        public Task<List<Motherboard>> GetAllMotherboards();
        public Task<Motherboard> GetMotherboardWithPcId(Guid guid);

        public Task<Motherboard> InsertMotherboard(Motherboard motherboard);

        public Task<Motherboard> UpdateMotherboard(Motherboard motherboard);

        public Task<Motherboard> DeleteMotherboard(Guid cpuGuid);
    }
}
