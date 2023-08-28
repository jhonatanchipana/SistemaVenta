using Microsoft.EntityFrameworkCore;
using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using Venta.Data.Interfaces;
using Venta.Data.Connection;
using System.Linq.Dynamic.Core;

namespace Venta.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<User>, int)> GetAll(string filter, bool? isActive, UserType userType, int offset, int limit, string sortBy, string orderBy)
        {
            var query = (from a in _context.User
                         where
                            (string.IsNullOrEmpty(filter) ||
                                a.UserName.ToUpper().Contains(filter.ToUpper()) || a.Email.ToUpper().Contains(filter.ToUpper())
                            )
                            &&
                            (userType == 0 || userType == a.UserType)
                            &&
                            (isActive.HasValue ? (a.IsActive == isActive) : (a.IsActive == a.IsActive))
                            &&
                            a.DeletionDate == null
                         select a).OrderBy($"{sortBy} {orderBy}");

            var totalRows = await query.CountAsync();
            var records = await query.Skip(offset).Take(limit).ToListAsync();

            return (records, totalRows);
        }

        public void Add(User entity)
        {
            _context.Add(entity);
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == id && x.DeletionDate == null);
        }

        public void Update(User entity)
        {
            _context.Update(entity);
        }

        public async Task<User?> GetByEmailNormalized(string emailNormalized)
        {
            var entity = (from a in _context.User
                          where a.DeletionDate == null 
                            && a.IsActive 
                            && a.NormalizedEmail.Equals(emailNormalized)
                          select a);

            return await entity.FirstOrDefaultAsync();
        }

        public async Task<User?> GetByUserNameNormalized(string userNameNormalized)
        {
            var entitylist = (from a in _context.User select a).ToList();

            var entity = (from a in _context.User
                          where a.DeletionDate == null
                            && a.IsActive
                            && a.NormalizedName.Equals(userNameNormalized)
                          select a);

            return await entity.FirstOrDefaultAsync();
        }

    }
}
