using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface IClothingRepository
    {
        void Add(Clothing entity);
        Task<(IEnumerable<Clothing>, int)> GetAll(string filter, bool? isActive, ClothingSizeType clothingSizeType, int? clothingCategoryId, int offset, int limit, string sortBy, string orderBy);
        Task<Clothing?> GetById(int id);
        void Update(Clothing entity);
    }
}
