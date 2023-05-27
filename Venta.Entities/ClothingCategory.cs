namespace SistemaVenta.Entities
{
    /// <summary>
    /// Representa la Categoria de un ropa
    /// </summary>
    public class ClothingCategory : BaseEntity
    {
        /// <summary>
        /// Nombre de la categoria
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripcion de la categoria
        /// </summary>
        public string? Description { get; set; }
    }
}
