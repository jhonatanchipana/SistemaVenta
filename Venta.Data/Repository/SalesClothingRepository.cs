using Microsoft.EntityFrameworkCore;
using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Venta.Dto.Object.Sales;

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
                         select a).Include(x => x.Clothing).Include(x => x.ClothingSize);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<GetSalesClothingDTO>> GetAllBySalesIdDTO(int salesId)
        {
            var query = (from a in _context.SalesClothingSize
                         join b in _context.Clothing on a.ClothingId equals b.Id
                         join c in _context.ClothingSize on a.ClothingSizeId equals c.Id
                         join d in _context.Size on c.SizeId equals d.Id
                         where a.SalesId == salesId
                           && a.DeletionDate == null
                           && b.DeletionDate == null
                           && c.DeletionDate == null
                           && d.DeletionDate == null
                         select new GetSalesClothingDTO()
                         {
                             Id = a.Id,
                             ClothingId = b.Id,
                             ClothingName = b.Name,
                             Quantity = a.Quantity,
                             PriceUnit = a.PriceUnit,
                             SizeId = d.Id,
                             SizeName = d.Name
                         });

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
