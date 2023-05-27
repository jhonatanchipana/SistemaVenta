using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface IClothingManufacturingRepository
    {
        void Add(ClothingManufacturing entity);
        void ChangeStatus(int id, bool isActive, string user);
        void Delete(int id, string user);
        Task<(IEnumerable<ClothingManufacturing>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy);
        Task<ClothingManufacturing?> GetById(int id);
        void Update(ClothingManufacturing entity);
    }
}
