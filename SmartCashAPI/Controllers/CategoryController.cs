﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCashAPI.DTOs;
using SmartCashAPI.DTOs.CategoryDtos;
using SmartCashAPI.Interfaces;
using SmartCashAPI.Models;

namespace SmartCashAPI.Controllers
{
    [Route("api/[controller]")]


    public class CategoryController: ControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryController(IApplicationDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllUserCategories()
        {
            return Ok(_mapper.Map<List<CategoryDto>>(await _context.Categories.ToListAsync()));
        }

        [HttpPost("CreateCategory")]

        public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto category)
        {
            if (category == null) throw new Exception("Cannot create null category");

            Category newCategory = _mapper.Map<Category>(category);

            await _context.Categories.AddAsync(newCategory);

            await _context.SaveChangesAsync();

            return Ok("Category successfully created");
        }
    }
}
