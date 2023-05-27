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
        void ChangeStatus(int id, bool isActive, string user);
        void Delete(int id, string user);
        Task<(IEnumerable<Clothing>, int)> GetAll(string filter, ClothingSizeType clothingSizeType, bool? isActive, int offset, int limit, string sortBy, string orderBy);
        Task<Clothing?> GetById(int id);
        void Update(Clothing entity);
    }
}
