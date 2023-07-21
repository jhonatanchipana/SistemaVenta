using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Interfaces;
using Venta.Services.Interface;

namespace Venta.Services.Bussiness
{
    public class ClothingCategoryService : IClothingCategoryService
    {
        private readonly IClothingCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ClothingCategoryService(IClothingCategoryRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SelectListItem>> GetCombo()
        {
            var records = await _repository.GetAll();

            var results = records.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return results;
        }
    }
}
