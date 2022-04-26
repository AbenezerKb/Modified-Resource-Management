using ERP.Context;
using ERP.DTOs;
using ERP.Models;
using ERP.Services.MiscServices;
using ERP.Services.SiteServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiscController : Controller
    {
        private readonly DataContext context;
        private readonly IMiscService _miscService;

        public MiscController(DataContext context, IMiscService miscService)
        {
            this.context = context;
            _miscService = miscService;
        }

        [HttpGet("company")]
        public async Task<ActionResult<CompanyNamePrefixDTO>> GetNamePrefix()
        {
            var nameData = await _miscService.GetCompanyNamePrefix();

            return Ok(nameData);
        }

        [HttpPost("company")]
        public async Task<ActionResult<bool>> SetNamePrefix(CompanyNamePrefixDTO companyDTO)
        {
            var result = await _miscService.SetCompanyNamePrefix(companyDTO);

            return Ok(result);
        }
    }
}
