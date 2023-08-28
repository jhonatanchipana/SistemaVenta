using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Manufacturing
{
    /// <summary>
    /// Representa el DTO 
    /// </summary>
    public class GetManufacturingMaterialDTO
    {
        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }
        public int? ClothingSizeId { get; set; }
        public int SizeId { get; set; }
        public int ClothingId { get; set; }
        public string ClothingName { get; set; }
        public string SizeName { get; set; }
    }
}
