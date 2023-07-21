using Microsoft.EntityFrameworkCore;
using SistemaVenta.Entities;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Venta.Entities;

namespace Venta.Data.Repository
{
    public class ManufacturingClothingSizeRepository : IManufacturingClothingSizeRepository
    {
        private readonly ApplicationContext _context;

        public ManufacturingClothingSizeRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<IEnumerable<ManufacturingClothingSize>> GetAllByManufacturingId(int manufacturingId)
        {
            var query = (from a in _context.ManufacturingClothingSize
                         where a.ManufacturingId == manufacturingId
                           && a.IsActive
                           && a.DeletionDate == null
                         select a).Include(x => x.ClothingSize);

            return await query.ToListAsync();
        }

        public void Add(ManufacturingClothingSize entity)
        {
            _context.Add(entity);
        }

        public void Update(ManufacturingClothingSize entity)
        {
            _context.Update(entity);
        }


    }
}
