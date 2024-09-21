using Microsoft.AspNetCore.Identity;

namespace SmartCashAPI.Models
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public IdentityUser? User { get; set; }
        public string? UserId { get; set; }
        public Category? Category { get; set; }
        public string? CategoryId { get; set; }
        public string? Description { get; set; }

        public double? Amount { get; set; }

    }
}
