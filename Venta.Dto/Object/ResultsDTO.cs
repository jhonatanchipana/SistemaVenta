using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object
{
    public class ResultsDTO<T> where T : class
    {
        public IEnumerable<T>? Results { get; set; }
        public int Rows { get; set; }
    }
}
