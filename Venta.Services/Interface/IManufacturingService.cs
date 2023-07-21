using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.Cross;
using Venta.Dto.Object.Manufacturing;

namespace Venta.Services.Interface
{
    public interface IManufacturingService
    {
        Task ChangeStatus(int id, bool isActive, string user);
        Task<int> Create(PostManufacturingViewModel model, string user);
        Task Delete(int id, string user);
        Task<ResultsDTO<GetListManufacturingDTO>> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy);
        Task<PostManufacturingViewModel> GetById(int id);
        Task<int> Update(PostManufacturingViewModel model, string user);
    }
}
