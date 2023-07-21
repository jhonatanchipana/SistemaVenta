using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Manufacturing
{
    /// <summary>
    /// Representa el View Model para el registro y actualización para la entidad ManufacturingClothing
    /// </summary>
    public class PostManufacturingClothingViewModel
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int Id { get; set; }             

        /// <summary>
        /// Cantidad de prenda fabricado
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Cantidad")]
        [Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "Debe ingresar un valor mayor o igual a {1}")]
        public int Quantity { get; set; }

        /// <summary>
        /// Prenda talla asociado a la prenda
        /// </summary>
        [Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "Debe ingresar un valor mayor o igual a {1}")]
        public int ClothingSizeId { get; set; }

        /// <summary>
        /// Id de la prenda
        /// </summary>
        [Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "Debe ingresar un valor mayor o igual a {1}")]
        public int ClothingId { get; set; }

    }
}
