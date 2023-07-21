using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Others
{
    /// <summary>
    /// Representa un item de la lista para los combos de UI
    /// </summary>
    public class ItemDTO
    {
        /// <summary>
        /// Identificador del elemento
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Descripcion del elemento
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
