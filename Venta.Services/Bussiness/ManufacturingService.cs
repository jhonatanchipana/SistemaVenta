using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.Clothing;
using Venta.Dto.Object.Cross;
using Venta.Entities;
using Venta.Services.Interface;
using Venta.Data.Repository;
using Venta.Data.Interfaces;
using Venta.Dto.Object.Manufacturing;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Cryptography;

namespace Venta.Services.Bussiness
{
    public class ManufacturingService : IManufacturingService
    {
        private readonly IManufacturingRepository _manufacturingRepository;
        private readonly IManufacturingClothingSizeRepository _manufacturingClothingRepository;
        private readonly IClothingRepository _clothingRepository;
        private readonly IClothingSizeRepository _clothingSizeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ManufacturingService(IManufacturingRepository manufacturingRepository,
            IManufacturingClothingSizeRepository manufacturingClothingRepository,
            IClothingRepository clothingRepository,
            IClothingSizeRepository clothingSizeRepository,
            IUnitOfWork unitOfWork)
        {
            _manufacturingRepository = manufacturingRepository;
            _manufacturingClothingRepository = manufacturingClothingRepository;
            _clothingRepository = clothingRepository;
            _clothingSizeRepository = clothingSizeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultsDTO<GetListManufacturingDTO>> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            try
            {
                var tuple = await _manufacturingRepository.GetAll(filter, isActive, offset, limit, sortBy, orderBy);

                var records = tuple.Item1
                            .Select(a => new GetListManufacturingDTO
                            {
                                Id = a.Id,
                                ManufacturingDate = a.ManufacturingDate,
                                QuantityTotal = a.QuantityTotal,
                                CreateBy = a.CreateBy,
                                CreationDate = a.CreationDate,
                                ModifiedBy = a.ModifiedBy,
                                ModificationDate = a.ModificationDate,
                                IsActive = a.IsActive,
                                DeletionDate = a.DeletionDate
                            });

                var result = new ResultsDTO<GetListManufacturingDTO>()
                {
                    Results = records,
                    Rows = tuple.Item2
                };

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar Fabricación de Prendas");
            }
        }

        public async Task<int> Create(PostManufacturingViewModel model, string user)
        {

            try
            {
                var entity = new Manufacturing()
                {
                    Id = model.Id,
                    ManufacturingDate = DateTime.Now,
                    QuantityTotal = CalculateQuantityTotal(model.PostManufacturingClothings),
                    CreateBy = user,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                };

                _manufacturingRepository.Add(entity);

                foreach (var item in model.PostManufacturingClothings)
                {
                    var entityDetail = new ManufacturingClothingSize()
                    {
                        Manufacturing = entity,
                        ClothingSizeId = item.ClothingSizeId,
                        ClothingId  = item.ClothingId,
                        Quantity = item.Quantity,
                        CreateBy = user,
                        CreationDate = DateTime.Now,
                        IsActive = true,
                    };

                    _manufacturingClothingRepository.Add(entityDetail);

                    var clothingSize = await _clothingSizeRepository.GetById(item.ClothingSizeId);

                    if(clothingSize is not null)
                    {
                        clothingSize.Stock += item.Quantity;
                        clothingSize.ModifiedBy = user;
                        clothingSize.ModificationDate = DateTime.Now;
                        _clothingSizeRepository.Update(clothingSize);
                    }

                }

                var clothingIds = model.PostManufacturingClothings
                                        .GroupBy(g => g.ClothingId)
                                        .Select(x => new { clothingId = x.Key, quantityClothing = x.Select(x => x.Quantity).Sum() })
                                        .Distinct()
                                        .ToList();

                foreach (var item in clothingIds)
                {
                    var clothing = await _clothingRepository.GetById(item.clothingId);

                   if(clothing is not null )
                    {
                        clothing.Stock += item.quantityClothing;
                        clothing.ModifiedBy = user;
                        clothing.ModificationDate = DateTime.Now;

                        _clothingRepository.Update(clothing);
                    }

                }

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar Fabricación de Prendas");
            }
        }

        public async Task<int> Update(PostManufacturingViewModel model, string user)
        {
            var entity = await _manufacturingRepository.GetById(model.Id);
            if (entity == null) throw new Exception("El registro no existe");

            try
            {

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar Fabricación de prenda");
            }
        }

        public async Task<PostManufacturingViewModel> GetById(int id)
        {
            var entity = await _manufacturingRepository.GetById(id);
            if (entity == null) throw new Exception("El registro no existe");
            var entityDetail = await _manufacturingClothingRepository.GetAllByManufacturingId(entity.Id);

            try
            {
                var material = new PostManufacturingViewModel
                {
                    Id = entity.Id,
                    ManufacturingDate = entity.ManufacturingDate,
                    QuantityTotal = entity.QuantityTotal,    
                    PostManufacturingClothings = entityDetail.Select(x => new PostManufacturingClothingViewModel()
                    {
                        Id = x.Id,
                        Quantity = x.Quantity,
                        ClothingSizeId = x.ClothingSizeId ?? 0,
                        ClothingId = x.ClothingId
                    }).ToList()
                };

                return material;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Fabricación de Prenda");
            }
        }

        public async Task ChangeStatus(int id, bool isActive, string user)
        {
            var entity = await _manufacturingRepository.GetById(id);
            if (entity == null) throw new Exception("El registro no existe");

            try
            {
                entity.IsActive = isActive;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.Now;

                _manufacturingRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Cambiar estado de la Fabricación de Prenda");
            }
        }

        public async Task Delete(int id, string user)
        {
            var entity = await _manufacturingRepository.GetById(id);
            if (entity == null) throw new Exception("El registro no existe");

            try
            {
                entity.DeletionDate = DateTime.Now;

                _manufacturingRepository.Update(entity);

                var entityDetail = await _manufacturingClothingRepository.GetAllByManufacturingId(entity.Id);

                foreach (var item in entityDetail)
                {
                    item.DeletionDate = DateTime.Now;

                    _manufacturingClothingRepository.Update(item);

                    var clothingSize = await _clothingSizeRepository.GetById(item.ClothingSizeId ?? 0);

                    if(clothingSize is not null)
                    {
                        clothingSize.Stock -= item.Quantity;
                        clothingSize.ModifiedBy = user;
                        clothingSize.ModificationDate = DateTime.Now;

                        _clothingSizeRepository.Update(clothingSize);
                    }
                }

                var clothingIds = entityDetail
                                        .GroupBy(g => g.ClothingId)
                                        .Select(x => new { clothingId = x.Key, quantityClothing = x.Select(x => x.Quantity).Sum() })
                                        .Distinct()
                                        .ToList();

                foreach (var clothingId in clothingIds)
                {
                    var clothing = await _clothingRepository.GetById(clothingId.clothingId);

                    if (clothing is not null)
                    {
                        clothing.Stock -= clothingId.quantityClothing;
                        clothing.ModifiedBy = user;
                        clothing.ModificationDate = DateTime.Now;

                        _clothingRepository.Update(clothing);
                    }

                }

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la Fabricación de Prenda");
            }
        }

        private static int CalculateQuantityTotal(IEnumerable<PostManufacturingClothingViewModel> modelList)
        {
            var result = modelList.Select(x => x.Quantity).Sum();
            return result;
        }
    }
}
