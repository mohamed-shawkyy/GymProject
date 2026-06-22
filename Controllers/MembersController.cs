using Gym.BLL.Contacts;
using Gym.BLL.ViewModeles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gym.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;
        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var members = await _memberService.GetAllMembersAsync(ct);
            return View(members);
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Create),model);
            }
            await _memberService.CreateMemberAsync(model, ct);
            return RedirectToAction(nameof(Index));
        }
    }
}
