using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Interfaces;
using Venta.Services.Interface;

namespace Venta.Services.Bussiness
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public async Task<IEnumerable<SelectListItem>> GetCombo()
        {
            var records = await _sizeRepository.GetAllByCategory();
            var results = records.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });

            return results;
        }

    }
}
