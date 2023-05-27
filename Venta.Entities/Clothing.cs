using SistemaVenta.Entities.Enums;

namespace SistemaVenta.Entities
{
    /// <summary>
    /// Representa una ropa
    /// </summary>
    public class Clothing : BaseEntity
    {
        /// <summary>
        /// Nombre de la ropa
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripcion de la ropa
        /// </summary>
        public string? Descripcion { get; set; }

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
        public ClothingCategory? ClothingCategory { get; set; }
        
        /// <summary>
        /// Modelo de la ropa
        /// </summary>
        public int ClothingModelId { get; set; }

        /// <summary>
        /// Modelo de la ropa
        /// </summary>
        //public ClothingModel? ClothingModel { get; set; }

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
    }
}
