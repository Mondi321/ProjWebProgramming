using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjWebProgramming.Data;

namespace ProjWebProgramming.Controllers
{
    public class RolesController : Controller
    {
        private readonly ILogger<RolesController> _logger;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly ApplicationDbContext context;
        public RolesController(ILogger<RolesController> logger, RoleManager<IdentityRole<Guid>> roleManager, ApplicationDbContext context)
        {
            _logger = logger;
            this.roleManager = roleManager;
            this.context = context;
        }

        public IActionResult Index()
        {
            ViewBag.roles = roleManager.Roles.ToList();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                if (!String.IsNullOrEmpty(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                }
            }
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            if (!String.IsNullOrEmpty(roleName))
            {
                if (await roleManager.RoleExistsAsync(roleName))
                {
                    var role = await roleManager.FindByNameAsync(roleName);
                    await roleManager.DeleteAsync(role);
                }
            }

            return RedirectToAction(nameof(Index));

        }

    }
}
