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
        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model, CancellationToken ct)
        {

            if (!ModelState.IsValid)
            {
                return View(nameof(Create), model);
            }
            var result = await _memberService.CreateMemberAsync(model, ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Member created successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create member.";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Details

        public async Task<IActionResult> MemberDetails(int id, CancellationToken ct)
        {
            var member = await _memberService.GetMemberDetailsByIdAsync(id, ct);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken ct)
        {
            var healthRecord = await _memberService.GetHealthRecordByMemberIdAsync(id, ct);
            if (healthRecord == null)
            {
                TempData["ErrorMessage"] = "Health record not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(healthRecord);
        }


        #endregion

        #region delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id,CancellationToken ct)
        {
            var member = await _memberService.GetMemberDetailsByIdAsync(id, ct);
            if (member == null)
            {   TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(member);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            var result = await _memberService.DeleteMemberAsync(id, ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Member deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete member.";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
