using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SmartCashAPI.DTOs;
using SmartCashAPI.Interfaces;
using SmartCashAPI.Models;

namespace SmartCashAPI.Controllers;

[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    IApplicationDbContext _context;
    IMapper _mapper;
    public TransactionController(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("GetAllUserTransactions")]
    public async Task<ActionResult<List<TransactionDto>>> GetAllUserTransactions()
    {
        return _mapper.Map<List<TransactionDto>>(await _context.Transactions.ToListAsync());
    }

    [HttpPost("CreateTransaction")]
    public async Task<ActionResult> CreateTransaction([FromBody] TransactionDto transaction)
    {
        if (transaction == null) return BadRequest("Cannot create null transaction");

        Transaction newTransaction = _mapper.Map<Transaction>(transaction);

        await _context.Transactions.AddAsync(newTransaction);

        await _context.SaveChangesAsync();

        return Ok(newTransaction);
    }

    [HttpPost("CreateMultipleTransactions")]
    public async Task<ActionResult> CreateMultipleTransactions([FromBody] List<TransactionDto> transactions)
    {
        foreach (TransactionDto transaction in transactions)
        {
            Transaction newTransaction = _mapper.Map<Transaction>(transaction);
            await _context.Transactions.AddAsync(newTransaction);
        }

        await _context.SaveChangesAsync();

        return Ok("Transactions created");
    }

}

