using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.Clothing;
using Venta.Dto.Object.Cross;
using Venta.Dto.Object.Others;

namespace Venta.Services.Interface
{
    public interface IClothingService
    {
        Task ChangeStatus(int id, bool isActive, string user);
        Task<int> Create(PostClothingViewModel material, string user);
        Task Delete(int id, string user);
        Task<ResultsDTO<GetListClothingDTO>> GetAll(string filter, bool? isActive, int? clothingCategoryId, int offset, int limit, string sortBy, string orderBy);
        Task<IEnumerable<ItemDTO>> GetAutocomplete(string filter, int limit);
        Task<IEnumerable<ItemClothingSizeDTO>> GetAutocompleteSize(string filter, int limit);
        Task<PostClothingViewModel> GetById(int id);
        Task<int> Update(PostClothingViewModel material, string user);
        Task<bool> ValidateClothingInUse(int id);
    }
}
