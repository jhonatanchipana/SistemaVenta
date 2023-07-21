using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Entities;

namespace Venta.Data.Interfaces
{
    public interface IClothingMaterialRepository
    {
        void Add(ClothingMaterial entity);
        Task<IEnumerable<ClothingMaterial>> GetAllByClothingId(int clothingId);
        void Update(ClothingMaterial entity);
    }
}
