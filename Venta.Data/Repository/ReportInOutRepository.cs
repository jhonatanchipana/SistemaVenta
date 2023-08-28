using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Venta.Data.Repository
{
    public class ReportInOutRepository : IReportInOutRepository
    {
        private readonly ApplicationContext _context;

        public ReportInOutRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<ReportInOut>, int)> GetAll(string filter, bool? isActive, StatusActivityType statusActivityType, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.ReportInOut
                         where
                            (string.IsNullOrEmpty(filter) ||
                                a.Name.ToUpper().Contains(filter.ToUpper())                                  
                            )
                            &&
                            (statusActivityType == 0 || statusActivityType == a.StatusActivityType)
                            &&
                            (isActive.HasValue ? (a.IsActive == isActive) : (a.IsActive == a.IsActive))
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public void Add(ReportInOut entity)
        {
            _context.Add(entity);
        }

        public async Task<ReportInOut?> GetById(int id)
        {
            return await _context.ReportInOut.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);
        }

        public void Update(ReportInOut entity)
        {
            _context.Update(entity);
        }

        public async Task<decimal> GetCostTotalOfPurchase(int id)
        {
            var query = (from a in _context.ReportInOut
                          join b in _context.Purchase on a.PurchaseId equals b.Id
                          where
                            a.DeletionDate == null
                            & b.DeletionDate == null
                          select b.CostTotal);

            return await query.FirstOrDefaultAsync();

        }

    }
}
