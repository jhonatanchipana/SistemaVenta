using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.ClothingSize
{
    /// <summary>
    /// Representa el DTO para el obtener tallas de prendas
    /// </summary>
    public class GetClothingSizeDTO
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id de la prenda asociado
        /// </summary>
        public int ClothingId { get; set; }

        /// <summary>
        /// Talla id
        /// </summary>
        public int SizeId { get; set; }

        /// <summary>
        /// Nombre de la Talla
        /// </summary>
        public string SizeName { get; set; } = string.Empty;

        /// <summary>
        /// Stock por talla
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Cantidad de agregado
        /// </summary>
        public int Quantity { get; set; }

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
