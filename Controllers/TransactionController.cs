using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Dtos.TransactionDtos;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController:ControllerBase
{
    private readonly AppDbContext _context;
    
    public TransactionController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransactions()
    {
        var transactions = await _context.Transaction.ToListAsync();
        return Ok(transactions);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetUserIdTransaction([FromRoute]Guid userId)
    {
        var transactions = await _context.Transaction.Where(x => x.UserId == userId).ToListAsync();
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransactionProduct([FromBody] CreateTransactionDto transactionDto)
    {
        var transactions = new Transactions()
        {
            Amount = transactionDto.Amount,
            Category = transactionDto.Category,
            Date = transactionDto.Date,
            Description = transactionDto.Description,
            IsSubscription = transactionDto.IsSubscription,
            UserId = transactionDto.UserId,
        };
        await _context.Transaction.AddAsync(transactions);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUserIdTransaction),  new { userId = transactions.UserId }, transactions);
    }

    [HttpPut("{transactionId:guid}")]
    public async Task<IActionResult> UpdateTransactionProduct([FromRoute]Guid transactionId, [FromBody] UpdateTransactionDto transactionDto)
    {
        var existingTransaction = await _context.Transaction.FirstOrDefaultAsync(y => y.Id == transactionId);
        if (existingTransaction == null)
            return NotFound(new { Message = $"Transaction id {transactionId} was not found or is invalid" });
        existingTransaction.UserId = transactionDto.UserId;
        existingTransaction.Amount = transactionDto.Amount;
        existingTransaction.Category = transactionDto.Category;
        existingTransaction.IsSubscription = transactionDto.IsSubscription;
        existingTransaction.Date = transactionDto.Date;
        existingTransaction.Description = transactionDto.Description;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{transactionId:guid}")]
    public async Task<IActionResult> DeleteTransactionProduct([FromRoute] Guid transactionId)
    {
        var deleteTransaction = await _context.Transaction.FindAsync(transactionId);
        if (deleteTransaction == null)
            return NotFound(new { message = $"Transaction id {transactionId} was not found or is invalid" });
        _context.Transaction.Remove(deleteTransaction);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}