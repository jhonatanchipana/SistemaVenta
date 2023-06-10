namespace SistemaVenta.Entities
{
    /// <summary>
    /// Representa la compra de materiales
    /// </summary>
    public class Purchase : BaseEntity
    {
        /// <summary>
        /// Fecha que se realizo la compra
        /// </summary>
        public DateTime BuyDate { get; set; }

        /// <summary>
        /// Nombre quien realizo la compra
        /// </summary>
        public string? NameBuyer { get; set; }

        /// <summary>
        /// Cantidad total de materiales comprados
        /// </summary>
        public int QuantityMaterial { get; set; }

        /// <summary>
        /// Gasto Total de la compra
        /// </summary>
        public decimal CostTotal { get; set; }
    }
}
