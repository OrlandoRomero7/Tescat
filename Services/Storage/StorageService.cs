using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Tescat.Models;

namespace Tescat.Services.Storages
{
    public class StorageService : IStorageService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;

        public StorageService(IDbContextFactory<TescatDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }
        public Task<Storage> DeleteStorage(Guid storageGuid)
        {
            throw new NotImplementedException();
        }

        public Task<List<Storage>> GetAllStorages()
        {
            throw new NotImplementedException();
        }

        public async Task<Storage> GetStorageWithPcId(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var storageDb = await context.Storages
                                 .FirstOrDefaultAsync(storage => storage.IdPc == guid);
            if (storageDb != null)
            {
                return storageDb;
            }
            else
            {
                return new Storage();
            }
        }

        public Task<Storage> InsertStorage(Storage storage)
        {
            throw new NotImplementedException();
        }

        public async Task<Storage> UpdateStorage(Storage storage)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(storage).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return storage;
        }
    }
}
