using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Venta.Dto.Object.ClothingSize;
using Venta.Dto.Object.Others;
using Venta.Entities;

namespace Venta.Data.Repository
{
    public class ClothingSizeRepository : IClothingSizeRepository
    {
        private readonly ApplicationContext _context;

        public ClothingSizeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClothingSize>> GetAllByClothingId(int clothingId)
        {
            var query = (from a in _context.ClothingSize
                        where
                            a.ClothingId == clothingId
                            &&
                            a.IsActive
                            &&
                            a.DeletionDate == null
                         select a).Include(x => x.Size);

            var records = await query.ToListAsync();

            return records;
        }

        public async Task<IEnumerable<ClothingSize>> GetAllByIds(int[] ids)
        {
            var query = (from a in _context.ClothingSize
                         where ids.Contains(a.Id)
                            && a.IsActive
                            && a.DeletionDate == null
                         select a).Include(x => x.Clothing);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<ItemClothingSizeDTO>> GetAll(string filter, int limit)
        {
            var query = (from a in _context.Clothing
                         join b in _context.ClothingSize on a.Id equals b.ClothingId
                         join c in _context.Size on b.SizeId equals c.Id                         
                         where a.IsActive && b.IsActive && c.IsActive
                            && a.DeletionDate == null
                            && b.DeletionDate == null
                            && c.DeletionDate == null
                            && (string.IsNullOrEmpty(filter) || a.Name.ToUpper().Contains(filter.ToUpper()) )
                         orderby a.Name descending
                         select new ItemClothingSizeDTO()
                         {
                             Id = a.Id,
                             Name = a.Name,
                             InvestmentUnit = a.InvestmentUnit,
                             ClothingSizeId = b.Id,
                             SizeName = c.Name,
                             ClothingSizeStock = b.Stock
                         });

            return await query.Take(limit).ToListAsync();
        }

        public async Task<ClothingSize?> GetById(int id)
        {
            var entity = (from a in _context.ClothingSize
                          where a.Id == id
                            && a.DeletionDate == null
                          select a).Include(x => x.Clothing);

            return await entity.FirstOrDefaultAsync();
        }

        public void Add(ClothingSize entity)
        {
            _context.Add(entity);
        }

        public void Update(ClothingSize entity)
        {
            _context.Update(entity);
        }
    }
}
