using Microsoft.EntityFrameworkCore;
using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Connection;
using Venta.Data.Interfaces;

namespace Venta.Data.Repository
{
    public class SalesClothingRepository : ISalesClothingRepository
    {
        private readonly ApplicationContext _context;

        public SalesClothingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesClothingSize>> GetAllBySalesId(int salesId)
        {
            var query = (from a in _context.SalesClothingSize
                         where a.SalesId == salesId
                           && a.IsActive
                           && a.DeletionDate == null
                         select a).Include(x => x.Clothing);

            return await query.ToListAsync();
        }

        public void Add(SalesClothingSize entity)
        {
            _context.Add(entity);
        }

        public void Update(SalesClothingSize entity)
        {
            _context.Update(entity);
        }
    }
}
