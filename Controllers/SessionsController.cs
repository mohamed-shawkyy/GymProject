using Gym.BLL.Contacts;
using Gym.BLL.ViewModeles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Gym.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ISessionService _sessionService;
        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var result =await _sessionService.GetAllSessionsAsync(ct);
            return View(result.value);
        }

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken ct)
        {
            await PopulateDropDownListAsync(ct);
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateSessionViewModel model,CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropDownListAsync(ct);
                return View(model);
            }
            var result=await _sessionService.CreateSessionAsync(model, ct);
            if(result.success)
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await PopulateDropDownListAsync(ct);
                TempData["ErrorMessage"] = result.errorMessage;
                return View(model);
            }
        }
        private async Task PopulateDropDownListAsync(CancellationToken ct)
        {
            ViewBag.Categories = new SelectList(await _sessionService.GetCategorysForDropdownListAsync(ct), "Id", "Name");
            ViewBag.Trainers = new SelectList(await _sessionService.GetTrainersForDropdownListAsync(ct), "Id", "Name");
        }
        #endregion
    }
}
