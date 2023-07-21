using SistemaVenta.Entities;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.Entities.Enums;
using Venta.Dto.Object.Others;

namespace Venta.Data.Repository
{
    public class ClothingRepository : IClothingRepository
    {
        private readonly ApplicationContext _context;

        public ClothingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Clothing>, int)> GetAll(string filter, bool? isActive, int? clothingCategoryId, int offset, int limit, string sortBy, string orderBy)
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
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Include(x => x.ClothingCategory).Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public async Task<IEnumerable<Clothing>> GetAll(string filter, int limit)
        {
            var records = (from a in _context.Clothing
                           where a.DeletionDate == null
                             && a.IsActive
                             && (string.IsNullOrEmpty(filter) || a.Name.ToUpper().Contains(filter.ToUpper()))
                           orderby a.Name descending
                           select a);

            return await records.Take(limit).ToListAsync();
        }

        public async Task<IEnumerable<ItemClothingSizeDTO>> GetAllWithSizes(string filter, int limit)
        {
            var records = (from a in _context.Clothing
                           join b in _context.ClothingSize on a.Id equals b.ClothingId into lst
                           from b in lst.DefaultIfEmpty()
                           join c in _context.Size on b.SizeId equals c.Id
                           where a.DeletionDate == null
                             && a.IsActive
                             && (string.IsNullOrEmpty(filter) || a.Name.ToUpper().Contains(filter.ToUpper()))
                           orderby a.Name descending
                           group new { b, c } by a into grp
                           select new ItemClothingSizeDTO
                           {
                               Id = grp.Key.Id,
                               Name = grp.Key.Name,
                               InvestmentUnit = grp.Key.InvestmentUnit
                           });

            return await records.Take(limit).ToListAsync();
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

        public async Task<bool> ClothingInUse(int id)
        {
            var existsInSales = await _context.SalesClothingSize.Where(x => x.DeletionDate == null && x.ClothingId == id).AnyAsync();

            return existsInSales;

        }

    }
}
