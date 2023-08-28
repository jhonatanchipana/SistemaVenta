using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface IReportInOutRepository
    {
        void Add(ReportInOut entity);
        Task<(IEnumerable<ReportInOut>, int)> GetAll(string filter, bool? isActive, StatusActivityType statusActivityType, int offset, int limit, string sortBy, string orderBy);
        Task<ReportInOut?> GetById(int id);
        Task<decimal> GetCostTotalOfPurchase(int id);
        void Update(ReportInOut entity);
    }
}
