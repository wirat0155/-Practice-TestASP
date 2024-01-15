using TestASP.DataAccess.Data;
using TestASP.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestASP.Models;

namespace TestASP.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDBContext _dbContext;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDBContext dbContext
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            //migration if they are not applied
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch ( Exception ex ) { }

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_HR).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_HR)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
            }

            IdentityUser user = _dbContext.Users.FirstOrDefault(u => u.Email == "hr@gmail.com");
            if (user == null) {
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "hr@gmail.com",
                    Email = "hr@gmail.com",
                    PhoneNumber = "0830817916",
                }, "Hr@12345").GetAwaiter().GetResult();

                user = _dbContext.Users.FirstOrDefault(u => u.Email == "hr@gmail.com");
                _userManager.AddToRoleAsync(user, SD.Role_HR).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, SD.Role_Employee).GetAwaiter().GetResult();

                Employee employee = new Employee()
                {
                    Id = Guid.Parse("1C47A13A-B35A-4501-BF1C-76D5501F9B83"),
                    UserId = user.Id,
                    FirstName = "Weera",
                    LastName = "Ouboun",
                    EmployeeCode = "60011001",
                    DepartmentId = Guid.Parse("AEF862CE-4192-4F69-ABEB-126C92E66EA4")
                };
                _dbContext.tbl_employee.Add(employee);
                _dbContext.SaveChanges();
            }

            user = _dbContext.Users.FirstOrDefault(u => u.Email == "employee@gmail.com");
            if (user == null)
            {
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "employee@gmail.com",
                    Email = "employee@gmail.com",
                    PhoneNumber = "0830817916",
                }, "Employee@12345").GetAwaiter().GetResult();

                user = _dbContext.Users.FirstOrDefault(u => u.Email == "employee@gmail.com");
                _userManager.AddToRoleAsync(user, SD.Role_Employee).GetAwaiter().GetResult();

                Employee employee = new Employee()
                {
                    Id = Guid.Parse("C5535F20-4DB1-45AA-9D91-03F449863F7F"),
                    UserId = user.Id,
                    FirstName = "Wirat",
                    LastName = "Sakorn",
                    EmployeeCode = "62160109",
                    DepartmentId = Guid.Parse("2BBBA09D-6199-4F74-936B-F5723EEAEF3C")
                };
                _dbContext.tbl_employee.Add(employee);
                _dbContext.SaveChanges();
            }
            return;
        }
    }
}
