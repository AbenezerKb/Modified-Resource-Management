using ERP.Models;

namespace ERP.Services.SiteServices
{
    public interface ISiteService
    {
        Task<List<Site>> GetAll();
    }
}
