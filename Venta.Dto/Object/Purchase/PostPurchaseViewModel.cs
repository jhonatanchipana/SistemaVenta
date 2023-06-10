using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Purchase
{
    /// <summary>
    /// Representa el View Model para el registro y actualizacion de la compra de material
    /// </summary>
    public class PostPurchaseViewModel
    {
        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha que se realizo la compra
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha de Compra")]
        public DateTime BuyDate { get; set; } = DateTime.Today.Date;

        /// <summary>
        /// Nombre quien realizo la compra
        /// </summary>
        [Display(Name = "Nombre del comprador")]
        public string? NameBuyer { get; set; }

        /// <summary>
        /// Cantidad total de materiales comprados
        /// </summary>
        public int QuantityMaterial { get; set; }

        /// <summary>
        /// Gasto Total de la compra
        /// </summary>
        public decimal CostTotal { get; set; }

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
        /// Listado de los materiales comprados
        /// </summary>
        public List<PostPurchaseMaterialViewModel> PostBuyMaterialDetail { get; set; } =new List<PostPurchaseMaterialViewModel>();

    }
}
