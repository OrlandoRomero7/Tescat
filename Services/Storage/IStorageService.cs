using Tescat.Models;

namespace Tescat.Services.Storages
{
    public interface IStorageService
    {
        public Task<List<Storage>> GetAllStorages();
        public Task<Storage> GetStorageWithPcId(Guid guid);

        public Task<Storage> InsertStorage(Storage storage);

        public Task<Storage> UpdateStorage(Storage storage, Guid IdPc);

        public Task<Storage> DeleteStorage(Guid storageGuid);
    }
}
