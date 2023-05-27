using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Venta.Data.Repository
{
    public class ClothingManufacturingRepository : IClothingManufacturingRepository
    {
        private readonly ApplicationContext _context;

        public ClothingManufacturingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<ClothingManufacturing>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.ClothingManufacturing
                         where
                            (isActive.HasValue ? (a.IsActive == isActive ) : (a.IsActive == a.IsActive))
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public void Add(ClothingManufacturing entity)
        {
            _context.Add(entity);
        }

        public async Task<ClothingManufacturing?> GetById(int id)
        {
            return await _context.ClothingManufacturing.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);
        }

        public void Update(ClothingManufacturing entity)
        {
            _context.Update(entity);
        }

        public void ChangeStatus(int id, bool isActive, string user)
        {
            var entity = _context.ClothingManufacturing.FirstOrDefault(x => x.Id == id && x.DeletionDate == null);
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
            var entity = _context.ClothingManufacturing.FirstOrDefault(x => x.Id == id && x.DeletionDate == null);
            if (entity != null)
            {
                entity.DeletionDate = DateTime.UtcNow;
                _context.Update(entity);
            }
        }
    }
}
