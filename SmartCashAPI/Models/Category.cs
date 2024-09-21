using Microsoft.AspNetCore.Identity;

namespace SmartCashAPI.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public CategoryType? Type { get; set; }
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
        public List<Transaction>? Transactions { get; set; }

        public enum CategoryType { Income, Outcome }
    }

    
}
