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
            return View(new PostPurchaseViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostPurchaseViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            modelo.CreateBy = _userService.GetUserName();
            await _purchaseService.Create(modelo);

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

            var modelo = new PostPurchaseViewModel()
            {
                Id = entity.Id,
                BuyDate= entity.BuyDate,
                CostTotal = entity.CostTotal,
                NameBuyer = entity.NameBuyer,
                QuantityMaterial = entity.QuantityMaterial,
                //PostBuyMaterialDetail = 
                CreateBy = entity.CreateBy,
                CreationDate = entity.CreationDate,
                ModifiedBy = entity.ModifiedBy,
                ModificationDate = entity.ModificationDate
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostPurchaseViewModel modelo)
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
            await _purchaseService.Update(modelo);

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
