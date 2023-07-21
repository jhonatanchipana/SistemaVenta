using Microsoft.EntityFrameworkCore;
using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Connection;
using Venta.Dto.Object.Material;
using System.Linq.Dynamic.Core;
using Venta.Data.Interfaces;

namespace Venta.Data.Repository
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationContext _context;

        public MaterialRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Material>, int)> GetAll(string filter, bool? isActive, UnitMeasurementType unitMeasurement, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.Material
                         let description = a.Description ?? string.Empty
                         where
                            (string.IsNullOrEmpty(filter) ||
                                a.Name.ToUpper().Contains(filter.ToUpper()) || description.ToUpper().Contains(filter.ToUpper())
                            )
                            &&
                            (unitMeasurement == 0 || unitMeasurement == a.UnitMeasurement)
                            &&
                            (isActive.HasValue ? (a.IsActive == isActive) : (a.IsActive == a.IsActive))
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public async Task<IEnumerable<Material>> GetAll(string filter, int limit, int[] ignoreIds)
        {
            var records = (from a in _context.Material
                           where a.DeletionDate == null
                             && a.IsActive
                             && !ignoreIds.Contains(a.Id)
                             && (string.IsNullOrEmpty(filter) || a.Name.ToUpper().Contains(filter.ToUpper()))
                           orderby a.Name descending
                           select a);

            return await records.Take(limit).ToListAsync();
        }

        public void Add(Material entity)
        {
            _context.Add(entity);
        }

        public async Task<Material?> GetById(int id)
        {
            return await _context.Material.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);
        }

        public void Update(Material entity)
        {
            _context.Update(entity);
        }

        public async Task<bool> MaterialInUse(int id)
        {
            var existsPurchase = await _context.PurchaseMaterial.Where(x => x.DeletionDate == null && x.MaterialId == id).AnyAsync();
            var existsClothing = await _context.ClothingMaterial.Where(x => x.DeletionDate == null && x.MaterialId == id).AnyAsync();

            return existsPurchase || existsClothing;

        }
    }
}
