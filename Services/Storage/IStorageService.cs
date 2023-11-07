using Tescat.Models;

namespace Tescat.Services.Storages
{
    public interface IStorageService
    {
        public Task<List<Storage>> GetListStoragesWithoutIdPC();

        //public Task<Storage> GetStorageWithoutIdPC();

        public Task<Storage> GetStorage(Guid guid);
        public Task<List<Storage>> GetStorageWithPcId(Guid guid);

        public Task<Storage> InsertStorage(Storage storage);

        //public Task<Storage> UpdateStorage(Storage storage, Guid IdPc);

        public Task<Storage> DeleteStorage(Guid storageGuid);

        public Task<Storage> UpdateStorageForStock(Storage storage);

        public Task<List<Storage>> UpdateStorages(List<Storage> updatedStorages, Guid IdPc);

    }
}
