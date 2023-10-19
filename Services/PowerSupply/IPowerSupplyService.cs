using Tescat.Models;

namespace Tescat.Services.PowerSupplys
{
    public interface IPowerSupplyService
    {
        public Task<List<PowerSupply>> GetAllMotherboards();
        public Task<PowerSupply> GetPowerSupplyWithPcId(Guid guid);

        public Task<List<PowerSupply>> GetPowerSuppliesWithoutIdPC();

        public Task<PowerSupply> GetPowerSupply(Guid guid);

        public Task<PowerSupply> InsertPowerSupply(PowerSupply powerSupply);

        public Task<PowerSupply> UpdatePowerSupply(PowerSupply powerSupply, Guid IdPc);

        public Task<PowerSupply> DeletePowerSupplyd(Guid cpuGuid);
    }
}
