using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Venta.Data.Repository
{
    public class SalesClothingRepository : ISalesClothingRepository
    {
        private readonly ApplicationContext _context;

        public SalesClothingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<SalesClothing>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.SalesClothing
                         where
                            (isActive.HasValue ? (a.IsActive == isActive) : (a.IsActive == a.IsActive))
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public void Add(SalesClothing entity)
        {
            _context.Add(entity);
        }

        public async Task<SalesClothing?> GetById(int id)
        {
            return await _context.SalesClothing.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);
        }

        public void Update(SalesClothing entity)
        {
            _context.Update(entity);
        }

        public void ChangeStatus(int id, bool isActive, string user)
        {
            var entity = _context.SalesClothing.FirstOrDefault(x => x.Id == id && x.DeletionDate == null);
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
            var entity = _context.SalesClothing.FirstOrDefault(x => x.Id == id && x.DeletionDate == null);
            if (entity != null)
            {
                entity.DeletionDate = DateTime.UtcNow;
                _context.Update(entity);
            }
        }
    }
}
