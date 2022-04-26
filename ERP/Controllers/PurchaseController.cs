using ERP.Context;
using ERP.DTOs;
using ERP.DTOs.Purchase;
using ERP.Models;
using ERP.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using PurchaseItem = ERP.Models.PurchaseItem;

namespace ERP.Controllers
{

    [Route("/api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class PurchaseController : Controller
    {
        private readonly DataContext context;
        private readonly IUserService _userService;

        public UserAccount? UserAccount { get; }

        public Employee Employee { get; }

        public PurchaseController(DataContext context, IUserService userService)
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
        public async Task<ActionResult<Purchase>> Get(int id)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanViewPurchase != 1) return Forbid();

            var purchase = context.Purchases.Where(purchase => purchase.PurchaseId == id)
               .Include(purchase => purchase.RequestedBy)
               .Include(purchase => purchase.CheckedBy)
               .Include(purchase => purchase.ApprovedBy)
               .Include(purchase => purchase.ReceivingSite)
               .Include(purchase => purchase.PurchaseItems)
               .ThenInclude(purchaseItem => purchaseItem.Item)
               .ThenInclude(item => item.Material)
               .Include(purchase => purchase.PurchaseItems)
               .ThenInclude(purchaseItem => purchaseItem.Item.Equipment)
               .FirstOrDefault();

            if (purchase == null) return NotFound("Purchase Not Found.");

            return Ok(purchase);
        }

        [HttpGet]
        public async Task<ActionResult<List<Purchase>>> Get()
        {
            //if (UserAccount != null && UserAccount.UserRole.CanViewPurchase != 1) return Forbid();

            var purchases = await context.Purchases
                .Include(purchase => purchase.ReceivingSite)
                .OrderByDescending(purchase => purchase.PurchaseId)
                .ToListAsync();

            return Ok(purchases);
        }

        [HttpPost("request")]
        public async Task<ActionResult<List<Purchase>>> Post(CreatePurchaseDTO purchaseDTO)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanRequestPurchase != 1) return Forbid();

            Purchase purchase = new();
            purchase.RequestedById = Employee.EmployeeId;
            purchase.ReceivingSiteId = purchaseDTO.ReceivingSiteId;

            ICollection<PurchaseItem> purchaseItems = new List<PurchaseItem>();
            ICollection<PurchaseItemEmployee> purchaseItemEmployees = new List<PurchaseItemEmployee>();
            
            foreach (var requestItem in purchaseDTO.PurchaseItems)
            {
                PurchaseItem purchaseItem = new();
                purchaseItem.ItemId = requestItem.ItemId;
                purchaseItem.QtyRequested = requestItem.QtyRequested;
                purchaseItem.RequestRemark = requestItem.RequestRemark;

                var itemTemp = context.Items.Where(item => item.ItemId == requestItem.ItemId).
                    Include(i => i.Equipment).
                    Include(i => i.Material).
                    FirstOrDefault();

                if (itemTemp == null) return NotFound($"Purchase Item with Id {requestItem.ItemId} Not Found");

                if (itemTemp.Type == ITEMTYPE.MATERIAL)
                {
                    purchaseItem.Cost = itemTemp.Material.Cost;
                    purchaseItem.TotalCost = itemTemp.Material.Cost * purchaseItem.QtyRequested;
                }
               
                purchaseItems.Add(purchaseItem);

                purchase.TotalPurchaseCost += purchaseItem.TotalCost;
            
                PurchaseItemEmployee purchaseItemEmployee = new();
                purchaseItemEmployee.ItemId = requestItem.ItemId;

                purchaseItemEmployee.RequestedById = Employee.EmployeeId;
                purchaseItemEmployee.QtyRequested = requestItem.QtyRequested;
                purchaseItemEmployee.RequestRemark = requestItem.RequestRemark;
                purchaseItemEmployees.Add(purchaseItemEmployee);

            }

            purchase.PurchaseItems = purchaseItems;
            purchase.PurchaseItemEmployees = purchaseItemEmployees;

            context.Purchases.Add(purchase);

            await context.SaveChangesAsync();

            return Ok(purchase.PurchaseId);
        }

        [HttpPost("approve")]
        public async Task<ActionResult<Purchase>> Approve(ApprovePurchaseDTO approveDTO)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanApprovePurchase != 1) return Forbid();

            var purchase = context.Purchases
                 .Where(t => t.PurchaseId == approveDTO.PurchaseId)
                 .Include(t => t.PurchaseItems)
                 .FirstOrDefault();
            if (purchase == null) return NotFound("Request Not Found.");

            purchase.ApproveDate = DateTime.Now;
            purchase.ApprovedById = Employee.EmployeeId;

            foreach (var requestItem in approveDTO.PurchaseItems)
            {
                var purchaseItem = purchase.PurchaseItems
                    .Where(purchaseItem => purchaseItem.ItemId == requestItem.ItemId)
                    .FirstOrDefault();

                if (purchaseItem == null) return NotFound($"Purchase Item with Id {requestItem.ItemId} Not Found");

                purchaseItem.QtyApproved = requestItem.QtyApproved;
                purchaseItem.TotalCost = purchaseItem.Cost * purchaseItem.QtyApproved;
                purchaseItem.ApproveRemark = requestItem.ApproveRemark;

            }

            purchase.TotalPurchaseCost = 0;
            
            foreach (var purchaseItem in purchase.PurchaseItems)
            {
                purchase.TotalPurchaseCost += purchaseItem.TotalCost;
            }

            purchase.Status = PURCHASESTATUS.APPROVED;

            await context.SaveChangesAsync();

            return Ok(purchase);
        }

        [HttpPost("check")]
        public async Task<ActionResult<Purchase>> Check(CheckPurchaseDTO checkDTO)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanCheckPurchase != 1) return Forbid();

            var purchase = await context.Purchases.FindAsync(checkDTO.PurchaseId);

            if (purchase == null) return NotFound("Request Not Found.");

            purchase.CheckDate = DateTime.Now;
            purchase.CheckedById = Employee.EmployeeId;
            purchase.Status = PURCHASESTATUS.CHECKED;

            await context.SaveChangesAsync();

            return Ok(purchase);
        }

        [HttpPost("requestqueued")]
        public async Task<ActionResult<Purchase>> RequestQueuedPurchase(CheckPurchaseDTO checkDTO)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanCheckPurchase != 1) return Forbid();

            var purchase = await context.Purchases.FindAsync(checkDTO.PurchaseId);

            if (purchase == null) return NotFound("Request Not Found.");

            purchase.Status = PURCHASESTATUS.REQUESTED;

            await context.SaveChangesAsync();

            return Ok(purchase);
        }

        [HttpPost("decline")]
        public async Task<ActionResult<Purchase>> Decline(ApprovePurchaseDTO approveDTO)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanApprovePurchase != 1) return Forbid();

            var purchase = context.Purchases
                  .Where(t => t.PurchaseId == approveDTO.PurchaseId)
                  .Include(t => t.PurchaseItems)
                  .FirstOrDefault();

            if (purchase == null) return NotFound("Purchase Not Found.");

            purchase.ApproveDate = DateTime.Now;
            purchase.ApprovedById = Employee.EmployeeId;
            purchase.TotalPurchaseCost = 0;

            foreach (var purchaseItem in purchase.PurchaseItems)
            {
                purchaseItem.QtyApproved = 0;

                purchaseItem.TotalCost = 0;
            }

            purchase.Status = PURCHASESTATUS.DECLINED;

            await context.SaveChangesAsync();

            return Ok(purchase);
        }

        [HttpPost("confirm")]
        public async Task<ActionResult<Purchase>> Confirm(ConfirmPurchaseDTO confirmDTO)
        {
            //if (UserAccount != null && UserAccount.UserRole.CanApprovePurchase != 1) return Forbid();

            var purchase = context.Purchases
                 .Where(t => t.PurchaseId == confirmDTO.PurchaseId)
                 .Include(t => t.PurchaseItems)
                 .FirstOrDefault();

            if (purchase == null) return NotFound("Request Not Found.");

            foreach (var requestItem in confirmDTO.PurchaseItems)
            {
                var purchaseItem = purchase.PurchaseItems
                    .Where(purchaseItem => purchaseItem.ItemId == requestItem.ItemId)
                    .FirstOrDefault();

                if (purchaseItem == null) return NotFound($"Purchase Item with Id {requestItem.ItemId} Not Found");

                purchaseItem.QtyPurchased = requestItem.QtyPurchased;
                purchaseItem.PurchaseRemark = requestItem.PurchaseRemark;
                purchaseItem.TotalCost = purchaseItem.Cost * purchaseItem.QtyPurchased;

            }

            purchase.TotalPurchaseCost = 0;
            foreach (var purchaseItem in purchase.PurchaseItems)
            {
                purchase.TotalPurchaseCost += purchaseItem.TotalCost;
            }

            purchase.Status = PURCHASESTATUS.PURCHASED;

            await context.SaveChangesAsync();

            //Add receive
            Receive receive = new();
            receive.PurchaseId = purchase.PurchaseId;
            receive.ReceivingSiteId = purchase.ReceivingSiteId;
            ICollection<ReceiveItem> receiveItems = new List<ReceiveItem>();

            foreach (var requestItem in purchase.PurchaseItems)
            {
                ReceiveItem receiveItem = new();
                receiveItem.ItemId = requestItem.ItemId;
                receiveItem.QtyPurchased = requestItem.QtyPurchased;

                receiveItems.Add(receiveItem);
            }

            receive.ReceiveItems = receiveItems;
            receive.Status = RECEIVESTATUS.PURCHASED;

            context.Receives.Add(receive);

            await context.SaveChangesAsync();

            return Ok(purchase);
        }
    }
}