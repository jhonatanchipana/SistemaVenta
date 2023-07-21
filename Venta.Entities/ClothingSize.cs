using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Entities
{
    /// <summary>
    /// Representa la entidad Prenda Talla
    /// </summary>
    public class ClothingSize : BaseEntity
    {
        /// <summary>
        /// Prenda asociado a la entidad
        /// </summary>
        public Clothing? Clothing { get; set; }

        /// <summary>
        /// Prenda id asociado a la entidad
        /// </summary>
        public int ClothingId { get; set; }

        /// <summary>
        /// Talla
        /// </summary>
        public Size? Size { get; set; }

        /// <summary>
        /// Talla id
        /// </summary>
        public int SizeId { get; set; }

        /// <summary>
        /// Stock por talla
        /// </summary>
        public int Stock { get; set; }
    }
}
