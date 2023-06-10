using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object;
using Venta.Dto.Object.Campaign;

namespace Venta.Services.Interface
{
    public interface ICampaignService
    {
        Task ChangeStatus(int id, bool isActive, string user);
        Task<int> Create(PostCampaignViewModel modelo);
        Task Delete(int id, string user);
        Task<ResultsDTO<GetListCampaignDTO>> GetAll(string filter, bool? isActive, int statusCampaignType, int offset, int limit, string sortBy, string orderBy);
        Task<GetCampaignDTO> GetById(int id);
        Task<int> Update(PostCampaignViewModel modelo);
    }
}
