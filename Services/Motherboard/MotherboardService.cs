using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        public Task<Motherboard> DeleteMotherboard(Guid cpuGuid)
        {
            throw new NotImplementedException();
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
                return null;
            }
        }

        public Task<Motherboard> InsertMotherboard(Motherboard motherboard)
        {
            throw new NotImplementedException();
        }

        public async Task<Motherboard> UpdateMotherboard(Motherboard motherboard)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(motherboard).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return motherboard;
        }
    }
}
