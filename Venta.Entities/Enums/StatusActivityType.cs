using System.ComponentModel;

namespace SistemaVenta.Entities.Enums
{
    /// <summary>
    /// Enumeracion asociado al estado de una campaña
    /// </summary>
    public enum StatusActivityType
    {
        [Description("Inicial")]
        Initial = 1,

        [Description("En Proceso")]
        InProgess = 2,

        [Description("Finalizado")]
        Terminated = 3
    }
}
