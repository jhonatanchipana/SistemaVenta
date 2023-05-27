using SistemaVenta.Entities;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.Entities.Enums;

namespace Venta.Data.Repository
{
    public class ClothingRepository : IClothingRepository
    {
        private readonly ApplicationContext _context;

        public ClothingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Clothing>, int)> GetAll(string filter, ClothingSizeType clothingSizeType, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.Clothing
                         let description = a.Descripcion ?? string.Empty
                         where
                            (string.IsNullOrEmpty(filter) ||
                                a.Name.ToUpper().Contains(filter.ToUpper())
                                || description.Equals(filter.ToUpper())
                            )
                            &&
                            (isActive.HasValue ? (a.IsActive == isActive) : (a.IsActive == a.IsActive))
                            &&
                            (clothingSizeType == 0 || a.Size == clothingSizeType)
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public void Add(Clothing entity)
        {
            _context.Add(entity);
        }

        public async Task<Clothing?> GetById(int id)
        {
            return await _context.Clothing.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);
        }

        public void Update(Clothing entity)
        {
            _context.Update(entity);
        }

        public void ChangeStatus(int id, bool isActive, string user)
        {
            var entity = _context.Clothing.FirstOrDefault(x => x.Id == id && x.DeletionDate == null);
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
            var entity = _context.Clothing.FirstOrDefault(x => x.Id == id && x.DeletionDate == null);
            if (entity != null)
            {
                entity.DeletionDate = DateTime.UtcNow;
                _context.Update(entity);
            }
        }
    }
}
