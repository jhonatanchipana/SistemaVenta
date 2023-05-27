using SistemaVenta.Entities.Enums;

namespace SistemaVenta.Entities
{
    /// <summary>
    /// Representa los materiales de la prenda
    /// </summary>
    public class Material : BaseEntity
    {
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
    }
}
