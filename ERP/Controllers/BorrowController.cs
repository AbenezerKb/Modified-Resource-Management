using ERP.Context;
using ERP.DTOs;
using ERP.Models;
using ERP.Services.BorrowServices;
using ERP.Services.NotificationServices;
using ERP.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class BorrowController : Controller
    {
        private readonly DataContext context;
        private readonly IUserService _userService;
        private readonly IBorrowService _borrowService;

        public BorrowController(IBorrowService borrowService, IUserService userService)
        {
            this.context = context;
            _borrowService = borrowService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Borrow>> Get(int id)
        {

            try
            {
                var borrow = await _borrowService.GetById(id);
                return Ok(borrow);
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

        [HttpGet]
        public async Task<ActionResult<List<Borrow>>> Get()
        {

            var borrows = await context.Borrows
                .Include(borrow => borrow.Site)
                .ToListAsync();

            return Ok(borrows);
        }

        [HttpPost]
        public async Task<ActionResult<List<Borrow>>> GetByCondition(GetBorrowsDTO getBorrowsDTO)
        {
            var Borrows = await _borrowService.GetByCondition(getBorrowsDTO);

            return Ok(Borrows);
        }

        [HttpPost("request")]
        public async Task<ActionResult<int>> Post(CreateBorrowDTO borrowDTO)
        {
            try
            {
                var borrow = await _borrowService.RequestBorrow(borrowDTO);
                return Ok(borrow.BorrowId);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("approve")]
        public async Task<ActionResult<Borrow>> Approve(ApproveBorrowDTO approveDTO)
        {
            try
            {
                var borrow = await _borrowService.ApproveBorrow(approveDTO);
                return Ok(borrow);
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

        [HttpPost("decline")]
        public async Task<ActionResult<Borrow>> Decline(DeclineBorrowDTO declineDTO)
        {
            try
            {
                var borrow = await _borrowService.DeclineBorrow(declineDTO);
                return Ok(borrow);
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

        [HttpPost("hand")]
        public async Task<ActionResult<Borrow>> Hand(HandBorrowDTO handDTO)
        {

            try
            {
                var borrow = await _borrowService.HandBorrow(handDTO);
                return Ok(borrow);
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


    }
}
