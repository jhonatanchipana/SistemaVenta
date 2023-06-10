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

        public async Task<(IEnumerable<Clothing>, int)> GetAll(string filter, bool? isActive, ClothingSizeType clothingSizeType, int? clothingCategoryId, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.Clothing
                         let description = a.Description ?? string.Empty
                         where
                            (string.IsNullOrEmpty(filter) ||
                                a.Name.ToUpper().Contains(filter.ToUpper())
                                || description.Equals(filter.ToUpper())
                            )
                            &&
                            (clothingCategoryId.HasValue ? (a.ClothingCategoryId == clothingCategoryId) : (a.ClothingCategoryId == a.ClothingCategoryId))
                            &&
                            (isActive.HasValue ? (a.IsActive == isActive) : (a.IsActive == a.IsActive))
                            &&
                            (clothingSizeType == 0 || a.Size == clothingSizeType)
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Include(x => x.ClothingCategory).Skip(offset).Take(limit).ToListAsync();

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

    }
}
