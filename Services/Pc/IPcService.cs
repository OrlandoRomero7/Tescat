using Tescat.Models;

namespace Tescat.Services.Pcs
{
    public interface IPcService
    {
        public Task<List<Pc>> GetAllPc();

        public Task<Pc> GetPcId(int IdPc);

        public Task<Pc> InsertPc(Pc pc);

        public Task<bool> UpdatePc(int IdPc, Pc updatePc);

        public Task<Pc> DeletePc(int IdPc);
    }
}
