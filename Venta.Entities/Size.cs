using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Entities
{
    /// <summary>
    /// Representa la Talla
    /// </summary>
    public class Size : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Descripción { get; set; } = string.Empty;
    }
}
