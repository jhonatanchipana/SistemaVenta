using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Entities
{
    /// <summary>
    /// Representa el material para la prenda
    /// </summary>
    public class ClothingMaterial : BaseEntity
    {
        /// <summary>
        /// Material Id
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// Material 
        /// </summary>
        public Material? Material { get; set; }

        /// <summary>
        /// Prenda Id
        /// </summary>
        public int ClothingId { get; set; }

        /// <summary>
        /// Prenda 
        /// </summary>
        public Clothing? Clothing { get; set; }

        /// <summary>
        /// Cantidad de material para realizar la prenda
        /// </summary>
        public int Quantity { get; set; }   
    }
}
