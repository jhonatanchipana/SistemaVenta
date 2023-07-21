using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface IActivityRepository
    {
        void Add(Activity entity);
        Task<(IEnumerable<Activity>, int)> GetAll(string filter, bool? isActive, StatusActivityType statusActivityType, int offset, int limit, string sortBy, string orderBy);
        Task<Activity?> GetById(int id);
        void Update(Activity entity);
    }
}
