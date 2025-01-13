namespace SmartCashAPI.DTOs.CategoryDtos
{
    public class ListCategoryDto
    {
        public Guid Ig { get; set; }
        public string Name { get; set; } = null!;
        public string CategoryType { get; set; } = null!;
    }
}
