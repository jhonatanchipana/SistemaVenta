using Venta.Entities;

namespace SistemaVenta.Entities
{
    /// <summary>
    /// Representa el detalle de la venta
    /// </summary>
    public class SalesClothingSize : BaseEntity
    {
        /// <summary>
        /// Venta Id
        /// </summary>
        public int SalesId { get; set; }

        /// <summary>
        /// Venta
        /// </summary>
        public Sales? Sales { get; set; }

        /// <summary>
        /// Ropa de la venta
        /// </summary>
        public int ClothingId { get; set; }

        /// <summary>
        /// Ropa de la venta
        /// </summary>
        public Clothing? Clothing { get; set; }

        /// <summary>
        /// Id de la prenda talla
        /// </summary>
        public int? ClothingSizeId { get; set; }
        
        /// <summary>
        /// Prenda talla
        /// </summary>
        public ClothingSize? ClothingSize { get; set; }

        /// <summary>
        /// Cantidad vendido
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Precio vendido
        /// </summary>
        public decimal PriceUnit { get; set; }
    }
}
