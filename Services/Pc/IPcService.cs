using Tescat.Models;

namespace Tescat.Services.Pcs
{
    public interface IPcService
    {
        public Task<List<Pc>> GetAllPc();

        public Task<Pc> GetPcId(Guid IdPc);

        public Task<Pc> InsertPc(Pc pc);

        public Task<Pc> UpdatePc(Pc pc);

        public Task<Pc> DeletePc(Guid IdPc);

        public Task<Pc> QuitPc(Pc pc);

        public Task<Pc> GetPcWithIdUSer(int IdUSer);

        public Task<List<Pc>> GetNumberPc(int IdUser);

        public Task<Guid> GetPcForAssingComponent(int IdUser);

        public Task<Pc> QuitPcFromUserDetails(int IdUser);

    }
}
