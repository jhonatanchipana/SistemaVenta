using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.Others;

namespace Venta.Data.Interfaces
{
    public interface IClothingRepository
    {
        void Add(Clothing entity);
        Task<bool> ClothingInUse(int id);
        Task<(IEnumerable<Clothing>, int)> GetAll(string filter, bool? isActive, int? clothingCategoryId, int offset, int limit, string sortBy, string orderBy);
        Task<IEnumerable<Clothing>> GetAll(string filter, int limit);
        Task<IEnumerable<ItemClothingSizeDTO>> GetAllWithSizes(string filter, int limit);
        Task<Clothing?> GetById(int id);
        void Update(Clothing entity);
    }
}
