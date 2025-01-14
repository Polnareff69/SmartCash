using AutoMapper;
using SmartCashAPI.DTOs;
using SmartCashAPI.DTOs.CategoryDtos;
using SmartCashAPI.Models;

namespace SmartCashAPI.Services
{
    public class AutoMapper: Profile
    {
        public AutoMapper()
        {
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
