using ERP.Context;
using ERP.Models;
using ERP.Services.SiteServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : Controller
    {
        private readonly DataContext context;
        private readonly ISiteService _siteService;

        public SiteController(DataContext context, ISiteService siteService)
        {
            this.context = context;
            _siteService = siteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Site>>> Get()
        {
            var sites = await _siteService.GetAll();
            return Ok(sites);
        }
    }
}
