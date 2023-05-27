using SistemaVenta.Entities.Enums;

namespace SistemaVenta.Entities
{
    /// <summary>
    /// Representa al usuario
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Nombre de usuario para logearse al sistema
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de usuario normalizado
        /// </summary>
        public string NormalizedName { get; set; } = string.Empty;

        /// <summary>
        /// Email del usuario
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Email normalizado del usuario
        /// </summary>
        public string NormalizedEmail { get; set; } = string.Empty;

        /// <summary>
        /// Password Encriptado del usuario
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de Usuario
        /// </summary>
        public UserType UserType { get; set; }
    }
}
