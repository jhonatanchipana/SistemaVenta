using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface ISalesClothingRepository
    {
        void Add(SalesClothing entity);
        void ChangeStatus(int id, bool isActive, string user);
        void Delete(int id, string user);
        Task<(IEnumerable<SalesClothing>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy);
        Task<SalesClothing?> GetById(int id);
        void Update(SalesClothing entity);
    }
}
