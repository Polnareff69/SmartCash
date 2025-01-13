using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartCashAPI.Interfaces;
namespace SmartCashAPI.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasOne(c => c.User).WithMany(u => u.Categories).HasForeignKey(c => c.UserId);
        modelBuilder.Entity<Transaction>().HasOne(t => t.Category).WithMany(t => t.Transactions).HasForeignKey(t => t.CategoryId).OnDelete(DeleteBehavior.Cascade);

    }


    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
}

