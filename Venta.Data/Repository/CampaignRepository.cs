using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Venta.Data.Repository
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationContext _context;

        public CampaignRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Campaign>, int)> GetAll(string filter, bool? isActive, StatusCampaignType statusCampaignType, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.Campaign
                         where
                            (string.IsNullOrEmpty(filter) ||
                                a.Name.ToUpper().Contains(filter.ToUpper())                                  
                            )
                            &&
                            (statusCampaignType == 0 || statusCampaignType == a.StatusCampaignType)
                            &&
                            (isActive.HasValue ? (a.IsActive == isActive) : (a.IsActive == a.IsActive))
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public void Add(Campaign entity)
        {
            _context.Add(entity);
        }

        public async Task<Campaign?> GetById(int id)
        {
            return await _context.Campaign.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);
        }

        public void Update(Campaign entity)
        {
            _context.Update(entity);
        }

        public void ChangeStatus(int id, bool isActive, string user)
        {
            var entity = _context.Campaign.FirstOrDefault(x => x.Id == id && x.DeletionDate == null);
            if (entity != null)
            {
                entity.IsActive = isActive;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.UtcNow;
                _context.Update(entity);
            }
        }

        public void Delete(int id, string user)
        {
            var entity = _context.Campaign.FirstOrDefault(x => x.Id == id && x.DeletionDate == null);
            if (entity != null)
            {
                entity.DeletionDate = DateTime.UtcNow;
                _context.Update(entity);
            }
        }
    }
}
