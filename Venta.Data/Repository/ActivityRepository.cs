using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Venta.Data.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationContext _context;

        public ActivityRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Activity>, int)> GetAll(string filter, bool? isActive, StatusActivityType statusActivityType, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.Activity
                         where
                            (string.IsNullOrEmpty(filter) ||
                                a.Name.ToUpper().Contains(filter.ToUpper())                                  
                            )
                            &&
                            (statusActivityType == 0 || statusActivityType == a.StatusActivityType)
                            &&
                            (isActive.HasValue ? (a.IsActive == isActive) : (a.IsActive == a.IsActive))
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public void Add(Activity entity)
        {
            _context.Add(entity);
        }

        public async Task<Activity?> GetById(int id)
        {
            return await _context.Activity.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);
        }

        public void Update(Activity entity)
        {
            _context.Update(entity);
        }

    }
}
