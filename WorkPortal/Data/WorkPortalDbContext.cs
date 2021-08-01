using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace WorkPortal.Data
{
    public class WorkPortalDbContext : IdentityDbContext<User>
    {
        public WorkPortalDbContext(DbContextOptions<WorkPortalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<AnnualLeave> AnnualLeaves { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Payslip> Payslips { get; set; }

        public DbSet<Shift> Shifts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Address>()
                .HasOne(x=>x.Town)
                .WithMany()
                .HasForeignKey(x=>x.TownId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Employee>()
                .HasOne(d => d.Department)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Employee>()
                .HasOne(d => d.Manager)
                .WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .Entity<Employee>()
            //    .HasOne<User>()
            //    .WithOne()
            //    .HasForeignKey<Employee>(d => d.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Shift>()
                .HasOne(x => x.Location)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Department>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Departments)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
