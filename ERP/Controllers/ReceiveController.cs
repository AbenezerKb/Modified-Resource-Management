using ERP.Context;
using ERP.DTOs;
using ERP.Models;
using ERP.Services.User;
using ERP.Services.ItemSiteQtyServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceiveItem = ERP.Models.ReceiveItem;

namespace ERP.Controllers
{

    [Route("/api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class ReceiveController : Controller
    {
        private readonly DataContext context;
        private readonly IItemSiteQtyService _itemSiteQtyService;
        private readonly IUserService _userService;

        public UserAccount? UserAccount { get; }

        public Employee Employee { get; }


        public ReceiveController(IItemSiteQtyService itemSiteQtyService, DataContext context, IUserService userService)
        {
            _itemSiteQtyService = itemSiteQtyService;
            this.context = context;

            _userService = userService;

            UserAccount = context.UserAccounts
                .Where(u => u.Username == _userService.GetMyName())
                .Include(u => u.Employee)
                .ThenInclude(e => e.UserRole)
                .FirstOrDefault();

            Employee = UserAccount.Employee;
        }

        [HttpGet]
        public async Task<ActionResult<List<Receive>>> Get()
        {

            var receives = await context.Receives
                .OrderByDescending(receive => receive.ReceiveId)
                .ToListAsync();

            return Ok(receives);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receive>> Get(int id)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanViewPurchase != 1) return Forbid();

            var receive = context.Receives.Where(receive => receive.ReceiveId == id)
               .Include(receive => receive.ReceivedBy)
               .Include(receive => receive.ApprovedBy)
               .Include(receive => receive.DeliveredBy)
               .Include(receive => receive.ReceiveItems)
               .ThenInclude(receiveItem => receiveItem.Item)
               .ThenInclude(item => item.Material)
               .Include(receive => receive.ReceiveItems)
               .ThenInclude(receiveItem => receiveItem.Item.Equipment)
               .FirstOrDefault();

            if (receive == null) return NotFound("Receive Not Found.");

            return Ok(receive);
        }

        [HttpGet("mysite")]
        public async Task<ActionResult<List<Receive>>> GetReceive()
        {
            //if (UserAccount != null && UserAccount.UserRole.CanViewPurchase != 1) return Forbid();

            var receives = await context.Receives
              .Where(receive => receive.ReceivingSiteId == Employee.EmployeeSiteId && receive.Status == RECEIVESTATUS.PURCHASED)
              .Include(receive => receive.ReceiveItems)
              .ThenInclude(receiveItem => receiveItem.Item)
              .ThenInclude(item => item.Material)
              .Include(receive => receive.ReceiveItems)
              .ThenInclude(receiveItem => receiveItem.Item.Equipment)
              .OrderByDescending(receive => receive.ReceiveId)
              .ToListAsync();

            if (receives == null) return NotFound("Not Found.");

            return Ok(receives);
        }

        [HttpPost("request")]
        public async Task<ActionResult<List<Receive>>> Post(CreateReceiveDTO receiveDTO)
        {

            var receive = context.Receives
                 .Where(r => r.ReceiveId == receiveDTO.ReceiveId)
                 .Include(r => r.ReceiveItems)
                 .FirstOrDefault();

            if (receive == null) return NotFound("Request Not Found.");
            
            receive.ReceiveDate = DateTime.Now;
            //receive.DeliveredById = receiveDTO.DeliveredById;
            receive.ReceivedById = Employee.EmployeeId;
            ICollection<ReceiveItem> receiveItems = new List<ReceiveItem>();

            foreach (var requestItem in receiveDTO.ReceiveItems)
            {
                var receiveItem = receive.ReceiveItems
                    .Where(rItem => rItem.ItemId == requestItem.ItemId)
                    .FirstOrDefault();

                if (receiveItem == null) return NotFound($"Receive Item with Id {requestItem.ItemId} Not Found");

                receiveItem.QtyReceived = requestItem.QtyReceived;
                receiveItem.ReceiveRemark = requestItem.ReceiveRemark;

                receiveItems.Add(receiveItem);
                
                await _itemSiteQtyService.AddMaterial(requestItem.ItemId, (int)Employee.EmployeeSiteId, requestItem.QtyReceived);
            }

            receive.Status = RECEIVESTATUS.RECEIVED;
            
            foreach (var buy in context.Buys.Where(buy => buy.PurchaseId == receive.PurchaseId && buy.Status == BUYSTATUS.QUEUED))
            {
                buy.Status = BUYSTATUS.BOUGHT;
            }
            
            //Notify employees who requested the purchase here

            await context.SaveChangesAsync();
                        
            return Ok(receive.ReceiveId);
        }

        [HttpPost("approve")]
        public async Task<ActionResult<Receive>> Approve(ApproveReceiveDTO approveDTO)
        {
            var receive = await context.Receives.FindAsync(approveDTO.ReceieveId);

            if (receive == null) return NotFound($"Request With ID {approveDTO.ReceieveId} Not Found.");

            receive.ApproveDate = DateTime.Now;
            receive.ApprovedById = Employee.EmployeeId;
            receive.Status = RECEIVESTATUS.APPROVED;
            receive.ApproveRemark = approveDTO.ApproveRemark;

            await context.SaveChangesAsync();

            return Ok(receive);
        }
    }
}
