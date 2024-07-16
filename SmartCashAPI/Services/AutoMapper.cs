using AutoMapper;
using SmartCashAPI.DTOs;
using SmartCashAPI.Models;

namespace SmartCashAPI.Services
{
    public class AutoMapper: Profile
    {
        public AutoMapper()
        {
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
