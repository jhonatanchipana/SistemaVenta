using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Campaign
{
    /// <summary>
    /// Representa DTO para el obtener campaña
    /// </summary>
    public class GetCampaignDTO
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// Usuario quien creo el registro
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Ultimo usuario quien modifico el registro
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ultima fecha de modificacion
        /// </summary>
        public DateTime? ModificationDate { get; set; }

        /// <summary>
        /// Indica si esta activo o inactivo
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Fecha de Eliminacion
        /// </summary>
        public DateTime? DeletionDate { get; set; }
    }
}
