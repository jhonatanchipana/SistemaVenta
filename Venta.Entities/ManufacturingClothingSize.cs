using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Entities
{
    /// <summary>
    /// Representa le Detalle de la Fabricacion de prenda
    /// </summary>
    public class ManufacturingClothingSize : BaseEntity
    {
        /// <summary>
        /// Fabricacion Id
        /// </summary>
        public int ManufacturingId { get; set; }

        /// <summary>
        /// Fabricacion
        /// </summary>
        public Manufacturing? Manufacturing { get; set; }

        /// <summary>
        /// Ropa asociado a la clase
        /// </summary>
        public int ClothingId { get; set; }

        /// <summary>
        /// Ropa
        /// </summary>
        public Clothing? Clothing { get; set; }

        /// <summary>
        /// Ropa talla asociado a la clase
        /// </summary>
        public int? ClothingSizeId { get; set; }

        /// <summary>
        /// Ropa talla
        /// </summary>
        public ClothingSize? ClothingSize { get; set; }

        /// <summary>
        /// Cantidad fabricada por ropa
        /// </summary>
        public int Quantity { get; set; }
    }
}
