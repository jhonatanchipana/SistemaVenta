using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Venta.Entities;

namespace Venta.Data.Repository
{
    public class SizeRepository : ISizeRepository
    {
        private readonly ApplicationContext _context;

        public SizeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Size>> GetAllByCategory()
        {
            var result = (from a in _context.Size
                          where a.IsActive
                          && a.DeletionDate == null
                          select a);
            
            return await result.ToListAsync();
        }

    }
}
