using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.Cross;
using Venta.Dto.Object.Material;
using Venta.Dto.Object.Others;

namespace Venta.Services.Interface
{
    public interface IMaterialService
    {
        Task ChangeStatus(int id, bool isActive, string user);
        Task<int> Create(PostMaterialViewModel material, string user);
        Task Delete(int id, string user);
        Task<ResultsDTO<GetListMaterialDTO>> GetAll(string filter, bool? isActive, int unitMeasurement, int offset, int limit, string sortBy, string orderBy);
        Task<IEnumerable<ItemClothingCategoryDTO>> GetAutocomplete(string filter, int limit, int[] ignoreIds);
        Task<GetMaterialDTO> GetById(int id);
        Task<int> Update(PostMaterialViewModel material, string user);
        Task<bool> ValidateMaterialInUse(int id);
    }
}
