using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object;
using Venta.Dto.Object.Material;

namespace Venta.Services.Interface
{
    public interface IServiceMaterial
    {
        Task<int> Create(PostMaterialViewModel material);
        Task Delete(int id, string user);
        Task<ResultsDTO<GetListMaterialDTO>> GetAll(string filter, bool? isActive, int unitMeasurement, int offset, int limit, string sortBy, string orderBy);
        Task<GetMaterialDTO> GetById(int id);
        Task<int> Update(PostMaterialViewModel material);
    }
}
