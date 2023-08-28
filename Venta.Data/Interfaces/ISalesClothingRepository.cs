using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.Sales;

namespace Venta.Data.Interfaces
{
    public interface ISalesClothingRepository
    {
        void Add(SalesClothingSize entity);
        Task<IEnumerable<SalesClothingSize>> GetAllBySalesId(int salesId);
        Task<IEnumerable<GetSalesClothingDTO>> GetAllBySalesIdDTO(int salesId);
        void Update(SalesClothingSize entity);
    }
}
