using SistemaVenta.Entities.Enums;

namespace SistemaVenta.Entities
{
    /// <summary>
    /// Represenata una campaña
    /// </summary>
    public class Campaign : BaseEntity
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
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Estado de la campaña
        /// </summary>
        public StatusCampaignType StatusCampaignType { get; set; }
    }
}
