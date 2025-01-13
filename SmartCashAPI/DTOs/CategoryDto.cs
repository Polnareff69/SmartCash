using static SmartCashAPI.Common.Enums;

namespace SmartCashAPI.DTOs
{
    public class CategoryDto
    {
        public String? Id { get; set; }
        public string? Name { get; set; }
        public CategoryType? Type { get; set; }
        public List<TransactionDto>? Transactions { get; set; }

    }
}
