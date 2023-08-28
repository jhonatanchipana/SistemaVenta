using Microsoft.EntityFrameworkCore;
using SistemaVenta.Entities;
using System.Threading.Tasks.Dataflow;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Venta.Dto.Object.Manufacturing;
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
                         select a).Include(x => x.ClothingSize).Include(x => x.Clothing);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<GetManufacturingMaterialDTO>> GetAllByManufacturingIdDTO(int id)
        {
            var query = (from a in _context.Manufacturing
                         join b in _context.ManufacturingClothingSize on a.Id equals b.ManufacturingId
                         join c in _context.Clothing on b.ClothingId equals c.Id
                         join d in _context.ClothingSize on b.ClothingSizeId equals d.Id
                         join e in _context.Size on d.SizeId equals e.Id
                         where 
                            a.Id == id  
                            && b.DeletionDate == null
                            && c.DeletionDate == null
                            && d.DeletionDate == null
                            && e.DeletionDate == null
                         select new GetManufacturingMaterialDTO()
                         {
                             Id = b.Id,
                             Quantity = b.Quantity,
                             ClothingSizeId = d.Id,
                             SizeId = e.Id,
                             SizeName = e.Name,
                             ClothingId = c.Id,
                             ClothingName = c.Name,
                         });

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
