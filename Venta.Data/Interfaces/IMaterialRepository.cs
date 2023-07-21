using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface IMaterialRepository
    {
        void Add(Material entity);
        Task<bool> MaterialInUse(int id);
        Task<(IEnumerable<Material>, int)> GetAll(string filter, bool? isActive, UnitMeasurementType unitMeasurement, int offset, int limit, string sortBy, string orderBy);
        Task<IEnumerable<Material>> GetAll(string filter, int limit, int[] ignoreIds);
        Task<Material?> GetById(int id);
        void Update(Material entity);
    }
}
