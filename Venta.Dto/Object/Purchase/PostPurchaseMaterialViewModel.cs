using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Purchase
{
    /// <summary>
    /// Representa el View Model para el registro de compra Material detalle
    /// </summary>
    public class PostPurchaseMaterialViewModel
    {
        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        public int Id { get; set; } 

        /// <summary>
        /// Id del Material comprado
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// Nombre del Material
        /// </summary>
        public string MaterialName { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad de Material Comprado
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Debe ingresar solo nùmeros")]
        [Display(Name = "Cantidad")]
        public int? Quantity { get; set; }

        /// <summary>
        /// Precio del Material
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression("(^[0-9.]+$)", ErrorMessage = "Debe ingresar solo nùmeros")]
        [Display(Name = "Precio")]
        public decimal? Price { get; set; }

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
