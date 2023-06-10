using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Venta.Dto.Object.Clothing
{
    /// <summary>
    /// Representa el View Model para el registro y actualización de la entidad prenda
    /// </summary>
    public class PostClothingViewModel
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la ropa
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        [MaxLength(150, ErrorMessage = "El campo debe tener como máximo {0} caracteres")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripcion de la ropa
        /// </summary>
        [Display(Name = "Descripción")]
        [MaxLength(250, ErrorMessage = "El campo debe tener como máximo {0} caracteres")]
        public string? Description { get; set; }

        /// <summary>
        /// Precio sugerido de la ropa
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Precio Sugerido")]
        public decimal PriceSuggested { get; set; }

        /// <summary>
        /// Categoria de la ropa
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Categoria")]
        public int ClothingCategoryId { get; set; }

        /// <summary>
        /// Stock de la ropa
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        /// <summary>
        /// Talla de la ropa
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Talla")]
        public ClothingSizeType Size { get; set; }

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
