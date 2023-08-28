using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.Cross;
using Venta.Dto.Object.Sales;

namespace Venta.Services.Interface
{
    public interface ISalesService
    {
        Task ChangeStatus(int id, bool isActive, string user);
        Task<int> Create(PostSalesViewModel model, string user);
        Task Delete(int id, string user);
        Task<ResultsDTO<GetListSalesDTO>> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy);
        Task<PostSalesViewModel> GetByIdPost(int id);
        Task<GetSalesDTO> GetById(int id);
        Task<int> Update(PostSalesViewModel model, string user);
    }
}
