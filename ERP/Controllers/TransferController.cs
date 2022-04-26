using ERP.Context;
using ERP.DTOs;
using ERP.Models;
using ERP.Services.NotificationServices;
using ERP.Services.TransferServices;
using ERP.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TransferItem = ERP.Models.TransferItem;

namespace ERP.Controllers
{

    [Route("/api/[controller]")]
    [ApiController]
    //comment out the line below to disable the authorization
    [Authorize(Roles = "Employee")]
    public class TransferController : Controller
    {
        private readonly ITransferService _transferService;
        private readonly DataContext context;
        private readonly INotificationService notificationManager;

        private readonly IUserService _userService;


        public TransferController(ITransferService transferService, DataContext context, IUserService userService, INotificationService notificationManager)
        {
            _transferService = transferService;
            this.context = context;
            this.notificationManager = notificationManager;

            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transfer>> Get(int id)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanViewTransfer != 1) return Forbid();

            //if(_userService == null || !_userService.UserRole.CanViewTransfer)
            try
            {
                Transfer transfer = await _transferService.GetById(id);
                return Ok(transfer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            
        }

        [HttpGet]
        public async Task<ActionResult<List<Transfer>>> Get()
        {

            var transfers = await context.Transfers
                .Include(transfer => transfer.SendSite)
                .Include(transfer => transfer.ReceiveSite)
                .ToListAsync();

            return Ok(transfers);
        }

        [HttpPost]
        public async Task<ActionResult<List<Transfer>>> GetByParams(GetTransfersDTO getTransfersDTO)
        {

            var transfers = await _transferService.GetByCondition(getTransfersDTO);

            return Ok(transfers);
        }

        [HttpPost("request/equipment")]
        public async Task<ActionResult<int>> RequestEquipment(CreateEquipmentTransferDTO transferDTO)
        {

            try
            {
                var transfer = await _transferService.RequestEquipment(transferDTO);
                return Ok(transfer.TransferId);
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

        [HttpPost("request/material")]
        public async Task<ActionResult<List<Transfer>>> RequestMaterial(CreateMaterialTransferDTO transferDTO)
        {
            
            try
            {
                var transfer = await _transferService.RequestMaterial(transferDTO);
                return Ok(transfer.TransferId);
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

        [HttpPost("approve")]
        public async Task<ActionResult<Transfer>> Approve(ApproveTransferDTO approveDTO)
        {
            try
            {
                var transfer = await _transferService.ApproveTransfer(approveDTO);
                return Ok(transfer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("decline")]
        public async Task<ActionResult<Transfer>> Decline(DeclineTransferDTO declineDTO)
        {
            try
            {
                var transfer = await _transferService.DeclineTransfer(declineDTO);
                return Ok(transfer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("send")]
        public async Task<ActionResult<Transfer>> Send(SendTransferDTO sendDTO)
        {

            try
            {
                var transfer = await _transferService.SendTransfer(sendDTO);
                return Ok(transfer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost("receive")]
        public async Task<ActionResult<Transfer>> Receive(ReceiveTransferDTO receiveDTO)
        {


            try
            {
                var transfer = await _transferService.ReceiveTransfer(receiveDTO);
                return Ok(transfer);
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

    }
}
