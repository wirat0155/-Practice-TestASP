using TestASP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestASP.DataAccess.Data
{
    public class ApplicationDBContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        public DbSet<Department> tbl_department { get; set; }
        public DbSet<Employee> tbl_employee { get; set; }
        public DbSet<LeaveRecord> tbl_leave_record { get; set; }
        public DbSet<LeaveType> tbl_leave_type { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = Guid.Parse("AEF862CE-4192-4F69-ABEB-126C92E66EA4"), Name = "HR" },
                new Department { Id = Guid.Parse("2BBBA09D-6199-4F74-936B-F5723EEAEF3C"), Name = "Engineer" }
                );

            modelBuilder.Entity<LeaveType>().HasData(
                new LeaveType { Id = Guid.Parse("941D7B8D-4E68-493F-9602-7B699BC5282A"), Name = "Personal Leave" },
                new LeaveType { Id = Guid.Parse("FE5EA00A-B221-4EFB-BBE5-93B2644821AD"), Name = "Sick Leave" }
                );
        }
    }
}
