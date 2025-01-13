using Microsoft.AspNetCore.Identity;
using static SmartCashAPI.Common.Enums;

namespace SmartCashAPI.Models
{
    public class Category
    {
<<<<<<< HEAD
        public Guid Id { get; set; } = Guid.NewGuid(); 
=======
        public Guid Id { get; set; } = Guid.NewGuid();
>>>>>>> f6020d2b617864bab706401a3ef7a3bef864c6e0
        public string? Name { get; set; }
        public CategoryType? Type { get; set; }
        public Guid UserId { get; set; }
        public List<Transaction>? Transactions { get; set; }

        public virtual ApplicationUser? User { get; set; }
    }
}
