using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace DBApp
{


    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database connection string here
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DBApp;Trusted_Connection=True;"); // Replace with your actual connection string
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd(); // Set Employee Id to auto-generate

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department) // Specify relationship
                .WithMany(d => d.Employees) // Specify inverse relationship
                .HasForeignKey(e => e.DepartmentId); // Set foreign key
        }
    }

}
