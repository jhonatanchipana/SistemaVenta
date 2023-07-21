using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Sales
{
    /// <summary>
    /// Representa el DTO para la entidad sales
    /// </summary>
    public class GetListSalesDTO
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
        public int QuantityTotal { get; set; }

        /// <summary>
        /// Precio total de la venta
        /// </summary>
        public decimal PriceTotal { get; set; }

        /// <summary>
        /// Inversion total de la venta
        /// </summary>
        public decimal Investment { get; set; }

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
        /// Indica si esta activo o inactivo
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Fecha de Eliminacion
        /// </summary>
        public DateTime? DeletionDate { get; set; }
    }
}
