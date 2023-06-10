using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Interfaces;
using Venta.Data.Repository;
using Venta.Dto.Object.Material;
using Venta.Dto.Object;
using Venta.Services.Interface;
using Venta.Dto.Object.Clothing;

namespace Venta.Services.Bussiness
{
    public class ClothingService : IClothingService
    {
        private readonly IClothingRepository _clothingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClothingService(IClothingRepository clothingRepository,
            IUnitOfWork unitOfWork)
        {
            _clothingRepository = clothingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultsDTO<GetListClothingDTO>> GetAll(string filter, bool? isActive, int clothingSizeType, int? clothingCategoryId, int offset, int limit, string sortBy, string orderBy)
        {
            try
            {
                var tuple = await _clothingRepository.GetAll(filter, isActive, (ClothingSizeType)clothingSizeType, clothingCategoryId, offset, limit, sortBy, orderBy);

                var records = tuple.Item1
                            .Select(a => new GetListClothingDTO
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Descripcion = a.Description,
                                ClothingCategoryId = a.ClothingCategoryId,
                                ClothingCategoryName = a.ClothingCategory?.Name ?? string.Empty,
                                Size= a.Size,
                                PriceSuggested= a.PriceSuggested,
                                InvestmentUnit = a.InvestmentUnit,
                                Stock = a.Stock,
                                CreateBy = a.CreateBy,
                                CreationDate = a.CreationDate,
                                ModifiedBy = a.ModifiedBy,
                                ModificationDate = a.ModificationDate,
                                IsActive = a.IsActive,
                                DeletionDate = a.DeletionDate
                            }).ToList();

                var result = new ResultsDTO<GetListClothingDTO>()
                {
                    Results = records,
                    Rows = tuple.Item2
                };

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar Prenda");
            }
        }

        public async Task<int> Create(PostClothingViewModel material)
        {

            try
            {
                var entity = new Clothing()
                {
                    Id = material.Id,
                    Name = material.Name,
                    Description = material.Description,
                    Size = material.Size,
                    ClothingCategory = new ClothingCategory() { Id= material.ClothingCategoryId },
                    ClothingCategoryId = material.ClothingCategoryId,
                    //ClothingModelId = material.ClothingCategoryId,
                    InvestmentUnit = 0,
                    PriceSuggested = material.PriceSuggested,
                    Stock = material.Stock,
                    CreateBy = material.CreateBy,
                    CreationDate = material.CreationDate,
                    ModifiedBy = null,
                    ModificationDate = null,
                    IsActive = true,
                    DeletionDate = null
                };

                _clothingRepository.Add(entity);
                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar Prenda");
            }
        }

        public async Task<int> Update(PostClothingViewModel material)
        {
            var entity = await _clothingRepository.GetById(material.Id);
            if (entity == null) throw new Exception("La Prenda no existe");

            try
            {

                entity.Id = material.Id;
                entity.Name = material.Name;
                entity.Description = material.Description;
                entity.Size = material.Size;
                entity.ClothingCategory = new ClothingCategory() { Id = material.ClothingCategoryId };
                entity.ClothingCategoryId = material.ClothingCategoryId;
                entity.InvestmentUnit = 0;
                entity.PriceSuggested = material.PriceSuggested;                
                entity.Stock = material.Stock;
                entity.ModifiedBy = material.ModifiedBy;
                entity.ModificationDate = DateTime.UtcNow;

                _clothingRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar Prenda");
            }
        }


        public async Task<GetClothingDTO> GetById(int id)
        {
            var entity = await _clothingRepository.GetById(id);
            if (entity == null) throw new Exception("La Prenda no existe");

            try
            {
                var material = new GetClothingDTO
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    Size= entity.Size,
                    ClothingCategoryId= entity.ClothingCategoryId,  
                    ClothingCategoryName = entity.ClothingCategory?.Name ?? string.Empty,
                    PriceSuggested= entity.PriceSuggested,
                    InvestmentUnit= entity.InvestmentUnit,
                    Stock = entity.Stock,
                    CreateBy = entity.CreateBy,
                    CreationDate = entity.CreationDate,
                    ModifiedBy = entity.ModifiedBy,
                    ModificationDate = entity.ModificationDate,
                    IsActive = entity.IsActive,
                    DeletionDate = entity.DeletionDate
                };

                return material;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Prenda");
            }
        }

        public async Task ChangeStatus(int id, bool isActive, string user)
        {
            var entity = await _clothingRepository.GetById(id);
            if (entity == null) throw new Exception("La Prenda no existe");

            try
            {
                entity.IsActive = isActive;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.UtcNow;

                _clothingRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Cambiar estado de la Prenda");
            }
        }

        public async Task Delete(int id, string user)
        {
            var entity = await _clothingRepository.GetById(id);
            if (entity == null) throw new Exception("La Prenda no existe");

            try
            {
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.UtcNow;
                entity.DeletionDate = DateTime.UtcNow;

                _clothingRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la Prenda");
            }
        }

    }
}
