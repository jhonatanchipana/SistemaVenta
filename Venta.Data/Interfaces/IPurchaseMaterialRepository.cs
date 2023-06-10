using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface IPurchaseMaterialRepository
    {
        void Add(PurchaseMaterial entity);
        Task<(IEnumerable<PurchaseMaterial>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy);
        Task<IEnumerable<PurchaseMaterial>> GetAllByBuyMaterialId(int buyMaterialId);
        Task<PurchaseMaterial?> GetById(int id);
        void Update(PurchaseMaterial entity);
    }
}
