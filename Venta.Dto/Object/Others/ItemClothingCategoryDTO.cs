using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Others
{
    public class ItemClothingCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int UnitMeasurementId { get; set; }
        public string UnitMeasurementDescription { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public int UnitQuantity { get; set; }
    }
}
