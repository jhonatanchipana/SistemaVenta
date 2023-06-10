namespace SistemaVenta.Entities
{
    /// <summary>
    /// Representa una venta
    /// </summary>
    public class Sales : BaseEntity
    {
        /// <summary>
        /// Fecha de la venta
        /// </summary>
        public DateTime SaleDate { get; set; } 

        /// <summary>
        /// Cantidad total de ropa de la venta
        /// </summary>
        public int QuantityTotal { get; set; }

        /// <summary>
        /// Precio total de la venta
        /// </summary>
        public decimal PriceTotal { get; set; }

        /// <summary>
        /// Inversion total de la venta
        /// </summary>
        public decimal Investment { get; set; }

        /// <summary>
        /// Campaña Id
        /// </summary>
        public int CampaignId { get; set; }

        /// <summary>
        /// Campaña
        /// </summary>
        public Campaign? Campaign { get; set; }
    }
}
