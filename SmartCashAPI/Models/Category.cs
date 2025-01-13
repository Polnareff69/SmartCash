using Microsoft.AspNetCore.Identity;
using static SmartCashAPI.Common.Enums;

namespace SmartCashAPI.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string? Name { get; set; }
        public CategoryType? Type { get; set; }
        public Guid UserId { get; set; }
        public List<Transaction>? Transactions { get; set; }

        public virtual ApplicationUser? User { get; set; }
    }
}
