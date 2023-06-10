using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Material
{
    /// <summary>
    /// Representa el DTO para el obtener Material
    /// </summary>
    public class GetMaterialDTO
    {
        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del material
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripcion general del material
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Costo aproximado del material
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Unidad / Cantidad del material
        /// </summary>
        public int UnitQuantity { get; set; }

        /// <summary>
        /// Unidad de medidad del material
        /// </summary>
        public UnitMeasurementType UnitMeasurement { get; set; }

        /// <summary>
        /// Stock del material
        /// </summary>
        public int Stock { get; set; }

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
