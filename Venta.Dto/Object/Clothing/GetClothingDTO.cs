using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Clothing
{
    /// <summary>
    /// Representa el DTO para el obtener prenda
    /// </summary>
    public class GetClothingDTO
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la ropa
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripcion de la ropa
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Precio sugerido de la ropa
        /// </summary>
        public decimal PriceSuggested { get; set; }

        /// <summary>
        /// Categoria de la ropa
        /// </summary>
        public int ClothingCategoryId { get; set; }

        /// <summary>
        /// Categoria de la ropa
        /// </summary>
        public string ClothingCategoryName { get; set; } = string.Empty;

        /// <summary>
        /// Stock de la ropa
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Talla de la ropa
        /// </summary>
        public ClothingSizeType Size { get; set; }

        /// <summary>
        /// Inversion por la ropa (unidad)
        /// </summary>
        public decimal InvestmentUnit { get; set; }       

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
