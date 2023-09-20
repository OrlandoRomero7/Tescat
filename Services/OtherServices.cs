﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<string[]> getAreas()
        {
            return await _context.Users.Where(u => u.Area != null).Select(u => u.Area).Distinct().ToArrayAsync();
            //return null;  // O return Array.Empty<string>(); para devolver un array vacío.
        }
        public async Task<string[]> getDepartaments()
        {
            return await _context.Users.Where(u => u.Dept != null).Select(u => u.Dept).Distinct().ToArrayAsync();
        }

    }
}

