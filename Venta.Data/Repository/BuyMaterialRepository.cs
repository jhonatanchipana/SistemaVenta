using SistemaVenta.Entities;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Venta.Data.Repository
{
    public class BuyMaterialRepository : IBuyMaterialRepository
    {
        private readonly ApplicationContext _context;

        public BuyMaterialRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<BuyMaterial>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.BuyMaterial
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

        public void Add(BuyMaterial entity)
        {
            _context.Add(entity);
        }

        public async Task<BuyMaterial?> GetById(int id)
        {
            return await _context.BuyMaterial.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);            
        }

        public void Update(BuyMaterial entity)
        {
            _context.Update(entity);
        }

        public void ChangeStatus(int id, bool isActive, string user)
        {
            var entity = _context.BuyMaterial.FirstOrDefault(x => x.Id == id && x.DeletionDate == null);
            if (entity != null)
            {
                entity.IsActive = isActive;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.UtcNow;
                _context.Update(entity);
            }
        }

        public void Delete(int id, string user)
        {
            var entity = _context.BuyMaterial.FirstOrDefault(x => x.Id == id && x.DeletionDate == null);
            if (entity != null)
            {
                entity.DeletionDate = DateTime.UtcNow;
                _context.Update(entity);
            }
        }
    }
}
