using ERP.Context;
using ERP.DTOs;
using ERP.Models;
using ERP.Services.ReturnServices;
using ERP.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class ReturnController : Controller
    {
        private readonly DataContext context;
        private readonly IReturnService _returnService;
        private readonly IUserService _userService;

        public ReturnController(DataContext context, IReturnService returnService, IUserService userService)
        {
            this.context = context;
            _returnService = returnService;

            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Return>> Get(int id)
        {

            try
            {
                var returnResult = await _returnService.GetById(id);
                return Ok(returnResult);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Borrow>>> Get()
        {

            var returns = await context.Returns
                .Include(ret => ret.Site)
                .ToListAsync();

            return Ok(returns);
        }


        [HttpPost("return")]
        public async Task<ActionResult<int>> ReturnEquipments(ReturnBorrowDTO returnDTO)
        {

            try
            {
                var borrowReturn = await _returnService.ReturnEquipments(returnDTO);
                return Ok(borrowReturn.ReturnId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("assets")]
        public async Task<ActionResult<List<EquipmentAsset>>> GetReturnableItems(GetReturnItemsDTO returnItemsDTO)
        {
            try
            {
                var assets = await _returnService.GetReturnableItems(returnItemsDTO);
                return Ok(assets);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("damage")]
        public async Task<ActionResult<List<AssetDamage>>> GetDamageTypes()
        {

            var damages = await _returnService.GetDamageTypes();
            return Ok(damages);

        }

    }
}
