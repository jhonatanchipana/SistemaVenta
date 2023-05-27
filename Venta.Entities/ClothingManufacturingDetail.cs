using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Entities
{
    public class ClothingManufacturingDetail : BaseEntity
    {
        /// <summary>
        /// Cantidad fabricada por ropa
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Ropa asociado a la clase
        /// </summary>
        public int ClothingId { get; set; }

        /// <summary>
        /// Ropa
        /// </summary>
        public Clothing? Clothing { get; set; }
    }
}
