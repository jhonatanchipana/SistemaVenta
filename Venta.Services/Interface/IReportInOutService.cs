using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.ReportInOut;
using Venta.Dto.Object.Cross;

namespace Venta.Services.Interface
{
    public interface IReportInOutService
    {
        Task ChangeStatus(int id, bool isActive, string user);
        Task<int> Create(PostReportInOutViewModel model, string user);
        Task Delete(int id, string user);
        Task<ResultsDTO<GetListReportInOutDTO>> GetAll(string filter, bool? isActive, int statusActivityType, int offset, int limit, string sortBy, string orderBy);
        Task<PostReportInOutViewModel> GetById(int id);
        Task<GetReportInOutFinalDTO> GetInOut(int id);
        Task<int> Update(PostReportInOutViewModel model, string user);
    }
}
