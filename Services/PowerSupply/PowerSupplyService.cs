using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Radzen;
using System.Net.NetworkInformation;
using Tescat.Models;
using Tescat.Services.Notification;

namespace Tescat.Services.PowerSupplys
{
    public class PowerSupplyService : IPowerSupplyService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;
        private readonly NotificationService _notificationService;

        public PowerSupplyService(IDbContextFactory<TescatDbContext> dbContextFactory, NotificationService notificationService)
        {
            _contextFactory = dbContextFactory;
            _notificationService = notificationService;
        }
        public async Task<PowerSupply> DeletePowerSupply(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var powerSupply = await context.PowerSupplies.FindAsync(guid);
            context.PowerSupplies.Remove(powerSupply);
            await context.SaveChangesAsync();

            return powerSupply;


        }


        public async Task<PowerSupply> GetPowerSupplyWithPcId(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var powerSupplyDb = await context.PowerSupplies
                                 .FirstOrDefaultAsync(powerSupply => powerSupply.IdPc == guid);
            if (powerSupplyDb != null)
            {
                return powerSupplyDb;
            }
            else
            {
                return new PowerSupply();
            }
        }
        public async Task<PowerSupply> GetPowerSupply(Guid guid)
        {
            using var context = _contextFactory.CreateDbContext();
            var powerSupplyDb = await context.PowerSupplies
                                 .FirstOrDefaultAsync(powerSupply => powerSupply.IdPsu == guid);
            if (powerSupplyDb != null)
            {
                return powerSupplyDb;
            }
            else
            {
                return new PowerSupply();
            }
        }
        public async Task<List<PowerSupply>> GetPowerSuppliesWithoutIdPC()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.PowerSupplies.Where(p => p.IdPc == null).ToListAsync();
        }

        public async Task<PowerSupply> InsertPowerSupply(PowerSupply powerSupply)
        {
            using var context = _contextFactory.CreateDbContext();
            context.PowerSupplies.Add(powerSupply);
            await context.SaveChangesAsync();
            return powerSupply;
        }

        public async Task<PowerSupply> UpdatePowerSupply(PowerSupply powerSupply, Guid IdPc)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                //bool powerSuppyExists = context.PowerSupplies.Any(c => c.IdPc == IdPc);
                if (powerSupply.IdPsu!=Guid.Empty)
                {
                    context.Entry(powerSupply).State = EntityState.Modified;
                }
                else
                {
                    powerSupply.IdPc = IdPc;
                    context.PowerSupplies.Add(powerSupply);
                }
                await context.SaveChangesAsync();
                _notificationService.Notify(NotificationSeverity.Success, "Completado", "Se actualizo fuente de poder.");
                return powerSupply;

            }
            catch
            {
                _notificationService.Notify(NotificationSeverity.Error, "Error", "No se pudo actualizar fuente de poder.");
                return powerSupply;
            }
            
        }

        public async Task<PowerSupply> UpdatePowerSupplyForStock(PowerSupply powerSupply)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(powerSupply).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return powerSupply;
        }
    }
}
