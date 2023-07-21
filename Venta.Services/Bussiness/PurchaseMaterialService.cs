using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Interfaces;
using Venta.Dto.Object.Purchase;
using Venta.Services.Interface;

namespace Venta.Services.Bussiness
{
    public class PurchaseMaterialService : IPurchaseMaterialService
    {
        private readonly IPurchaseMaterialRepository _purchaseMaterialRepository;

        public PurchaseMaterialService(IPurchaseMaterialRepository purchaseMaterialRepository)
        {
            _purchaseMaterialRepository = purchaseMaterialRepository;
        }

        public async Task<IEnumerable<GetPurchaseMaterialDTO>> GetByPurchaseId(int id)
        {
            var records = await _purchaseMaterialRepository.GetAllByBuyMaterialId(id);

            var result = records.Select(x => new GetPurchaseMaterialDTO()
            {
                Id = x.Id,
                MaterialId = x.MaterialId,
                MaterialName = x.Material?.Name ?? string.Empty,
                Price = x.PriceUnit,
                Quantity = x.Quantity,
                CreateBy = x.CreateBy,
                CreationDate = x.CreationDate,
                ModifiedBy = x.ModifiedBy,
                ModificationDate = x.ModificationDate,
                IsActive = x.IsActive,
                DeletionDate = x.DeletionDate
            });

            return result;
        }

    }
}
