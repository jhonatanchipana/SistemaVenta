using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface IPurchaseRepository
    {
        void Add(Purchase entity);
        Task<(IEnumerable<Purchase>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy);
        Task<Purchase?> GetById(int id);
        void Update(Purchase entity);
    }
}
