namespace SistemaVenta.Entities
{
    /// <summary>
    /// Representa el modelo de la ropa
    /// </summary>
    public class ClothingModel : BaseEntity
    {
        /// <summary>
        /// Nombre del modelo
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripcion del modelo
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Categoria asociado al modelo
        /// </summary>
        public int ClothingCategoryId { get; set; } 

        /// <summary>
        /// Categoria asociado al modelo
        /// </summary>
        public ClothingCategory? ClothingCategory { get; set; } 
    }
}
