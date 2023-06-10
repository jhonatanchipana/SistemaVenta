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
    public class ManufacturingRepository : IManufacturingRepository
    {
        private readonly ApplicationContext _context;

        public ManufacturingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Manufacturing>, int)> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.Manufacturing
                         where
                            (isActive.HasValue ? (a.IsActive == isActive ) : (a.IsActive == a.IsActive))
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public void Add(Manufacturing entity)
        {
            _context.Add(entity);
        }

        public async Task<Manufacturing?> GetById(int id)
        {
            return await _context.Manufacturing.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);
        }

        public void Update(Manufacturing entity)
        {
            _context.Update(entity);
        }

    }
}
