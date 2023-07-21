using Microsoft.AspNetCore.Mvc;
using Venta.Data.Interfaces;
using Venta.Dto.Object.Clothing;
using Venta.Dto.Object.Purchase;
using Venta.Services.Interface;

namespace Venta.CMS.Controllers
{
    public class ClothingController : Controller
    {
        private readonly IClothingService _clothingService;
        private readonly IUserService _userService;
        private readonly IClothingCategoryService _clothingCategoryService;
        private readonly ISizeService _sizeService;

        public ClothingController(IClothingService clothingService,
            IUserService userService,
            IClothingCategoryService clothingCategoryService,
            ISizeService sizeService)
        {
            _clothingService = clothingService;
            _userService = userService;
            _clothingCategoryService = clothingCategoryService;
            _sizeService = sizeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList(string filter, bool? isActive, int? categoryId,int offset, int limit, string sortBy, string orderBy)
        {
            var result = await _clothingService.GetAll(filter, isActive, categoryId, offset, limit, sortBy, orderBy);
            return Json(result);
        }

        public async Task<IActionResult> Create()
        {

            var model = new PostClothingViewModel();
            model.ClothingCategories = await _clothingCategoryService.GetCombo();
            model.Sizes = await _sizeService.GetCombo();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostClothingViewModel model)
        {            
            if (!ModelState.IsValid)
            {
                model.ClothingCategories = await _clothingCategoryService.GetCombo();
                model.Sizes = await _sizeService.GetCombo();
                return View(model);
            }

            if (model.PostClothingMaterial.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Debe agregar al menos un material");
                model.ClothingCategories = await _clothingCategoryService.GetCombo();
                model.Sizes = await _sizeService.GetCombo();
                return View(model);
            }

            var userName = _userService.GetUserName();
            await _clothingService.Create(model, userName);

            TempData["SuccessMessage"] = "Registro exitoso";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _clothingService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            model.ClothingCategories = await _clothingCategoryService.GetCombo();
            model.Sizes = await _sizeService.GetCombo();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostClothingViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                model.ClothingCategories = await _clothingCategoryService.GetCombo();
                model.Sizes = await _sizeService.GetCombo();
                return View(model);
            }

            if (model.PostClothingMaterial.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Debe agregar al menos un material");
                model.ClothingCategories = await _clothingCategoryService.GetCombo();
                model.Sizes = await _sizeService.GetCombo();
                return View(model);
            }

            var userName = _userService.GetUserName();
            await _clothingService.Update(model, userName);

            TempData["SuccessMessage"] = "Actualización exitoso";
            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            var clothingInUse = await _clothingService.ValidateClothingInUse(id);

            if (clothingInUse) return Unauthorized("No se puede eliminar");

            var userName = _userService.GetUserName();
            await _clothingService.Delete(id, userName);

            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> ChangeStatus([FromQuery]int id, [FromQuery] bool isActive)
        {
            var userName = _userService.GetUserName();
            await _clothingService.ChangeStatus(id, isActive, userName);

            return Json(id);
        }

        [HttpGet]
        public async Task<IActionResult> Autocomplete(string filter, int limit)
        {
            var results = await _clothingService.GetAutocomplete(filter, limit);
            return Json(results);
        }

        [HttpGet]
        public async Task<IActionResult> AutocompleteSales(string filter, int limit)
        {
            var results = await _clothingService.GetAutocompleteSize(filter, limit);
            return Json(results);
        }

    }
}
