using System.Security.Cryptography;
using ERP.Context;
using ERP.DTOs.User;
using ERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.Helpers
{
    public class SampleDataHelper
    {
        DataContext context;
        public SampleDataHelper(DataContext context)
        {
            this.context = context;
        }
        public async Task InitData()
        {
            if (await context.Employees.AnyAsync(e => e.Position == "Manager" || e.Position == "ProjectManager"))
            {
                Console.WriteLine("Data Already Initialize! Initialization Ignore!");
                return;

            }
            await SeedRolesAsync();

            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("Initializing Data");
            var roles = await context.UserRoles.ToListAsync();

            Site site = (await InitSitesAsync()).First();

            //Create Manager
            await CreateEmployeeAsync(new RegisterUserDTO
            {
                FName = "AlexManager",
                LName = "Woreku",
                MName = "M",
                Position = "Manager",
                EmployeeSiteId = site.SiteId,
                UserRoleId = roles.First(r => r.Role.Equals("Manager")).RoleId,
                Password = "alex1234"
            });
            //Create Admin
            await CreateEmployeeAsync(new RegisterUserDTO
            {
                FName = "AlexAdmin",
                LName = "Woreku",
                MName = "A",
                Position = "Adminstrator",
                EmployeeSiteId = site.SiteId,
                UserRoleId = roles.First(r => r.Role.Equals("Employee") && r.IsAdmin).RoleId,
                Password = "alex1234"
            });
            //Create OfficeEngineer
            await CreateEmployeeAsync(new RegisterUserDTO
            {
                FName = "AlexOfficeEngineer",
                LName = "Woreku",
                MName = "OE",
                Position = "OfficeEngineer",
                EmployeeSiteId = site.SiteId,
                UserRoleId = roles.First(r => r.Role.Equals("OfficeEngineer")).RoleId,
                Password = "alex1234"
            });
            //Create PM
            await CreateEmployeeAsync(new RegisterUserDTO
            {
                FName = "AlexProjectManager",
                LName = "Woreku",
                MName = "PM",
                Position = "ProjectManager",
                EmployeeSiteId = site.SiteId,
                UserRoleId = roles.First(r => r.Role.Equals("ProjectManager")).RoleId,
                Password = "alex1234"
            });
            // Create SiteEngineer
            await CreateEmployeeAsync(new RegisterUserDTO
            {
                FName = "AlexSiteEngineer",
                LName = "Woreku",
                MName = "SE",
                Position = "SiteEngineer",
                EmployeeSiteId = site.SiteId,
                UserRoleId = roles.First(r => r.Role.Equals("SiteEngineer")).RoleId,
                Password = "alex1234"
            });
            // Create Coordinator
            await CreateEmployeeAsync(new RegisterUserDTO
            {
                FName = "AlexCoordinator",
                LName = "Woreku",
                MName = "C",
                Position = "Coordinator",
                EmployeeSiteId = site.SiteId,
                UserRoleId = roles.First(r => r.Role.Equals("Coordinator")).RoleId,
                Password = "alex1234"
            });

            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("Initialization Completed");
        }
        async Task SeedRolesAsync()
        {
            if (await context.UserRoles.AnyAsync(e => e.Role == "Manager" || e.Role == "ProjectManager"))
            {
                return;
            }
            await context.UserRoles.AddRangeAsync(
                new UserRole
                {
                    Role = "Employee",
                    IsAdmin = true,
                    CanEditUser = true,

                    CanRequestPurchase = true,

                    CanApprovePurchase = true,

                    CanCheckPurchase = true,


                    CanViewPurchase = true,

                    CanConfirmPurchase = true,


                    CanViewBulkPurchase = true,

                    CanRequestBulkPurchase = true,

                    CanApproveBulkPurchase = true,

                    CanConfirmBulkPurchase = true,

                    CanRequestBuy = true,

                    CanApproveBuy = true,

                    CanCheckBuy = true,

                    CanViewBuy = true,

                    CanConfirmBuy = true,

                    CanReceive = true,

                    CanApproveReceive = true,


                    CanViewReceive = true,

                    CanRequestIssue = true,

                    CanApproveIssue = true,


                    CanHandIssue = true,


                    CanViewIssue = true,


                    CanRequestBorrow = true,

                    CanApproveBorrow = true,


                    CanHandBorrow = true,


                    CanViewBorrow = true,

                    CanReturnBorrow = true,

                    CanRequestTransfer = true,

                    CanApproveTransfer = true,

                    CanSendTransfer = true,


                    CanReceiveTransfer = true,

                    CanViewTransfer = true,

                    CanRequestMaintenance = true,

                    CanApproveMaintenance = true,


                    CanFixMaintenance = true,


                    CanViewMaintenance = true,

                    CanGetStockNotification = true


                },
               new UserRole
               {
                   Role = "OfficeEngineer",
               },
               new UserRole
               {
                   Role = "ProjectManager",
               },
               new UserRole
               {
                   Role = "Manager",
               },
               new UserRole
               {

                   Role = "SiteEngineer",
               },
               new UserRole
               {

                   Role = "Coordinator",
               }
               );
            await context.SaveChangesAsync();

        }

        private async Task<List<Site>> InitSitesAsync()
        {
            context.Sites.Add(new Site
            {
                Name = "Addis Ababa Site",
                Location = "Bole, Addis Ababa, Ethiopia",
                PettyCashLimit = 4500
            });
            await context.SaveChangesAsync();
            return await context.Sites.ToListAsync();
        }

        private async Task CreateEmployeeAsync(RegisterUserDTO registerDTO)
        {

            Employee employee = new();
            employee.FName = registerDTO.FName;
            employee.MName = registerDTO.MName;
            employee.LName = registerDTO.LName;
            employee.Position = registerDTO.Position;
            employee.EmployeeSiteId = registerDTO.EmployeeSiteId;
            employee.UserRoleId = registerDTO.UserRoleId;
            employee.Status = 1;

            context.Employees.Add(employee);

            await context.SaveChangesAsync();
            await createUserAccountAsync(registerDTO, employee.EmployeeId);
        }
        private async Task createUserAccountAsync(RegisterUserDTO registerDTO, int employeeId)
        {
            UserAccount userAccount = new();
            SecurityHelper.CreatePasswordHash(registerDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            userAccount.Username = registerDTO.FName + employeeId;
            userAccount.PasswordHash = passwordHash;
            userAccount.PasswordSalt = passwordSalt;
            userAccount.EmployeeId = employeeId;

            context.UserAccounts.Add(userAccount);
            await context.SaveChangesAsync();
            Console.WriteLine($"Username of {registerDTO.Position} is {userAccount.Username}");

        }


    }

}