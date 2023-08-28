using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Venta.Data.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly ApplicationContext _context;

        public SalesRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Sales>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.Sales
                         where
                            (isActive.HasValue ? (a.IsActive == isActive) : (a.IsActive == a.IsActive))
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public void Add(Sales entity)
        {
            _context.Add(entity);
        }

        public async Task<Sales?> GetById(int id)
        {
            return await _context.Sales.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);
        }

        public void Update(Sales entity)
        {
            _context.Update(entity);
        }

        public async Task<decimal> GetSaleBetweenDates(DateTime start, DateTime end)
        {
            var endDate = end.AddDays(1).AddSeconds(-1);
            var query = (from a in _context.Sales
                         where 
                            a.DeletionDate == null
                            && a.SaleDate >= start
                            && a.SaleDate <= endDate
                         select a.PriceTotal);

            return await query.SumAsync();

        }

    }
}
