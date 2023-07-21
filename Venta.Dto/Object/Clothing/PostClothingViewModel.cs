using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [MaxLength(250, ErrorMessage = "El campo debe tener como máximo {0} caracteres")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripcion de la ropa
        /// </summary>
        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo debe tener como máximo {0} caracteres")]
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
        /// Listado de la categoria de la prenda
        /// </summary>
        public IEnumerable<SelectListItem> ClothingCategories { get; set; } = Enumerable.Empty<SelectListItem>();

        /// <summary>
        /// Stock de la ropa
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Lista de Tallas
        /// </summary>
        public IEnumerable<SelectListItem> Sizes { get; set; } = Enumerable.Empty<SelectListItem>();

        /// <summary>
        /// Talla de la ropa
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Talla")]
        public int[]? SizeIds { get; set; }

        /// <summary>
        /// Listado de Materiales usados
        /// </summary>
        public List<PostClothingMaterialViewModel> PostClothingMaterial { get; set; } = new List<PostClothingMaterialViewModel>();

    }
}
