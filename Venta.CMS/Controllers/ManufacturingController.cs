using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Venta.Dto.Object.Manufacturing;
using Venta.Services.Bussiness;
using Venta.Services.Interface;

namespace Venta.CMS.Controllers
{
    public class ManufacturingController : Controller
    {
        private readonly IManufacturingService _manufacturingService;
        private readonly IUserService _userService;

        public ManufacturingController(IManufacturingService manufacturingService,
            IUserService userService)
        {
            _manufacturingService = manufacturingService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            var result = await _manufacturingService.GetAll(filter, isActive, offset, limit, sortBy, orderBy);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostManufacturingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();
                return Json(new {success = false, errors });
            }

            if(model.PostManufacturingClothings.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar al menos una prenda");
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();
                return Json(new { success = false, errors });
            }

            var userName = _userService.GetUserName();
            var id = await _manufacturingService.Create(model, userName);

            return Json(new { success = true, message = "Registro exitoso" });
        }


        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _manufacturingService.GetByIdPost(id);

            if (entity is null) return NotFound();

            var userName = _userService.GetUserName();
            await _manufacturingService.Delete(id, userName);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await _manufacturingService.GetById(id);

            if(model is null) return NotFound(); 

            return Json(model);
        }
    }
}
