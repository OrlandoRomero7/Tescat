﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
        public async Task<Storage> DeleteStorage(Guid storageGuid)
        {
            using var context = _contextFactory.CreateDbContext();
            var storage = await context.Storages.FindAsync(storageGuid);
            context.Storages.Remove(storage);
            await context.SaveChangesAsync();
            return storage;
        }

        public async Task<List<Storage>> GetStoragesWithoutIdPC()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Storages.Where(s => s.IdPc == null).ToListAsync();
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
        public async Task<Storage> GetStorage(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var storageDb = await context.Storages
                                 .FirstOrDefaultAsync(storage => storage.IdStorage == guid);
            if (storageDb != null)
            {
                return storageDb;
            }
            else
            {
                return new Storage();
            }
        }

        public async Task<Storage> InsertStorage(Storage storage)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Storages.Add(storage);
            await context.SaveChangesAsync();
            return storage;
        }

        public async Task<Storage> UpdateStorage(Storage storage, Guid IdPc)
        {
            using var context = _contextFactory.CreateDbContext();
            bool existeStorage = context.Storages.Any(c => c.IdPc == IdPc);
            if (existeStorage)
            {
                context.Entry(storage).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return storage;
            }
            else
            {
                storage.IdPc = IdPc;
                return await InsertStorage(storage);
            }
            
        }

        public async Task<Storage> UpdateStorageForStock(Storage storage)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(storage).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return storage;
        }
    }
}
