using System.ComponentModel;

namespace SistemaVenta.Entities.Enums
{
    /// <summary>
    /// Enumeracion asociado a la unidad de medida de una ropa
    /// </summary>
    public enum UnitMeasurementMaterialType
    {
        /// <summary>
        /// Valor asociado a Metro
        /// </summary>
        [Description("Metro")]
        Metro = 1,

        /// <summary>
        /// Valor asociado a Kilo
        /// </summary>
        [Description("Kilo")]
        Kilo = 2,

        /// <summary>
        /// Valor asociado a Bolsa
        /// </summary>
        [Description("Bolsa")]
        Bolsa = 3,

        /// <summary>
        /// Valor asociado a Yarda
        /// </summary>
        [Description("Yarda")]
        Yarda = 4

    }
}
