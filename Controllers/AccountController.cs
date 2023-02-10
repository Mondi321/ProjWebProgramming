using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjWebProgramming.Data;

namespace ProjWebProgramming.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
    }
}
