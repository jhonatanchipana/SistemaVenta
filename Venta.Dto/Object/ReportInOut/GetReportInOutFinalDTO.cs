using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Dto.Object.ReportInOut
{
    /// <summary>
    /// Representa el DTO para el resultado del reporte ingreso/salida
    /// </summary>
    public class GetReportInOutFinalDTO
    {
        public decimal PurchaseTotal { get; set; }
        public decimal SaleTotal { get; set; }
    }
}
