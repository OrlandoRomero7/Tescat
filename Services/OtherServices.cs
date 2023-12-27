using Microsoft.EntityFrameworkCore;
using Tescat.Models;

namespace Tescat.Services
{
    public class OtherServices
    {


        //private readonly TescatDbContext _context;
        private readonly IDbContextFactory<TescatDbContext> _contextFactory;

        public string Message;
        //public bool HasMessage => !string.IsNullOrEmpty(Message);

        public string LastNavigatedFrom { get; set; }

        public int NewUser { get; set; }

        public OtherServices(IDbContextFactory<TescatDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public string[]? Areas { get; set; }
        public async Task<string[]> getAreas()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Users.Where(u => u.Area != null).Select(u => u.Area).Distinct().ToArrayAsync();
            //return null;  // O return Array.Empty<string>(); para devolver un array vacío.
        }
        public async Task<string[]> getDepartaments()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Users.Where(u => u.Dept != null).Select(u => u.Dept).Distinct().ToArrayAsync();
        }

        //Estado para compartir mensaje utilzado cuando se emlimina un usuario en UserDetails

        public async Task ShowMessage(string message)
        {
            Message = message;
        }

        

    }
}

