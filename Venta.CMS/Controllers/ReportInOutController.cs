using Microsoft.AspNetCore.Mvc;
using Venta.Dto.Object.ReportInOut;
using Venta.Dto.Object.Purchase;
using Venta.Services.Bussiness;
using Venta.Services.Interface;

namespace Venta.CMS.Controllers
{
    public class ReportInOutController : Controller
    {
        private readonly IReportInOutService _reportInOutService;
        private readonly IPurchaseService _purchaseService;
        private readonly IUserService _userService;

        public ReportInOutController(IReportInOutService reportInOutservice,
            IPurchaseService purchaseService,
            IUserService userService)
        {
            _reportInOutService = reportInOutservice;
            _purchaseService = purchaseService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList(string filter, bool? isActive, int statusActivity, int offset, int limit, string sortBy, string orderBy)
        {
            var result = await _reportInOutService.GetAll(filter, isActive, statusActivity, offset, limit, sortBy, orderBy);
            return Json(result);
        }

        public async Task<IActionResult> Create()
        {
            var model = new PostReportInOutViewModel();
            model.PurchaseBuyDate = await _purchaseService.GetCombo();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostReportInOutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.PurchaseBuyDate = await _purchaseService.GetCombo();
                return View(model);
            }

            var userName = _userService.GetUserName();
            await _reportInOutService.Create(model, userName);

            TempData["SuccessMessage"] = "Registro exitoso";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _reportInOutService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            model.PurchaseBuyDate = await _purchaseService.GetCombo();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostReportInOutViewModel model)
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
            await _reportInOutService.Update(model, userName);

            TempData["SuccessMessage"] = "Actualización exitoso";
            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _reportInOutService.GetById(id);

            if (entity is null) return NotFound();

            var userName = _userService.GetUserName();
            await _reportInOutService.Delete(id, userName);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetOutIn(int id)
        {
            var entity = await _reportInOutService.GetById(id);

            if (entity is null) return NotFound();

            var result = await _reportInOutService.GetInOut(id);

            return Json(result);
        }

    }
}
