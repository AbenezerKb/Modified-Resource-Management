using ERP.Context;
using ERP.DTOs;
using ERP.DTOs.Buy;
using ERP.Models;
using ERP.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class BuyController : Controller
    {
        private readonly DataContext context;
        private readonly IUserService _userService;

        public UserAccount? UserAccount { get; }

        public Employee Employee { get; }

        public BuyController(DataContext context, IUserService userService)
        {
            this.context = context;

            _userService = userService;

            UserAccount = context.UserAccounts
                .Where(u => u.Username == _userService.GetMyName())
                .Include(u => u.Employee)
                .ThenInclude(e => e.UserRole)
                .FirstOrDefault();

            Employee = UserAccount.Employee;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Buy>> GetById(int id)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanViewbuy != 1) return Forbid();

            var buy = context.Buys.Where(buy => buy.BuyId == id)
               .Include(buy => buy.RequestedBy)
               .Include(buy => buy.CheckedBy)
               .Include(buy => buy.ApprovedBy)
               .Include(buy => buy.BuyItems)
               .ThenInclude(buyItem => buyItem.Item)
               .ThenInclude(item => item.Material)
               .Include(buy => buy.BuyItems)
               .ThenInclude(buyItem => buyItem.Item.Equipment)
               .FirstOrDefault();

            if (buy == null) return NotFound("buy Not Found.");

            return Ok(buy);
        }

        [HttpGet]
        public async Task<ActionResult<List<Buy>>> Get()
        {
            var buys = await context.Buys
                .OrderByDescending(buy => buy.BuyId)
                .ToListAsync();

            return Ok(buys);
        }

        [HttpPost("request")]
        public async Task<ActionResult<List<Buy>>> Post(CreateBuyDTO buyDTO)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanRequestBuy != 1) return Forbid();

            Buy buy = new();
            buy.RequestedById = Employee.EmployeeId;

            ICollection<BuyItem> buyItems = new List<BuyItem>();

            foreach (var requestItem in buyDTO.BuyItems)
            {
                BuyItem buyItem = new();
                buyItem.ItemId = requestItem.ItemId;
                buyItem.QtyRequested = requestItem.QtyRequested;
                buyItem.RequestRemark = requestItem.RequestRemark;

                var itemTemp = context.Items.Where(item => item.ItemId == requestItem.ItemId).
                    Include(i => i.Equipment).
                    Include(i => i.Material).
                    FirstOrDefault();

                if (itemTemp == null) return NotFound($"Buy Item with Id {requestItem.ItemId} Not Found");

                if (itemTemp.Type != ITEMTYPE.MATERIAL) return BadRequest($"Buy Item with Id {requestItem.ItemId} Is Not Type of Material");

                buyItem.Cost = itemTemp.Material.Cost;
                buyItem.TotalCost = itemTemp.Material.Cost * buyItem.QtyRequested;

                buyItems.Add(buyItem);

                buy.TotalBuyCost += buyItem.TotalCost;
            }

            buy.BuyItems = buyItems;

            context.Buys.Add(buy);
            await context.SaveChangesAsync();

            return Ok(buy.BuyId);

        }

        [HttpPost("check")]
        public async Task<ActionResult<Buy>> Check(CheckBuyDTO checkDTO)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanCheckBuy != 1) return Forbid();

            var buy = await context.Buys.FindAsync(checkDTO.BuyId);

            if (buy == null) return NotFound($"Buy request with Id {checkDTO.BuyId} Not Found");

            buy.CheckDate = DateTime.Now;
            buy.CheckedById = Employee.EmployeeId;
            buy.Status = BUYSTATUS.CHECKED;

            await context.SaveChangesAsync();

            return Ok(buy);
        }

        [HttpPost("queue")]
        public async Task<ActionResult<Buy>> Queue(QueueBuyDTO queueDTO)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanCheckBuy != 1) return Forbid();

            var buy = context.Buys.Where(buy => buy.BuyId == queueDTO.BuyId)
                .Include(buy => buy.BuyItems)
                .ThenInclude(buyItem => buyItem.Item)
                .FirstOrDefault();

            if (buy == null) return NotFound($"Buy request with Id {queueDTO.BuyId} Not Found");

            var purchase = context.Purchases
               .Where(purchase => purchase.Status == PURCHASESTATUS.QUEUED && purchase.ReceivingSiteId == Employee.EmployeeSiteId)
               .Include(purchase => purchase.PurchaseItems)
               .Include(purchase => purchase.PurchaseItemEmployees)
               .FirstOrDefault();

            ICollection<PurchaseItem>? purchaseItems;
            ICollection<PurchaseItemEmployee>? purchaseItemEmployees;

            if (purchase == null)
            {
                purchase = new Purchase
                {
                    Status = PURCHASESTATUS.QUEUED,
                    RequestedById = Employee.EmployeeId,
                    ReceivingSiteId = (int)Employee.EmployeeSiteId
                };

                purchaseItems = new List<PurchaseItem>();
                purchase.PurchaseItems = purchaseItems;

                purchaseItemEmployees = new List<PurchaseItemEmployee>();
                purchase.PurchaseItemEmployees = purchaseItemEmployees;
            }
            else
            {
                purchaseItems = purchase.PurchaseItems;
                purchaseItemEmployees = purchase.PurchaseItemEmployees;
            }

            foreach (var buyItem in buy.BuyItems)
            {
                var purchaseItemEmployee = purchase.PurchaseItemEmployees
                    .Where(pItem => pItem.PurchaseId == purchase.PurchaseId && pItem.ItemId == buyItem.ItemId && pItem.RequestedById == buy.RequestedById)
                    .FirstOrDefault();

                if (purchaseItemEmployee != null)
                {
                    purchaseItemEmployee.QtyRequested += buyItem.QtyRequested;
                }
                else
                {
                    purchaseItemEmployee = new PurchaseItemEmployee
                    {
                        ItemId = buyItem.ItemId,
                        RequestedById = buy.RequestedById,
                        QtyRequested = buyItem.QtyRequested,
                        RequestRemark = buyItem.RequestRemark
                    };
                }

                if (purchaseItemEmployee.QtyRequested == buyItem.QtyRequested)
                {
                    purchaseItemEmployees.Add(purchaseItemEmployee);

                }

                var purchaseItem = purchase.PurchaseItems
                    .Where(pItem => pItem.PurchaseId == purchase.PurchaseId && pItem.ItemId == buyItem.ItemId)
                    .FirstOrDefault();

                if (purchaseItem != null)
                {
                    purchaseItem.QtyRequested += buyItem.QtyRequested;
                }
                else
                {
                    purchaseItem = new PurchaseItem
                    {
                        ItemId = buyItem.ItemId,
                        QtyRequested = buyItem.QtyRequested
                    };

                    var itemTemp = context.Items.Where(item => item.ItemId == buyItem.ItemId).
                        Include(i => i.Equipment).
                        Include(i => i.Material).
                        FirstOrDefault();

                    if (itemTemp == null) return NotFound($"Item with Id {buyItem.ItemId} Not Found");

                    if (itemTemp.Type == ITEMTYPE.MATERIAL)
                    {
                        purchaseItem.Cost = itemTemp.Material.Cost;
                    }
                }

                purchaseItem.TotalCost = purchaseItem.Cost * purchaseItem.QtyRequested;

                if (purchaseItem.QtyRequested == buyItem.QtyRequested)
                {
                    purchaseItems.Add(purchaseItem);

                }
            }

            purchase.PurchaseItems = purchaseItems;
            purchase.PurchaseItemEmployees = purchaseItemEmployees;

            buy.CheckDate = DateTime.Now;
            buy.CheckedById = Employee.EmployeeId;
            buy.Status = BUYSTATUS.QUEUED;

            if (purchase.PurchaseId == 0)
            {
                context.Purchases.Add(purchase);
            }

            purchase.TotalPurchaseCost = 0;
            foreach (var purchaseItem in purchase.PurchaseItems)
            {
                purchase.TotalPurchaseCost += purchaseItem.TotalCost;
            }

            await context.SaveChangesAsync();

            buy.PurchaseId = purchase.PurchaseId;

            await context.SaveChangesAsync();

            return Ok(buy);
        }

        [HttpPost("approve")]
        public async Task<ActionResult<Buy>> Approve(ApproveBuyDTO approveDTO)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanApproveBuy != 1) return Forbid();

            var buy = context.Buys
                 .Where(b => b.BuyId == approveDTO.BuyId)
                 .Include(b => b.BuyItems)
                 .ThenInclude(bItem => bItem.Item)
                 .FirstOrDefault();
            if (buy == null) return NotFound("Request Not Found.");

            buy.ApproveDate = DateTime.Now;
            buy.ApprovedById = Employee.EmployeeId;

            foreach (var requestItem in approveDTO.BuyItems)
            {
                var buyItem = buy.BuyItems.Where(b => b.ItemId == requestItem.ItemId).FirstOrDefault();

                if (buyItem == null) return NotFound($"Buy Item with Id {requestItem.ItemId} Not Found");

                buyItem.QtyApproved = requestItem.QtyApproved;
                buyItem.ApproveRemark = requestItem.ApproveRemark;

                buyItem.TotalCost = buyItem.Cost * buyItem.QtyApproved;
            }

            buy.TotalBuyCost = 0;

            foreach (var purchaseItem in buy.BuyItems)
            {
                buy.TotalBuyCost += purchaseItem.TotalCost;
            }

            buy.Status = BUYSTATUS.APPROVED;

            await context.SaveChangesAsync();

            return Ok(buy);
        }

        [HttpPost("decline")]
        public async Task<ActionResult<Buy>> Decline(ApproveBuyDTO approveDTO)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanApprovePurchase != 1) return Forbid();

            var buy = context.Buys
                .Where(b => b.BuyId == approveDTO.BuyId)
                .Include(b => b.BuyItems)
                .ThenInclude(bItem => bItem.Item)
                .FirstOrDefault();

            if (buy == null) return NotFound("Buy Not Found.");

            buy.ApproveDate = DateTime.Now;
            buy.ApprovedById = Employee.EmployeeId;

            foreach (var buyItem in buy.BuyItems)
            {
                buyItem.QtyApproved = 0;

                buyItem.TotalCost = 0;
            }

            buy.Status = BUYSTATUS.DECLINED;

            await context.SaveChangesAsync();

            return Ok(buy);
        }

        [HttpPost("confirm")]
        public async Task<ActionResult<Buy>> Confirm(ConfirmBuyDTO confirmDTO)
        {
            var buy = context.Buys
                 .Where(b => b.BuyId == confirmDTO.BuyId)
                 .Include(b => b.BuyItems)
                 .ThenInclude(bItem => bItem.Item)
                 .FirstOrDefault();
            if (buy == null) return NotFound("Request Not Found.");

            buy.BuyDate = DateTime.Now;

            foreach (var requestItem in confirmDTO.BuyItems)
            {
                var buyItem = buy.BuyItems.Where(b => b.ItemId == requestItem.ItemId).FirstOrDefault();

                if (buyItem == null) return NotFound($"Buy Item with Id {requestItem.ItemId} Not Found");

                buyItem.QtyBought = requestItem.QtyBought;
                buyItem.BuyRemark = requestItem.BuyRemark;

                buyItem.TotalCost = buyItem.Cost * buyItem.QtyBought;
            }

            buy.TotalBuyCost = 0;

            foreach (var purchaseItem in buy.BuyItems)
            {
                buy.TotalBuyCost += purchaseItem.TotalCost;
            }

            buy.Status = BUYSTATUS.BOUGHT;

            await context.SaveChangesAsync();

            return Ok(buy);
        }
    }
}
