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
    public class ClothingCategoryRepository : IClothingCategoryRepository
    {
        private readonly ApplicationContext _context;

        public ClothingCategoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClothingCategory>> GetAll()
        {
            var records = (from a in _context.ClothingCategory
                           where a.IsActive 
                                && a.DeletionDate == null 
                           orderby a.CreationDate descending
                           select a);

            return await records.ToListAsync();
        }

    }
}
