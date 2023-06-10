using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Purchase
{
    /// <summary>
    /// Representa el DTO de obtener la compra de material
    /// </summary>
    public class GetPurchaseDTO
    {
        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha que se realizo la compra
        /// </summary>
        public DateTime BuyDate { get; set; }

        /// <summary>
        /// Nombre quien realizo la compra
        /// </summary>
        public string? NameBuyer { get; set; }

        /// <summary>
        /// Cantidad total de materiales comprados
        /// </summary>
        public int QuantityMaterial { get; set; }

        /// <summary>
        /// Gasto Total de la compra
        /// </summary>
        public decimal CostTotal { get; set; }

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

        /// <summary>
        /// Listado de los materiales comprados
        /// </summary>
        public IEnumerable<GetPurchaseMaterialDTO> BuyMaterialDetailDTO { get; set; } = Enumerable.Empty<GetPurchaseMaterialDTO>();
    }
}
