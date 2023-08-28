using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Manufacturing
{
    public class GetManufacturingDTO
    {
        public int Id { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public int QuantityTotal { get; set; }
        public string CreateBy { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public IEnumerable<GetManufacturingMaterialDTO> PostManufacturingClothings { get; set; } = Enumerable.Empty<GetManufacturingMaterialDTO>();
    }
}
