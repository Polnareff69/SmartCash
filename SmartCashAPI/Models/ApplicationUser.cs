using Microsoft.AspNetCore.Identity;

namespace SmartCashAPI.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public new Guid Id { get; set; } = Guid.NewGuid();
        public List<Category>? Categories { get; set; }

        public List<Transaction>? Transactions { get; set; }
    }
}
