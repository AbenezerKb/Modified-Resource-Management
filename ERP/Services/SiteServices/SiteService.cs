using ERP.Context;
using ERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.Services.SiteServices
{
    public class SiteService : ISiteService
    {
        private readonly DataContext _context;

        public SiteService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Site>> GetAll()
        {
            var sites = await _context.Sites.ToListAsync();

            return sites;

        }
    }
}
