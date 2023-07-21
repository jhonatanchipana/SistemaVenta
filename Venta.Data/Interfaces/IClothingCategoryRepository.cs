using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface IClothingCategoryRepository
    {
        Task<IEnumerable<ClothingCategory>> GetAll();
    }
}
