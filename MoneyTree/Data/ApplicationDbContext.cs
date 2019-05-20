using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyTree.Models;

namespace MoneyTree.Data {

    public class ApplicationDbContext : IdentityDbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<CostCategory> CostCategory { get; set; }
        public DbSet<CostItem> CostItem { get; set; }
        public DbSet<CostPerUnit> CostPerUnit { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectCost> ProjectCost { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public DbSet<CustomProjectCost> CustomProjectCost { get; set; }
    }
}
