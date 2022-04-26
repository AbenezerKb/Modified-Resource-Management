using ERP.Context;
using ERP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : Controller
    {
        private readonly DataContext context;

        public UserRoleController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserRole>>> Get()
        {
            var roles = await context.UserRoles.ToListAsync();
            
            return Ok(roles);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("add")]
        public async Task<ActionResult<UserRole>> AddNew(UserRole role)
        {
            UserRole userRole = new();
            userRole.Role = role.Role;

            userRole.CanRequestBorrow = role.CanRequestBorrow;
            userRole.CanApproveBorrow = role.CanApproveBorrow;
            userRole.CanHandBorrow = role.CanHandBorrow;
            userRole.CanReturnBorrow = role.CanReturnBorrow;
            userRole.CanViewBorrow = role.CanViewBorrow;

            userRole.CanRequestBuy = role.CanRequestBuy;
            userRole.CanCheckBuy = role.CanCheckBuy;
            userRole.CanApproveBuy = role.CanApproveBuy;
            userRole.CanConfirmBuy = role.CanConfirmBuy;
            userRole.CanViewBuy = role.CanViewBuy;

            userRole.CanReceive = role.CanReceive;
            userRole.CanApproveReceive = role.CanApproveReceive;
            userRole.CanViewReceive = role.CanViewReceive;

            userRole.CanApproveUser = role.CanApproveUser;
            userRole.CanDeleteUser = role.CanDeleteUser;

            userRole.CanRequestPurchase = role.CanRequestPurchase;
            userRole.CanCheckPurchase = role.CanCheckPurchase;
            userRole.CanApprovePurchase = role.CanApprovePurchase;
            userRole.CanConfirmPurchase = role.CanConfirmPurchase;
            userRole.CanViewPurchase = role.CanViewPurchase;

            userRole.CanFixMaintenance = role.CanFixMaintenance;
            userRole.CanApproveMaintenance = role.CanApproveMaintenance;
            userRole.CanRequestMaintenance = role.CanRequestMaintenance;
            userRole.CanViewMaintenance = role.CanViewMaintenance;

            userRole.CanRequestIssue = role.CanRequestIssue;
            userRole.CanApproveIssue = role.CanApproveIssue;
            userRole.CanHandIssue = role.CanHandIssue;
            userRole.CanViewIssue = role.CanViewIssue;

            userRole.CanRequestTransfer = role.CanRequestTransfer;
            userRole.CanApproveTransfer = role.CanApproveTransfer;
            userRole.CanReceiveTransfer = role.CanReceiveTransfer;
            userRole.CanSendTransfer = role.CanSendTransfer;
            userRole.CanViewTransfer = role.CanViewTransfer;

            userRole.CanGetStockNotification = role.CanGetStockNotification;

            context.UserRoles.Add(userRole);

            await context.SaveChangesAsync();

            return Ok(userRole);
        }
    }
}
