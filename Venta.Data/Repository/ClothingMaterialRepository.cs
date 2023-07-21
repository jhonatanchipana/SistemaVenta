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
    public class ClothingMaterialRepository : IClothingMaterialRepository
    {
        private readonly ApplicationContext _context;

        public ClothingMaterialRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClothingMaterial>> GetAllByClothingId(int clothingId)
        {
            var query = (from a in _context.ClothingMaterial
                          where a.ClothingId == clothingId
                            && a.IsActive
                            && a.DeletionDate == null                            
                          select a).Include(x => x.Material);

            return await query.ToListAsync();
        }

        public void Add(ClothingMaterial entity)
        {
            _context.Add(entity);
        }

        public void Update(ClothingMaterial entity)
        {
            _context.Update(entity);
        }

    }
}
