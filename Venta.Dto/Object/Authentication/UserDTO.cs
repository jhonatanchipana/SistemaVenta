using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Authentication
{
    public  class UserDTO
    {
        /// <summary>
        /// Identificador del usurio
        /// </summary>
        public int Id { get; set; }

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
        /// Email normalizado para el usuario
        /// </summary>
        public string EmailNormalizado { get; set; } = string.Empty;

        /// <summary>
        /// Password encriptado para el usuario
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
    }
}
