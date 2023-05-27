using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Material
{
    /// <summary>
    /// Representa el View Model para el registro Material
    /// </summary>
    public class PostMaterialViewModel
    {
        /// <summary>
        /// Identificador de Material
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del material
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        [MaxLength(120, ErrorMessage = "El campo debe tener como máximo {0} caracteres")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripcion general del material
        /// </summary>
        [Display(Name = "Descripción")]
        [MaxLength(250, ErrorMessage = "El campo debe tener como máximo {0} caracteres")]
        public string? Description { get; set; }

        /// <summary>
        /// Costo aproximado del material
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Costo(Precio)")]
        public decimal Cost { get; set; }

        /// <summary>
        /// Unidad / Cantidad del material
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Unidad/Cantidad")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Debe ingresar solo nùmeros")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Debe ingresar un valor mayor a {0}")]
        public int UnitQuantity { get; set; }

        /// <summary>
        /// Unidad de medidad del material
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Unidad de Medida")]
        public int UnitMeasurement { get; set; }

        /// <summary>
        /// Stock del material
        /// </summary>
        [Display(Name = "Stock")]               
        public int Stock { get; set; }

        /// <summary>
        /// Usuario quien registro la entidad
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de creación de la entidad
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Usuario que modifico la entidad
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Fecha de la ultima modificación de la entidad
        /// </summary>
        public DateTime? ModificationDate { get; set; }
    }
}
