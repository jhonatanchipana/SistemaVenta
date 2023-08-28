using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Purchase
{
    /// <summary>
    /// Representa el DTO par obtener la compra de material detalle
    /// </summary>
    public class GetPurchaseMaterialDTO
    {
        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id del Material comprado
        /// </summary>
        public int MaterialId { get; set; }              

        /// <summary>
        /// Nombre del Material comprado
        /// </summary>
        public string MaterialName { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad de Material Comprado
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Precio del Material
        /// </summary>
        public decimal Price { get; set; }


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
