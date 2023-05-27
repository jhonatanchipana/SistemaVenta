using System.ComponentModel;

namespace SistemaVenta.Entities.Enums
{
    /// <summary>
    /// Enumeracion asociado a la talla de una ropa
    /// </summary>
    public enum ClothingSizeType
    {
        /// <summary>
        /// Talla pequeña
        /// </summary>
        [Description("S")]
        Small = 1,

        /// <summary>
        /// Talla Mediano
        /// </summary>
        [Description("M")]
        Medium = 2,

        /// <summary>
        /// Talla Larga
        /// </summary>
        [Description("L")]
        Large = 3
    }
}
