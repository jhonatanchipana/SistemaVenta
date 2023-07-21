using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Interfaces;
using Venta.Dto.Object.ClothingSize;
using Venta.Dto.Object.Others;
using Venta.Services.Interface;

namespace Venta.Services.Bussiness
{
    public class ClothingSizeService : IClothingSizeService
    {
        private readonly IClothingSizeRepository _clothingSizeRepository;

        public ClothingSizeService(IClothingSizeRepository clothingSizeRepository)
        {
            _clothingSizeRepository = clothingSizeRepository;
        }

        public async Task<IEnumerable<GetClothingSizeDTO>> GetListByClothingId(int clothingId)
        {
            var records = await _clothingSizeRepository.GetAllByClothingId(clothingId);

            var result = records.Select(x => new GetClothingSizeDTO
            {
                Id = x.Id,
                ClothingId = x.ClothingId,
                SizeId = x.SizeId,
                SizeName = x.Size?.Name ?? string.Empty,
                Stock = x.Stock,
                CreateBy = x.CreateBy,
                CreationDate = x.CreationDate,
                ModifiedBy = x.ModifiedBy,
                ModificationDate = x.ModificationDate,
                IsActive = x.IsActive,
                DeletionDate = x.DeletionDate
            });

            return result;

        }
    
        public async Task<IEnumerable<ItemClothingSizeDTO>> GetAutocomplete(string filter, int limit)
        {
            return await _clothingSizeRepository.GetAll(filter, limit);  
        }
    }
}
