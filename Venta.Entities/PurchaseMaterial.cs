namespace SistemaVenta.Entities
{
    /// <summary>
    /// Representa el detalle de una compra de materiales
    /// </summary>
    public class PurchaseMaterial : BaseEntity
    {
        /// <summary>
        /// Cabecera de la Compra
        /// </summary>
        public int PurchaseId { get; set; }

        /// <summary>
        /// Cabecera de la Compra
        /// </summary>
        public Purchase? Purchase { get; set; }

        /// <summary>
        /// Material comprado
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// Material comprado
        /// </summary>
        public Material? Material { get; set; }

        /// <summary>
        /// Cantidad de Material Comprado
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Precio del Material
        /// </summary>
        public decimal Price { get; set; }
    }
}
