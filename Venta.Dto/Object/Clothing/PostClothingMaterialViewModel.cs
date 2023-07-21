using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.Clothing
{
    /// <summary>
    /// Representa el ViewModel de los materiales de la ropa
    /// </summary>
    public class PostClothingMaterialViewModel
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Material Id
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int MaterialId { get; set; }

        /// <summary>
        /// Nombre del Material
        /// </summary>
        public string MaterialName { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad de material para realizar la prenda
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Debe ingresar solo nùmeros")]
        public int Quantity { get; set; }
        
        /// <summary>
        /// Cost del material
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Unidad / Cantidad del material
        /// </summary>
        public int UnitQuantity { get; set; }

        /// <summary>
        /// Unidad de Medida del material
        /// </summary>
        public UnitMeasurementType UnitMeasurement { get; set; }

    }
}
