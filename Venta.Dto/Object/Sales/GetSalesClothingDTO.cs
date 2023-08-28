using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Sales
{
    public class GetSalesClothingDTO
    {
        public int Id { get; set; }
        public int ClothingId { get; set; }
        public string ClothingName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; } = string.Empty;
    }
}
