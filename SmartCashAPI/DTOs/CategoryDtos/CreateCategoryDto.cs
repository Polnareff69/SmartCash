using static SmartCashAPI.Common.Enums;
using static SmartCashAPI.DTOs.CategoryDto;

namespace SmartCashAPI.DTOs.CategoryDtos
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = null!;
        public CategoryType? Type { get; set; }
    }
}
