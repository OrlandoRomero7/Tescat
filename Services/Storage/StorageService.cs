using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Radzen;
using System;
using Tescat.Models;
using Tescat.Models.ExclusiveForApplication;

namespace Tescat.Services.Storages
{
    public class StorageService : IStorageService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        private readonly NotificationService _notificationService;

        public StorageService(IDbContextFactory<TescatDbContext> dbContextFactory, NotificationService notificationService)
        {
            _contextFactory = dbContextFactory;
            _notificationService = notificationService;
        }
        public async Task<Storage> DeleteStorage(Guid storageGuid)
        {
            using var context = _contextFactory.CreateDbContext();
            var storage = await context.Storages.FindAsync(storageGuid);
            context.Storages.Remove(storage);
            await context.SaveChangesAsync();
            return storage;
        }

        public async Task<List<Storage>> GetListStoragesWithoutIdPC()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Storages.Where(s => s.IdPc == null).ToListAsync();
        }

        //public async Task<List<Storage>> GetStorageWithoutIdPC()
        //{
        //    using var context = _contextFactory.CreateDbContext();
        //    return await context.Storages.Where(s => s.IdPc == null).ToListAsync();
        //}

        //public async Task<List<PcWithTotalRam>> GetMemoryRamsFromHomePage()
        //{
        //    using var context = _contextFactory.CreateDbContext();

        //    var pcsWithTotalRam = await context.Storages.Where(s=>s.AvailableStrge <= 40)
        //        .Select(pc => new PcWithTotalRam
        //        {
        //            Pc = pc,
        //            TotalRamCapacity = pc.MemoryRams.Sum(ram => ram.Size ?? 0)
        //        })
        //        .OrderBy(pcWithRam => pcWithRam.TotalRamCapacity)
        //        .ToListAsync();

        //    return pcsWithTotalRam;
        //}

        public async Task<List<PcWithAvailableStrge>> GetAvailableStrgeFromHomePage()
        {
            using var context = _contextFactory.CreateDbContext();

            var storagesWithPc = await context.Storages
                .Where(storage => storage.IdPc != null && storage.AvailableStrge <= 40)
                .Select(storage => new PcWithAvailableStrge
                {
                    Pc = context.Pcs.FirstOrDefault(pc => pc.IdPc == storage.IdPc),
                    AvailableStorage = storage.AvailableStrge ?? 0
                })
                .ToListAsync();

            return storagesWithPc;
        }



        public async Task<List<Storage>> GetStorageWithPcId(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var storageDbList = await context.Storages
                                 .Where(storage => storage.IdPc == guid)
                                 .OrderByDescending(s => s.IdStorage)
                                 .ToListAsync();
            return storageDbList;
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

        //public async Task<Storage> UpdateStorage(Storage storage, Guid IdPc)
        //{
        //    using var context = _contextFactory.CreateDbContext();
        //    bool existeStorage = context.Storages.Any(c => c.IdPc == IdPc);
        //    if (existeStorage)
        //    {
        //        context.Entry(storage).State = EntityState.Modified;
        //        await context.SaveChangesAsync();
        //        return storage;
        //    }
        //    else
        //    {
        //        storage.IdPc = IdPc;
        //        return await InsertStorage(storage);
        //    }

        //}
        public async Task<List<Storage>> UpdateStorages(List<Storage> updatedStorages, Guid IdPc)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                //List<Guid> originalOrder = updatedStorages.Select(s => s.IdStorage).ToList();

                foreach (var updatedStorage in updatedStorages)
                {
                    if (updatedStorage.IdStorage != Guid.Empty)
                    {
                        context.Entry(updatedStorage).State = EntityState.Modified;
                    }
                    else
                    {
                        updatedStorage.IdPc = IdPc;
                        context.Storages.Add(updatedStorage);
                        // context.Entry(updatedStorage).State = EntityState.Added;
                    }

                }
                await context.SaveChangesAsync();
                //Antes era verificaba igual a GuidEmpty pero lo cambie a null
                updatedStorages.RemoveAll(s => s.IdPc == null);
                _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se actualizo almacenamiento.");
                return updatedStorages;
            }
            catch
            {
                _notificationService.Notify(NotificationSeverity.Error, "Error", "No se pudo actualizar almacenamiento.");
                return updatedStorages;
            }
            // Recuperar las entidades ordenadas por el orden original
            // updatedStorages = context.Storages.Where(s => originalOrder.Contains(s.IdStorage)).ToList();
        }

        public async Task<Storage> UpdateStorageForStock(Storage storage)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Entry(storage).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return storage;
            }
            catch
            {
                return storage;
            }
            
        }
    }
}
