using Microsoft.AspNetCore.Mvc;
using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using Venta.Dto.Object.Material;
using Venta.Services.Bussiness;
using Venta.Services.Interface;

namespace Venta.CMS.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IMaterialService _serviceMaterial;
        private readonly IUserService _userService;

        public MaterialController(IMaterialService serviceMaterial, IUserService userService)
        {
            _serviceMaterial = serviceMaterial;
            _userService = userService;
        }

        public IActionResult Index()
        {            
            return View();
        }

        public async Task<IActionResult> GetList(string filter, bool? isActive, int unitMeasurement, int offset, int limit, string sortBy, string orderBy)
        {
            var result = await _serviceMaterial.GetAll(filter, isActive, unitMeasurement, offset, limit, sortBy, orderBy);
            return Json(result);
        }

        public IActionResult Create()
        {
            return View(new PostMaterialViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostMaterialViewModel model) 
        {
            if (!ModelState.IsValid) {
                return View(model);
            }

            var userName = _userService.GetUserName();
            await _serviceMaterial.Create(model , userName);

            TempData["SuccessMessage"] = "Registro exitoso";
            return RedirectToAction("Index");
        }        

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _serviceMaterial.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new PostMaterialViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Cost = entity.Cost,
                UnitQuantity = entity.UnitQuantity,
                UnitMeasurement = entity.UnitMeasurement,
                Stock = entity.Stock,
                CreatedBy = entity.CreateBy,
                CreationDate = entity.CreationDate,
                ModifiedBy = entity.ModifiedBy,
                ModificationDate = entity.ModificationDate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostMaterialViewModel model)
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
            await _serviceMaterial.Update(model, userName);

            TempData["SuccessMessage"] = "Actualización exitoso";
            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> ChangeStatus([FromQuery]int id, [FromQuery] bool isActive)
        {
            var userName = _userService.GetUserName();
            await _serviceMaterial.ChangeStatus(id, isActive, userName);

            return Json(id);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            var materialInUse = await _serviceMaterial.ValidateMaterialInUse(id);

            if (materialInUse) return Unauthorized("No se puede eliminar");

            var userName = _userService.GetUserName();
            await _serviceMaterial.Delete(id, userName);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Autocomplete(string filter, int limit, int[] ignoreId)
        {
            var results = await _serviceMaterial.GetAutocomplete(filter, limit, ignoreId);
            return Json(results);
        }

    }

}
