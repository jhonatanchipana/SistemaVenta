using System.ComponentModel;

namespace SistemaVenta.Entities.Enums
{
    /// <summary>
    /// Enumeracion asociado a la unidad de medida de una ropa
    /// </summary>
    public enum UnitMeasurementType
    {
        /// <summary>
        /// Valor asociado a Kilogramo
        /// </summary>
        [Description("Kilogramo")]
        Kilogramo = 1,

        /// <summary>
        /// Valor asociado a Metro
        /// </summary>
        [Description("Metro")]
        Metro = 2
    }
}
