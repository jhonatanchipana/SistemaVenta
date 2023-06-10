using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Campaign
{
    /// <summary>
    /// Representa el View Model para el registro y actualización de la entidad campaña
    /// </summary>
    public class PostCampaignViewModel
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>        
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la campaña
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        [MaxLength(120, ErrorMessage = "El campo debe tener como máximo {0} caracteres")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de Inicio de la campaña
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Date)]
        public DateTime InitialDate { get; set; }

        /// <summary>
        /// Fecha fin de la campaña
        /// </summary>
        [Display(Name = "Fecha Fin")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

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

    }
}
