using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Net.NetworkInformation;
using Tescat.Models;

namespace Tescat.Services.PowerSupplys
{
    public class PowerSupplyService : IPowerSupplyService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;

        public PowerSupplyService(IDbContextFactory<TescatDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
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
            using var context = _contextFactory.CreateDbContext();
            bool powerSuppyExists = context.PowerSupplies.Any(c => c.IdPc == IdPc);
            if(powerSuppyExists)
            {
                context.Entry(powerSupply).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return powerSupply;
            }
            else
            { 
                powerSupply.IdPc = IdPc;
                return await InsertPowerSupply(powerSupply);
            
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
