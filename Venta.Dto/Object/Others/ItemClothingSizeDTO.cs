using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Others
{
    public class ItemClothingSizeDTO
    {
        /// <summary>
        /// Identificador de la prenda
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la Prenda
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Precio por Unidad de la prenda
        /// </summary>
        public decimal InvestmentUnit { get; set; }

        /// <summary>
        /// nombre de la Talla
        /// </summary>
        public string SizeName { get; set; } = string.Empty;

        /// <summary>
        /// Id de la talla
        /// </summary>
        public int ClothingSizeId { get; set; }
        
        /// <summary>
        /// Stock de la prenda por talla
        /// </summary>
        public int ClothingSizeStock { get; set; }
    }
}
