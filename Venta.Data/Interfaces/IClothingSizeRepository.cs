using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.Others;
using Venta.Entities;

namespace Venta.Data.Interfaces
{
    public interface IClothingSizeRepository
    {
        void Add(ClothingSize entity);
        Task<IEnumerable<ItemClothingSizeDTO>> GetAll(string filter, int limit);
        Task<IEnumerable<ClothingSize>> GetAllByClothingId(int clothingId);
        Task<ClothingSize?> GetById(int id);
        void Update(ClothingSize entity);
    }
}
