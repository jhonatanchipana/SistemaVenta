using SistemaVenta.Entities;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Venta.Data.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationContext _context;

        public PurchaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Purchase>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.Purchase
                         let nameBuyer = a.NameBuyer ?? string.Empty
                         where
                            (string.IsNullOrEmpty(filter) ||
                                  nameBuyer.ToUpper().Contains(filter.ToUpper()) )
                            &&
                            (isActive.HasValue ? (a.IsActive == isActive) : (a.IsActive == a.IsActive))
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public void Add(Purchase entity)
        {
            _context.Add(entity);
        }

        public async Task<Purchase?> GetById(int id)
        {
            return await _context.Purchase.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);            
        }

        public void Update(Purchase entity)
        {
            _context.Update(entity);
        }

    }
}
