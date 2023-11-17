using Tescat.Models;

namespace Tescat.Services.Pc_Credentials
{
    public interface IPcCredentialService
    {
        public Task<List<PcCredential>> GetAllPcCredentials();

        public Task<PcCredential> GetPcCredentialId(Guid IdPc);

        public Task<PcCredential> InsertPcCredential(PcCredential pcCredential);

        public Task<PcCredential> UpdatePcCredential(PcCredential pcCredential);

        public Task<PcCredential> DeletePcCredential(Guid IdPc);

        public Task<PcCredential> QuitPcCredential(PcCredential pcCredential);

        public Task<bool> ExistsCredential(Guid IdPc);



    }
}
