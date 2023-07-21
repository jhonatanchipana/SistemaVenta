using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Manufacturing
{
    /// <summary>
    /// Representa el View Model para el registro y actualización la entidad Manufacturing
    /// </summary>
    public class PostManufacturingViewModel
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha de la fabricacion
        /// </summary>        
        public DateTime ManufacturingDate { get; set; }

        /// <summary>
        /// Cantidad de ropa total
        /// </summary>
        public int QuantityTotal { get; set; }        

        /// <summary>
        /// Listado de la Prendas fabricadas
        /// </summary>
        public List<PostManufacturingClothingViewModel> PostManufacturingClothings { get; set; } = new List<PostManufacturingClothingViewModel>();  
    }
}
