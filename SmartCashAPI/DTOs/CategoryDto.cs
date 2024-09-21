
namespace SmartCashAPI.DTOs
{
    public class CategoryDto
    {
        public string? Name { get; set; }
        public CategoryType? Type { get; set; }
        public enum CategoryType { Income, Outcome }
    }
}
