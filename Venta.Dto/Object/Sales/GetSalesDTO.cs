using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Sales
{
    public class GetSalesDTO
    {
        public int Id { get; set; }
        public IEnumerable<GetSalesClothingDTO> SalesClothings { get; set; } = Enumerable.Empty<GetSalesClothingDTO>();
    }
}
