using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface ISalesRepository
    {
        void Add(Sales entity);
        Task<(IEnumerable<Sales>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy);
        Task<Sales?> GetById(int id);
        void Update(Sales entity);
    }
}
