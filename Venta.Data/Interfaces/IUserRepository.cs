using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Data.Interfaces
{
    public interface IUserRepository
    {
        void Add(User entity);
        Task<(IEnumerable<User>, int)> GetAll(string filter, bool? isActive, UserType userType, int offset, int limit, string sortBy, string orderBy);
        Task<User?> GetByEmailNormalized(string emailNormalized);
        Task<User?> GetById(int id);
        Task<User?> GetByUserNameNormalized(string userNameNormalized);
        void Update(User entity);
    }
}
