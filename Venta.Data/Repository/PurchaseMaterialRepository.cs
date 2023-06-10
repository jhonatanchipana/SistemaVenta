using Microsoft.EntityFrameworkCore;
using SistemaVenta.Entities;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using System.Linq.Dynamic.Core;

namespace Venta.Data.Repository
{
    public class PurchaseMaterialRepository : IPurchaseMaterialRepository
    {
        private readonly ApplicationContext _context;

        public PurchaseMaterialRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<PurchaseMaterial>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.PurchaseMaterial
                         where
                            (isActive.HasValue ? (a.IsActive == isActive) : (a.IsActive == a.IsActive))
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public async Task<IEnumerable<PurchaseMaterial>> GetAllByBuyMaterialId(int buyMaterialId)
        {
            var query = (from a in _context.PurchaseMaterial
                         where
                            a.PurchaseId == buyMaterialId
                            &&
                            a.IsActive
                            &&
                            a.DeletionDate == null
                         select a).Include(c => c.Material);

            var records = await query.ToListAsync();

            return records;
        }

        public void Add(PurchaseMaterial entity)
        {
            _context.Add(entity);
        }

        public async Task<PurchaseMaterial?> GetById(int id)
        {
            return await _context.PurchaseMaterial.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);
        }

        public void Update(PurchaseMaterial entity)
        {
            _context.Update(entity);
        }


    }
}
