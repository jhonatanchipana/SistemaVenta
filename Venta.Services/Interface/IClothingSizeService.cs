using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.ClothingSize;
using Venta.Dto.Object.Others;

namespace Venta.Services.Interface
{
    public interface IClothingSizeService
    {
        Task<IEnumerable<ItemClothingSizeDTO>> GetAutocomplete(string filter, int limit);
        Task<IEnumerable<GetClothingSizeDTO>> GetListByClothingId(int clothingId);
    }
}
