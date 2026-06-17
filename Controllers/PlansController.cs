using Gym.DAL.AppDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Gym.DAL.Interfaces;
using Gym.DAL.Repositories;

namespace Gym.Controllers
{
    public class PlansController : Controller
    {
        private readonly IPlanRepository _planRepository;
        public PlansController(IPlanRepository planRepo)
        {
            _planRepository = planRepo;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await _planRepository.GetAllPlansAsync(false,ct);
            return View(plans); 
        }
        public async Task<IActionResult> Details(int id)
        {
            var plan = await _planRepository.GetPlanByIdAsync(id);
            if (plan == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
    }
}
