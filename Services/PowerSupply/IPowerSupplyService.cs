using Tescat.Models;

namespace Tescat.Services.PowerSupplys
{
    public interface IPowerSupplyService
    {
        public Task<List<PowerSupply>> GetAllMotherboards();
        public Task<PowerSupply> GetPowerSupplyWithPcId(Guid guid);

        public Task<PowerSupply> InsertPowerSupply(PowerSupply powerSupply);

        public Task<PowerSupply> UpdatePowerSupply(PowerSupply powerSupply);

        public Task<PowerSupply> DeletePowerSupplyd(Guid cpuGuid);
    }
}
