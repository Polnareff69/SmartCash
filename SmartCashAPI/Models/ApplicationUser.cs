using Microsoft.AspNetCore.Identity;

namespace SmartCashAPI.Models
{
    public class ApplicationUser: IdentityUser
    {
        public List<Category>? Categories { get; set; }

        public List<Transaction>? Transactions { get; set; }


    }
}
