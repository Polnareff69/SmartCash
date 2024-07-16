using Microsoft.AspNetCore.Identity;
using SmartCashAPI.Models;

namespace SmartCashAPI.DTOs
{
    public class TransactionDto
    {
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public double? Amount { get; set; }

        public string? Description { get; set; }
    }
}
