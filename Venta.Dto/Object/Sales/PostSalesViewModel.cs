using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Sales
{
    /// <summary>
    /// Representa el View Model para el registro y actualización de la entidad sales
    /// </summary>
    public class PostSalesViewModel
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha de la venta
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Cantidad total de ropa de la venta
        /// </summary>
        [Display(Name = "Cantidad Total")]
        public int QuantityTotal { get; set; }

        /// <summary>
        /// Precio total de la venta
        /// </summary>
        public decimal PriceTotal { get; set; }

        /// <summary>
        /// Inversion total de la venta
        /// </summary>
        public decimal InvestmentTotal { get; set; }

        /// <summary>
        /// Lista de prendas asociado a la venta
        /// </summary>
        
        public List<PostSalesClothingSizeViewModel> PostSalesClothesSize { get; set; } = new List<PostSalesClothingSizeViewModel>();

    }
}
