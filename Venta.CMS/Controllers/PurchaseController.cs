using Microsoft.AspNetCore.Mvc;
using Venta.Dto.Object.Purchase;
using Venta.Services.Interface;

namespace Venta.CMS.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IUserService _userService;

        public PurchaseController(IPurchaseService purchaseService,
            IUserService userService)
        {
            _purchaseService = purchaseService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            var result = await _purchaseService.GetAll(filter, isActive, offset, limit, sortBy, orderBy);
            return Json(result);
        }

        public IActionResult Create()
        {
            var model = new PostPurchaseViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostPurchaseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.PostBuyMaterialDetail.Count == 0)
            {
                ModelState.AddModelError(string.Empty,
                                 "Debe agregar al menos un material");
                return View(model);
            }

            var userName = _userService.GetUserName();
            await _purchaseService.Create(model , userName);

            TempData["SuccessMessage"] = "Registro exitoso";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _purchaseService.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            var modelDetail = entity.BuyMaterialDetailDTO.Select(x => new PostPurchaseMaterialViewModel()
            {
                Id = x.Id,
                MaterialId = x.MaterialId,
                MaterialName = x.MaterialName,
                PriceUnit = x.Price,
                Quantity = x.Quantity,
                CreateBy = x.CreateBy,
                CreationDate = x.CreationDate,
                ModifiedBy= x.ModifiedBy,
                ModificationDate = x.ModificationDate,                                                        
            }).ToList();

            var model = new PostPurchaseViewModel()
            {
                Id = entity.Id,
                BuyDate = entity.BuyDate,
                CostTotal = entity.CostTotal,
                NameBuyer = entity.NameBuyer,
                QuantityMaterial = entity.QuantityMaterial,
                PostBuyMaterialDetail = modelDetail,
                CreateBy = entity.CreateBy,
                CreationDate = entity.CreationDate,
                ModifiedBy = entity.ModifiedBy,
                ModificationDate = entity.ModificationDate
            };            

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostPurchaseViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (model.PostBuyMaterialDetail.Count == 0)
            {
                ModelState.AddModelError(nameof(model.PostBuyMaterialDetail),
                                 "Agrega al menos material");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userName = _userService.GetUserName();
            await _purchaseService.Update(model, userName);

            TempData["SuccessMessage"] = "Actualización exitoso";
            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _purchaseService.GetById(id);

            if (entity is null) return NotFound();

            var userName = _userService.GetUserName();
            await _purchaseService.Delete(id, userName);

            return RedirectToAction("Index");
        }

    }
}
