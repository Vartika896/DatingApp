using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Entities;

namespace TestAPI.Controllers;
public class UsersController : BasApiController
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        return  await _context.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public  async Task<ActionResult<AppUser>> GetUserById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

}
