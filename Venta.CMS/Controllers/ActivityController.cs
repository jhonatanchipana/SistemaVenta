using Microsoft.AspNetCore.Mvc;
using Venta.Dto.Object.Activity;
using Venta.Dto.Object.Purchase;
using Venta.Services.Bussiness;
using Venta.Services.Interface;

namespace Venta.CMS.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IPurchaseService _purchaseService;
        private readonly IUserService _userService;

        public ActivityController(IActivityService campaignservice,
            IPurchaseService purchaseService,
            IUserService userService)
        {
            _activityService = campaignservice;
            _purchaseService = purchaseService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList(string filter, bool? isActive, int statusActivity, int offset, int limit, string sortBy, string orderBy)
        {
            var result = await _activityService.GetAll(filter, isActive, statusActivity, offset, limit, sortBy, orderBy);
            return Json(result);
        }

        public async Task<IActionResult> Create()
        {
            var model = new PostActivityViewModel();
            model.PurchaseBuyDate = await _purchaseService.GetCombo();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostActivityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.PurchaseBuyDate = await _purchaseService.GetCombo();
                return View(model);
            }

            var userName = _userService.GetUserName();
            await _activityService.Create(model, userName);

            TempData["SuccessMessage"] = "Registro exitoso";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _activityService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            model.PurchaseBuyDate = await _purchaseService.GetCombo();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostActivityViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                model.PurchaseBuyDate = await _purchaseService.GetCombo();
                return View(model);
            }

            var userName = _userService.GetUserName();
            await _activityService.Update(model, userName);

            TempData["SuccessMessage"] = "Actualización exitoso";
            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _activityService.GetById(id);

            if (entity is null) return NotFound();

            var userName = _userService.GetUserName();
            await _activityService.Delete(id, userName);

            return RedirectToAction("Index");
        }

    }
}
