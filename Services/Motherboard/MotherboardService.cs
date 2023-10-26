using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using Tescat.Models;

namespace Tescat.Services.Motherboards
{
    public class MotherboardService : IMotherboardService
    {
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;

        public MotherboardService(IDbContextFactory<TescatDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
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
            return await context.Motherboards.Where(m => m.IdPc==null).ToListAsync();
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
            using var context = _contextFactory.CreateDbContext();
            context.Motherboards.Add(motherboard);
            await context.SaveChangesAsync();
            return motherboard;
        }

        public async Task<Motherboard> UpdateMotherboard(Motherboard motherboard, Guid IdPc)
        {
            using var context = _contextFactory.CreateDbContext();
            bool existeMotherboard = context.Motherboards.Any(c => c.IdPc == IdPc);

            if (existeMotherboard)
            {
                context.Entry(motherboard).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return motherboard;
            }
            else
            {
                motherboard.IdPc = IdPc;
                return await InsertMotherboard(motherboard);
            }
            
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
