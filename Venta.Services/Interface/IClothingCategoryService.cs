using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Services.Interface
{
    public interface IClothingCategoryService
    {
        Task<IEnumerable<SelectListItem>> GetCombo();
    }
}
