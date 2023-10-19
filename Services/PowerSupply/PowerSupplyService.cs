﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        public Task<PowerSupply> DeletePowerSupplyd(Guid cpuGuid)
        {
            throw new NotImplementedException();
        }

        public Task<List<PowerSupply>> GetAllMotherboards()
        {
            throw new NotImplementedException();
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
    }
}