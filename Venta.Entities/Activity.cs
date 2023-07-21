using SistemaVenta.Entities.Enums;

namespace SistemaVenta.Entities
{
    /// <summary>
    /// Represenata una campaña
    /// </summary>
    public class Activity : BaseEntity
    {
        /// <summary>
        /// Nombre de la campaña
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de Inicio de la campaña
        /// </summary>
        public DateTime InitialDate { get; set; }

        /// <summary>
        /// Fecha fin de la campaña
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Id de la compra
        /// </summary>
        public int PurchaseId { get; set; }

        /// <summary>
        /// Fecha de la compra
        /// </summary>
        public Purchase? Purchase { get; set; }

        /// <summary>
        /// Estado de la campaña
        /// </summary>
        public StatusActivityType StatusActivityType { get; set; }
    }
}
