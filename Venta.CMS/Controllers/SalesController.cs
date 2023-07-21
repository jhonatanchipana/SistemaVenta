using Microsoft.AspNetCore.Mvc;
using Venta.Dto.Object.Clothing;
using Venta.Dto.Object.Sales;
using Venta.Services.Bussiness;
using Venta.Services.Interface;

namespace Venta.CMS.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly IUserService _userService;

        public SalesController(ISalesService salesService,
            IUserService userService)
        {
            _salesService = salesService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            var result = await _salesService.GetAll(filter, isActive, offset, limit, sortBy, orderBy);
            return Json(result);
        }

        public IActionResult Create()
        {
            var model = new PostSalesViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostSalesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userName = _userService.GetUserName();
            await _salesService.Create(model, userName);

            TempData["SuccessMessage"] = "Registro exitoso";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _salesService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostSalesViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userName = _userService.GetUserName();
            await _salesService.Update(model, userName);

            TempData["SuccessMessage"] = "Actualización exitoso";
            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _salesService.GetById(id);

            if (entity is null) return NotFound();

            var userName = _userService.GetUserName();
            await _salesService.Delete(id, userName);

            return RedirectToAction("Index");
        }
    }
}
