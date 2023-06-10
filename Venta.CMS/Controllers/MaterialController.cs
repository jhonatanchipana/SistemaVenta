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
        public async Task<IActionResult> Create(PostMaterialViewModel modelo) 
        {
            if (!ModelState.IsValid) {
                return View(modelo);
            }

            modelo.CreatedBy = _userService.GetUserName();
            await _serviceMaterial.Create(modelo);

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

            var modelo = new PostMaterialViewModel()
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

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostMaterialViewModel modelo)
        {
            if (id != modelo.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            modelo.ModifiedBy = _userService.GetUserName();
            await _serviceMaterial.Update(modelo);

            TempData["SuccessMessage"] = "Actualización exitoso";
            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _serviceMaterial.GetById(id);

            if(entity is null) return NotFound();

            var userName = _userService.GetUserName();
            await _serviceMaterial.Delete(id, userName);

            return RedirectToAction("Index");
        }

    }

}
