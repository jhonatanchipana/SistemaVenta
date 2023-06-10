using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object;
using Venta.Dto.Object.Purchase;

namespace Venta.Services.Interface
{
    public interface IPurchaseService
    {
        Task ChangeStatus(int id, bool isActive, string user);
        Task<int> Create(PostPurchaseViewModel modelo);
        Task Delete(int id, string user);
        Task<ResultsDTO<GetListPurchaseDTO>> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy);
        Task<GetPurchaseDTO> GetById(int id);
        Task<int> Update(PostPurchaseViewModel modelo);
    }
}
