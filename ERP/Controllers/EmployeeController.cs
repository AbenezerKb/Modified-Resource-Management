using ERP.Context;
using ERP.DTOs.User;
using ERP.Models;
using ERP.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using UserAccount = ERP.Models.UserAccount;

namespace ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly DataContext context;

        public EmployeeController(DataContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var employee = context.Employees
                .Where(employee => employee.EmployeeId == id)
                .FirstOrDefault();

            if (employee == null) return NotFound("Employee Not Found.");

            return Ok(employee);
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            var employees = await context.Employees
                .Include(employee => employee.UserRole)
                .ToListAsync();

            return Ok(employees);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("changesite")]
        public async Task<ActionResult<Employee>> ChangeEmployeeSite(UpdateUserDTO userDTO)
        {
            var employee = context.Employees
                .Where(e => e.EmployeeId == userDTO.EmployeeId)
                .FirstOrDefault();

            if (employee == null) return NotFound("Employee Not Found.");

            employee.EmployeeSiteId = userDTO.EmployeeSiteId;

            await context.SaveChangesAsync();

            return Ok(employee);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("changerole")]
        public async Task<ActionResult<Employee>> ChangeEmployeeRole(UpdateUserDTO userDTO)
        {
            var employee = context.Employees
                .Where(e => e.EmployeeId == userDTO.EmployeeId)
                .FirstOrDefault();

            if (employee == null) return NotFound("Employee Not Found.");

            employee.UserRoleId = userDTO.UserRoleId;

            await context.SaveChangesAsync();

            return Ok(employee);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("update")]
        public async Task<ActionResult<Employee>> UpdateEmployee(UpdateUserDTO userDTO)
        {
            var employee = context.Employees
                .Where(e => e.EmployeeId == userDTO.EmployeeId)
                .FirstOrDefault();

            if (employee == null) return NotFound("Employee Not Found.");

            employee.Status = userDTO.Status;
            employee.FName = userDTO.FName;
            employee.MName = userDTO.MName;
            employee.LName = userDTO.LName;
            employee.Position = userDTO.Position;
            employee.EmployeeSiteId = userDTO.EmployeeSiteId;
            employee.UserRoleId = userDTO.UserRoleId;

            await context.SaveChangesAsync();

            return Ok(employee);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("approve")]
        public async Task<ActionResult<UserAccount>> Approve(ApproveUserDTO approveDTO)
        {
            var employee = context.Employees.Where(employee => employee.EmployeeId == approveDTO.EmployeeId)
                .FirstOrDefault();

            if (employee == null) return NotFound("User Not Found.");

            employee.UserRoleId = approveDTO.UserRoleId;

            employee.Status = EMPLOYEESTATUS.APPROVED;

            await context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPost("initialize")]
        public async Task<ActionResult<UserAccount>> CreateFirstUser(InitializeAdminDTO adminDTO)
        {
            var emp = context.Employees
                .FirstOrDefault();

            if (emp != null) return NotFound("An Employee Already Exists");

            UserRole userRole = new();
            userRole.Role = "Admin";

            userRole.CanRequestBorrow = true;
            userRole.CanApproveBorrow = true;
            userRole.CanHandBorrow = true;
            userRole.CanReturnBorrow = true;
            userRole.CanViewBorrow = true;

            userRole.CanRequestBuy = true;
            userRole.CanCheckBuy = true;
            userRole.CanApproveBuy = true;
            userRole.CanConfirmBuy = true;
            userRole.CanViewBuy = true;

            userRole.CanReceive = true;
            userRole.CanApproveReceive = true;
            userRole.CanViewReceive = true;

            userRole.CanEditUser = true;

            userRole.CanRequestPurchase = true;
            userRole.CanCheckPurchase = true;
            userRole.CanApprovePurchase = true;
            userRole.CanConfirmPurchase = true;
            userRole.CanViewPurchase = true;

            userRole.CanRequestBulkPurchase = true;
            userRole.CanApproveBulkPurchase = true;
            userRole.CanConfirmBulkPurchase = true;
            userRole.CanViewBulkPurchase = true;

            userRole.CanFixMaintenance = true;
            userRole.CanApproveMaintenance = true;
            userRole.CanRequestMaintenance = true;
            userRole.CanViewMaintenance = true;

            userRole.CanRequestIssue = true;
            userRole.CanApproveIssue = true;
            userRole.CanHandIssue = true;
            userRole.CanViewIssue = true;

            userRole.CanRequestTransfer = true;
            userRole.CanApproveTransfer = true;
            userRole.CanReceiveTransfer = true;
            userRole.CanSendTransfer = true;
            userRole.CanViewTransfer = true;

            userRole.CanGetStockNotification = true;
            userRole.IsFinance = true;

            context.UserRoles.Add(userRole);

            await context.SaveChangesAsync();


            Employee employee = new();
            employee.FName = "AdmFname";
            employee.MName = "AdmMname";
            employee.LName = "AdmLname";
            employee.Position = "Head";
            employee.UserRoleId = userRole.RoleId;
            employee.Status = 1;

            context.Employees.Add(employee);

            await context.SaveChangesAsync();


            UserAccount userAccount = new();

            CreatePasswordHash(adminDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            userAccount.Username = "admin";
            userAccount.PasswordHash = passwordHash;
            userAccount.PasswordSalt = passwordSalt;
            userAccount.EmployeeId = employee.EmployeeId;

            context.UserAccounts.Add(userAccount);

            await context.SaveChangesAsync();

            return Ok(userAccount);
        }

        private void CreatePasswordHash(string pass, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
            }
        }
    }
}
