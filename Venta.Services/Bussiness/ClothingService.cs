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
using Venta.Services.Interface;
using Venta.Dto.Object.Clothing;
using Venta.Dto.Object.Cross;
using Venta.Entities;
using LaTinka.Common;
using Venta.Dto.Object.Others;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Venta.Services.Bussiness
{
    public class ClothingService : IClothingService
    {
        private readonly IClothingRepository _clothingRepository;
        private readonly IClothingMaterialRepository _clothingMaterialRepository;
        private readonly IClothingSizeRepository _clothingSizeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClothingService(IClothingRepository clothingRepository,
            IClothingMaterialRepository clothingMaterialRepository,
            IClothingSizeRepository clothingSizeRepository,
            IUnitOfWork unitOfWork)
        {
            _clothingRepository = clothingRepository;
            _clothingMaterialRepository = clothingMaterialRepository;
            _clothingSizeRepository = clothingSizeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultsDTO<GetListClothingDTO>> GetAll(string filter, bool? isActive, int? clothingCategoryId, int offset, int limit, string sortBy, string orderBy)
        {
            try
            {
                var tuple = await _clothingRepository.GetAll(filter, isActive, clothingCategoryId, offset, limit, sortBy, orderBy);

                var records = tuple.Item1
                            .Select(a => new GetListClothingDTO
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Descripcion = a.Description,
                                ClothingCategoryId = a.ClothingCategoryId,
                                ClothingCategoryName = a.ClothingCategory?.Name ?? string.Empty,
                                PriceSuggested= a.PriceSuggested,
                                InvestmentUnit = a.InvestmentUnit,
                                Stock = a.Stock,
                                CreateBy = a.CreateBy,
                                CreationDate = a.CreationDate,
                                ModifiedBy = a.ModifiedBy,
                                ModificationDate = a.ModificationDate,
                                IsActive = a.IsActive,
                                DeletionDate = a.DeletionDate
                            });

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

        public async Task<int> Create(PostClothingViewModel clothing, string user)
        {

            try
            {
                var entity = new Clothing()
                {
                    Id = clothing.Id,
                    Name = clothing.Name,
                    Description = clothing.Description,
                    ClothingCategoryId = clothing.ClothingCategoryId ,
                    InvestmentUnit = CalculateInvestmentUnit(clothing.PostClothingMaterial),
                    PriceSuggested = clothing.PriceSuggested,
                    Stock = clothing.Stock,
                    CreateBy = user,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                };

                _clothingRepository.Add(entity);

                if (clothing.SizeIds is not null)
                {
                    foreach (var item in clothing.SizeIds)
                    {
                        var entitySize = new ClothingSize()
                        {
                            Clothing = entity,
                            SizeId = item,
                            CreateBy = user,
                            CreationDate = DateTime.Now,
                            IsActive = true
                        };

                        _clothingSizeRepository.Add(entitySize);
                    }
                }
                
                foreach (var item in clothing.PostClothingMaterial)
                {
                    var entityDetail = new ClothingMaterial()
                    {
                        Clothing = entity,
                        Quantity = item.Quantity,
                        MaterialId = item.MaterialId,
                        CreateBy = user,
                        CreationDate = DateTime.Now,
                        IsActive = true,
                    };
                    _clothingMaterialRepository.Add(entityDetail);
                }

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar Prenda");
            }
        }        

        public async Task<int> Update(PostClothingViewModel model, string user)
        {
            var entity = await _clothingRepository.GetById(model.Id);
            if (entity == null) throw new Exception("La Prenda no existe");

            try
            {
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.ClothingCategoryId = model.ClothingCategoryId;
                entity.InvestmentUnit = CalculateInvestmentUnit(model.PostClothingMaterial);
                entity.PriceSuggested = model.PriceSuggested;                
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.Now;

                _clothingRepository.Update(entity);

                var clothingSizes = await _clothingSizeRepository.GetAllByClothingId(entity.Id);

                var clothingSizeDelete = clothingSizes
                                           .Where(x => !(model.SizeIds ?? Array.Empty<int>()).Contains(x.SizeId))
                                           .ToList();

                var clothingSizeUpdate = clothingSizes
                                           .Where(x => (model.SizeIds ?? Array.Empty<int>()).Contains(x.SizeId))
                                           .ToList();

                var clothingSizeNew = (model.SizeIds ?? Array.Empty<int>()).Where(x => !clothingSizes.Select(y => y.SizeId).Contains(x)).ToList();

                foreach (var item in clothingSizeDelete)
                {
                    item.DeletionDate = DateTime.Now;

                    _clothingSizeRepository.Update(item);
                }

                foreach (var item in clothingSizeUpdate)
                {
                    var modelSizeUpdate = (model.SizeIds ?? Array.Empty<int>()).Where(x => x == item.SizeId).FirstOrDefault();

                    if (modelSizeUpdate != 0)
                    {
                        item.ModifiedBy = user;
                        item.ModificationDate = DateTime.Now;

                        _clothingSizeRepository.Update(item);
                    }
                }

                foreach (var item in clothingSizeNew)
                {
                    var entitySize = new ClothingSize()
                    {
                        ClothingId = entity.Id,
                        SizeId = item,
                        CreateBy = user,
                        CreationDate = DateTime.Now,
                        IsActive = true
                    };

                    _clothingSizeRepository.Add(entitySize);
                }

                var entityDetail = await _clothingMaterialRepository.GetAllByClothingId(entity.Id);

                var entityDetailDelete = entityDetail
                                            .Where(x => !model.PostClothingMaterial
                                                                    .Select(y => y.Id)
                                                                    .Contains(x.Id))
                                            .ToList();

                var entityDetailUpdate = entityDetail
                                           .Where(x => model.PostClothingMaterial
                                                                   .Select(y => y.Id)
                                                                   .Contains(x.Id))
                                           .ToList();

                var entityDetailNew = model.PostClothingMaterial.Where(x => x.Id == 0).ToList();

                foreach (var item in entityDetailDelete)
                {
                    item.DeletionDate = DateTime.Now;

                    _clothingMaterialRepository.Update(item);
                }

                foreach (var item in entityDetailUpdate)
                {
                    var modelDetailUpdate = model.PostClothingMaterial.Where(x => x.Id == item.Id).FirstOrDefault();

                    if (modelDetailUpdate is not null)
                    {
                        item.Quantity = modelDetailUpdate.Quantity;
                        item.ModifiedBy = user;
                        item.ModificationDate = DateTime.Now;

                        _clothingMaterialRepository.Update(item);
                    }

                }

                foreach (var item in entityDetailNew)
                {
                    var buyMaterialDetail = new ClothingMaterial()
                    { 
                        Clothing = entity,                        
                        MaterialId = item.MaterialId,
                        Quantity = item.Quantity,
                        CreateBy = user,
                        CreationDate = DateTime.Now,
                        IsActive = true,
                    };

                    _clothingMaterialRepository.Add(buyMaterialDetail);

                }

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar Prenda");
            }
        }

        public async Task<PostClothingViewModel> GetById(int id)
        {
            var entity = await _clothingRepository.GetById(id);
            if (entity == null) throw new Exception("La Prenda no existe");
            var entityDetail = await _clothingMaterialRepository.GetAllByClothingId(entity.Id);

            try
            {
                var clothingSize = await _clothingSizeRepository.GetAllByClothingId(entity.Id);

                var material = new PostClothingViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    ClothingCategoryId= entity.ClothingCategoryId,                     
                    PriceSuggested= entity.PriceSuggested,
                    Stock = entity.Stock,
                    SizeIds = clothingSize.Select(x => (int)x.SizeId).ToArray(),
                    PostClothingMaterial = entityDetail.Select(x => new PostClothingMaterialViewModel()
                    {
                        Id = x.Id,
                        Quantity = x.Quantity,
                        Cost = x.Material?.Cost ?? 0,
                        MaterialId = x.MaterialId,
                        MaterialName = x.Material?.Name ?? string.Empty,
                        UnitQuantity = x.Material?.UnitQuantity ?? 0,
                        UnitMeasurement = x.Material?.UnitMeasurement ?? 0,
                    }).ToList()
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
                entity.ModificationDate = DateTime.Now;

                _clothingRepository.Update(entity);

                //cambiar el estado de los clothingMaterial ?
                //cambiar el estado de los clothingSize

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
                entity.ModificationDate = DateTime.Now;
                entity.DeletionDate = DateTime.Now;

                _clothingRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la Prenda");
            }
        }

        public async Task<bool> ValidateClothingInUse(int id)
        {
            try
            {
                return await _clothingRepository.ClothingInUse(id);
            }
            catch (Exception)
            {
                throw new Exception("Error la validar eliminación de prenda");
            }
        }

        public async Task<IEnumerable<ItemDTO>> GetAutocomplete(string filter, int limit)
        {
            try
            {
                var records = await _clothingRepository.GetAll(filter, limit);

                var result = records.Select(x => new ItemDTO
                {
                    Id = x.Id,
                    Description = x.Name
                });

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autocompletar la Prenda");
            }

        }

        public async Task<IEnumerable<ItemClothingSizeDTO>> GetAutocompleteSize(string filter, int limit)
        {
            try
            {
                var result = await _clothingRepository.GetAllWithSizes(filter, limit);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autocompletar la Prenda");
            }

        }

        private decimal CalculateInvestmentUnit(IEnumerable<PostClothingMaterialViewModel> modelList)
        {
            var result = modelList.Select(x => x.Cost * x.Quantity / x.UnitQuantity).Sum();
            return result;
        }
    }
}
