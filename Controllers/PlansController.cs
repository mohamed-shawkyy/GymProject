using Gym.AppDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gym.Controllers
{
    public class PlansController : Controller
    {
        private readonly GymDbContext gymDbContext;
        public PlansController()
        {
            gymDbContext = new GymDbContext();
        }
        public async Task<IActionResult> Index()
        {
            var plans = await gymDbContext.Plans.ToListAsync();
            return View(plans); 
        }
        public async Task<IActionResult> Details(int id)
        {
            var plan = await gymDbContext.Plans.FindAsync(id);
            if (plan == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
    }
}
