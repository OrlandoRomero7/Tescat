using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Radzen;
using System;
using Tescat.Models;
using Tescat.Services.Notification;

namespace Tescat.Services.Motherboards
{
    public class MotherboardService : IMotherboardService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        private readonly NotificationService _notificationService;

        public MotherboardService(IDbContextFactory<TescatDbContext> dbContextFactory, NotificationService notificationService)
        {
            _contextFactory = dbContextFactory;
            _notificationService = notificationService;
        }
        public async Task<Motherboard> DeleteMotherboard(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var motherboardDb = await context.Motherboards.FindAsync(guid);
            context.Motherboards.Remove(motherboardDb);
            await context.SaveChangesAsync();
            return motherboardDb;
        }

        public Task<List<Motherboard>> GetAllMotherboards()
        {
            throw new NotImplementedException();
        }

        public async Task<Motherboard> GetMotherboardWithPcId(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var motherboardDb = await context.Motherboards
                                 .FirstOrDefaultAsync(motherboard => motherboard.IdPc == guid);
            if (motherboardDb != null)
            {
                return motherboardDb;
            }
            else
            {
                return new Motherboard();
            }
        }
        public async Task<List<Motherboard>> GetMotherboardsWithoutIdPC()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Motherboards.Where(m => m.IdPc == null).ToListAsync();
        }

        public async Task<Motherboard> GetMotherboard(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var motherboardDb = await context.Motherboards
                                 .FirstOrDefaultAsync(motherboard => motherboard.IdMotherboard == guid);
            if (motherboardDb != null)
            {
                return motherboardDb;
            }
            else
            {
                return new Motherboard();
            }
        }

        public async Task<Motherboard> InsertMotherboard(Motherboard motherboard)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Motherboards.Add(motherboard);
                await context.SaveChangesAsync();
                return motherboard;
            }
            catch
            {
                return motherboard;
            }

        }

        public async Task<Motherboard> UpdateMotherboard(Motherboard motherboard, Guid IdPc)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                if (motherboard.IdMotherboard != Guid.Empty)
                {
                    context.Entry(motherboard).State = EntityState.Modified;
                }
                else
                {
                    motherboard.IdPc = IdPc;
                    context.Motherboards.Add(motherboard);
                }
                await context.SaveChangesAsync();
                _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se actualizó la tarjeta madre.");
                return motherboard;
            }
            catch
            {
                _notificationService.Notify(NotificationSeverity.Error, "Error", "No se pudo actualizar la tarjeta madre.");
                return motherboard;
            }
            //bool existeMotherboard = context.Motherboards.Any(c => c.IdPc == IdPc);
        }
        public async Task<Motherboard> UpdateMotherboardForStock(Motherboard motherboard)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(motherboard).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return motherboard;
        }
    }
}
