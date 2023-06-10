using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface IManufacturingRepository
    {
        void Add(Manufacturing entity);
        Task<(IEnumerable<Manufacturing>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy);
        Task<Manufacturing?> GetById(int id);
        void Update(Manufacturing entity);
    }
}
