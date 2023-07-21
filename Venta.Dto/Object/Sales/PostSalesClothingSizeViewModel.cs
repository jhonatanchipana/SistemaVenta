using System.ComponentModel.DataAnnotations;

namespace Venta.Dto.Object.Sales
{
    /// <summary>
    /// Representa el View Model para el registro y actualización de la entidad SalesClothing
    /// </summary>
    public class PostSalesClothingSizeViewModel
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ropa de la venta
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "Debe ingresar un valor mayor o igual a {1}")]
        public int ClothingId { get; set; }

        /// <summary>
        /// Id de Prenda talla
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Prenda Talla")]
        [Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "Debe ingresar un valor mayor o igual a {1}")]
        public int ClothingSizeId { get; set; }

        /// <summary>
        /// Nombre de la Prenda
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Prenda")]
        public string ClothingName { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad vendido
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Cantidad")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Debe ingresar solo números")]
        public int Quantity { get; set; }

        /// <summary>
        /// Precio vendido por Unidad
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Precio/Unidad")]
        public decimal PriceUnit { get; set; }

        /// <summary>
        /// Inversion por unidad
        /// </summary>        
        public decimal InvestmentUnit { get; set; }
    }
}