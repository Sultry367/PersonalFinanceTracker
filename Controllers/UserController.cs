using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Dtos.UserDtos;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController:ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var user = await _context.User.ToListAsync();
        return Ok(user);
    }

    [HttpGet("user/{id:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null)
            return NotFound(new { Message = $"User id {id} was not found or is invalid" });
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var user = new Users()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Age = userDto.Age,
        };
        await _context.User.AddAsync(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut("user/{id:guid}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto userDto)
    {
        var existingUser = await _context.User.FirstOrDefaultAsync(t => t.Id == id);
        if(existingUser == null)
            return NotFound(new { Message = $"User id {id} was not found or is invalid" });
        existingUser.FirstName = userDto.FirstName;
        existingUser.LastName = userDto.LastName;
        existingUser.Age = userDto.Age;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("user/{id:guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var deleteUser = await _context.User.FindAsync(id);
        if(deleteUser == null)
            return NotFound(new { Message = $"User id {id} was not found or is invalid" });
        _context.User.Remove(deleteUser);
        await _context.SaveChangesAsync();
        return NoContent(); 
    }
}