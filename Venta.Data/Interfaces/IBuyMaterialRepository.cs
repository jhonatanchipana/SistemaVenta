using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface IBuyMaterialRepository
    {
        void Add(BuyMaterial entity);
        void ChangeStatus(int id, bool isActive, string user);
        void Delete(int id, string user);
        Task<(IEnumerable<BuyMaterial>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy);
        Task<BuyMaterial?> GetById(int id);
        void Update(BuyMaterial entity);
    }
}
