using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.Activity;
using Venta.Dto.Object.Cross;

namespace Venta.Services.Interface
{
    public interface IActivityService
    {
        Task ChangeStatus(int id, bool isActive, string user);
        Task<int> Create(PostActivityViewModel model, string user);
        Task Delete(int id, string user);
        Task<ResultsDTO<GetListActivityDTO>> GetAll(string filter, bool? isActive, int statusActivityType, int offset, int limit, string sortBy, string orderBy);
        Task<PostActivityViewModel> GetById(int id);
        Task<int> Update(PostActivityViewModel model, string user);
    }
}
