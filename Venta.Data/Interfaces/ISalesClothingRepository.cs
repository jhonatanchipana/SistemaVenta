using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface ISalesClothingRepository
    {
        void Add(SalesClothingSize entity);
        Task<IEnumerable<SalesClothingSize>> GetAllBySalesId(int salesId);
        void Update(SalesClothingSize entity);
    }
}
