using Microsoft.EntityFrameworkCore;
using Tescat.Models;

namespace Tescat.Services
{
    public class OtherServices
    {
        private readonly TescatDbContext _context;
        public OtherServices(TescatDbContext context)
        {
            _context = context;
        }
        public string[]? Areas { get; set; }
        public async Task<string[]> prueba()
        {
            return await _context.Users.Select(u => u.Area).Distinct().ToArrayAsync();
            

            //return null;  // O return Array.Empty<string>(); para devolver un array vacío.
        }

    }
}

