using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Entities;

namespace Venta.Data.Interfaces
{
    public interface ISizeRepository
    {
        Task<IEnumerable<Size>> GetAllByCategory();
    }
}
