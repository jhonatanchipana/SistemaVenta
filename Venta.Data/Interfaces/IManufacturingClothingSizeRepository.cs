using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.Manufacturing;
using Venta.Entities;

namespace Venta.Data.Interfaces
{
    public interface IManufacturingClothingSizeRepository
    {
        void Add(ManufacturingClothingSize entity);
        Task<IEnumerable<ManufacturingClothingSize>> GetAllByManufacturingId(int manufacturingId);
        Task<IEnumerable<GetManufacturingMaterialDTO>> GetAllByManufacturingIdDTO(int id);
        void Update(ManufacturingClothingSize entity);
    }
}
