using Microsoft.EntityFrameworkCore;
using SmartCashAPI.Models;

namespace SmartCashAPI.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
