using Microsoft.AspNetCore.Mvc;
using SistemaVenta.Entities;
using Venta.Services.Interface;

namespace Venta.CMS.Controllers
{
    public class ClothingSizeController : Controller
    {
        private readonly IClothingSizeService _clothingSizeService;

        public ClothingSizeController(IClothingSizeService clothingSizeService)
        {
            _clothingSizeService = clothingSizeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListByClothingId(int clothingId)
        {
            var result = await _clothingSizeService.GetListByClothingId(clothingId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Autocomplete(string filter, int limit)
        {
            var result = await _clothingSizeService.GetAutocomplete(filter, limit);
            return Json(result);
        }
    }
}
