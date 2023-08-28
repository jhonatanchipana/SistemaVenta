using System.ComponentModel;

namespace SistemaVenta.Entities.Enums
{
    /// <summary>
    /// Enumeracion asociado a la unidad de medida de una ropa
    /// </summary>
    public enum UnitMeasurementType
    {
        /// <summary>
        /// Valor asociado a Centímetro
        /// </summary>
        [Description("Centímetro")]
        Centímetro = 1,

        /// <summary>
        /// Valor asociado a Kilogramo
        /// </summary>
        [Description("Kilogramo")]
        Kilogramo = 2,

        /// <summary>
        /// Valor asociado a Unidad
        /// </summary>
        [Description("Unidad")]
        Unidad = 3
    }
}
