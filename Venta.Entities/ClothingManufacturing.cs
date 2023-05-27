
namespace SistemaVenta.Entities
{
    /// <summary>
    /// Representa la Fabricacion de la ropa
    /// </summary>
    public class ClothingManufacturing : BaseEntity
    {
        /// <summary>
        /// Fecha de la fabricacion
        /// </summary>
        public DateTime ManufacturingDate { get; set; }

        /// <summary>
        /// Cantidad de ropa total
        /// </summary>
        public int QuantityTotal { get; set; }
    }
}
