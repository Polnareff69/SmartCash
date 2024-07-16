using Microsoft.AspNetCore.Identity;

namespace SmartCashAPI.Models
{
    public class Transaction
    {
        public string? Id { get; set; }
        public IdentityUser? User { get; set; }
        public string? UserId { get; set; }
        public Category? Category { get; set; }
        public string? CategoryId { get; set; }
        public string? Description { get; set; }

        public double? Amount { get; set; }

    }
}
