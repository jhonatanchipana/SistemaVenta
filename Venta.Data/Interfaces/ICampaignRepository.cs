using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface ICampaignRepository
    {
        void Add(Campaign entity);
        Task<(IEnumerable<Campaign>, int)> GetAll(string filter, bool? isActive, StatusCampaignType statusCampaignType, int offset, int limit, string sortBy, string orderBy);
        Task<Campaign?> GetById(int id);
        void Update(Campaign entity);
    }
}
