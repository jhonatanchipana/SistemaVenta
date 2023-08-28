using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using Venta.Data.Interfaces;
using Venta.Dto.Object.Material;
using Venta.Services.Interface;
using Venta.Dto.Object.Purchase;
using System.Linq;
using Venta.Dto.Object.Cross;
using Venta.Dto.Object.Others;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Venta.Services.Bussiness
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IPurchaseMaterialRepository _purchaseMaterialRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IPurchaseRepository buyMaterialRepository,
            IPurchaseMaterialRepository buyMaterialDetailRepository,
            IMaterialRepository materialRepository,
            IUnitOfWork unitOfWork)
        {
            _purchaseRepository = buyMaterialRepository;
            _purchaseMaterialRepository = buyMaterialDetailRepository;
            _materialRepository = materialRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultsDTO<GetListPurchaseDTO>> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            try
            {
                var tuple = await _purchaseRepository.GetAll(filter, isActive, offset, limit, sortBy, orderBy);

                var records = tuple.Item1
                            .Select(a => new GetListPurchaseDTO
                            {
                                Id = a.Id,
                                BuyDate = a.BuyDate,
                                CostTotal = a.CostTotal,
                                NameBuyer = a.NameBuyer,
                                QuantityMaterial = a.QuantityMaterial,
                                CreateBy = a.CreateBy,
                                CreationDate = a.CreationDate,
                                ModifiedBy = a.ModifiedBy,
                                ModificationDate = a.ModificationDate,
                                IsActive = a.IsActive,
                                DeletionDate = a.DeletionDate
                            });

                var result = new ResultsDTO<GetListPurchaseDTO>()
                {
                    Results = records,
                    Rows = tuple.Item2
                };

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar de Compras de Materiales");
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetCombo()
        {
            var records = await _purchaseRepository.GetAllByBuyDate(string.Empty, Int32.MaxValue);
            var results = records.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.BuyDate.ToString("dd-MM-yyyy")
            });

            return results;
        }
        public async Task<int> Create(PostPurchaseViewModel model, string user)
        {

            try
            {
                var entity = new Purchase()
                {
                    Id = model.Id,
                    BuyDate = model.BuyDate,
                    CostTotal = model.PostBuyMaterialDetail.Select(x => x.PriceUnit * x.Quantity).Sum(),
                    NameBuyer = model.NameBuyer,
                    QuantityMaterial = model.PostBuyMaterialDetail.Count,
                    CreateBy = user,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    DeletionDate = null
                };

                _purchaseRepository.Add(entity);

                foreach (var item in model.PostBuyMaterialDetail)
                {
                    var entityDetail = new PurchaseMaterial()
                    {
                        Purchase = entity,
                        MaterialId = item.MaterialId,
                        PriceUnit = item.PriceUnit,
                        Quantity = item.Quantity,
                        CreateBy = user,
                        CreationDate = DateTime.Now,
                        IsActive = true,
                        DeletionDate = null                     
                    };

                    _purchaseMaterialRepository.Add(entityDetail);

                    var material = await _materialRepository.GetById(item.MaterialId);

                    if (material is not null)
                    {
                        material.Stock += item.Quantity;
                        material.StockReal += item.Quantity * item.UnitQuantity;
                        _materialRepository.Update(material);
                    }
                   
                }

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la Compra de Material");
            }
        }

        public async Task<int> Update(PostPurchaseViewModel model, string user)
        {
            var entity = await _purchaseRepository.GetById(model.Id);
            if (entity == null) throw new Exception("La Compra de Material no existe");

            try
            {
                entity.Id = model.Id;
                entity.BuyDate = model.BuyDate;
                entity.CostTotal = model.PostBuyMaterialDetail.Select(x => x.PriceUnit).Sum();
                entity.NameBuyer = model.NameBuyer;
                entity.QuantityMaterial = model.PostBuyMaterialDetail.Count;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.Now;

                _purchaseRepository.Update(entity);

                var entityDetail = await _purchaseMaterialRepository.GetAllByBuyMaterialId(entity.Id);

                var entityDetailDelete = entityDetail
                                            .Where(x => !model.PostBuyMaterialDetail
                                                                    .Select(y => y.Id)
                                                                    .Contains(x.Id))
                                            .ToList();

                var entityDetailUpdate = entityDetail
                                            .Where(x => model.PostBuyMaterialDetail
                                                                    .Select(y => y.Id)
                                                                    .Contains(x.Id))
                                            .ToList();

                var entityDetailNew = model.PostBuyMaterialDetail.Where(x => x.Id == 0).ToList();

                foreach (var item in entityDetailDelete)
                {
                    item.DeletionDate = DateTime.Now;

                    _purchaseMaterialRepository.Update(item);

                    var material = await _materialRepository.GetById(item.MaterialId);
                    if (material is not null)
                    {
                        material.Stock -= item.Quantity;
                        material.StockReal -= item.Quantity * material.UnitQuantity;
                        _materialRepository.Update(material);
                    }
                }

                foreach (var item in entityDetailUpdate)
                {
                    var modelDetailUpdate = model.PostBuyMaterialDetail.Where(x => x.Id == item.Id).FirstOrDefault();
                    
                    if (modelDetailUpdate is not null)
                    {
                        var quantityActual = item.Quantity;

                        item.PriceUnit = modelDetailUpdate.PriceUnit;
                        item.Quantity = modelDetailUpdate.Quantity;
                        item.ModifiedBy = user;
                        item.ModificationDate = DateTime.Now;

                        _purchaseMaterialRepository.Update(item);

                        var material = await _materialRepository.GetById(item.MaterialId);
                        if (material is not null)
                        {
                            material.Stock = material.Stock + modelDetailUpdate.Quantity - quantityActual;
                            material.StockReal = material.StockReal - (quantityActual * material.UnitQuantity)  +  (material.UnitQuantity * modelDetailUpdate.Quantity);
                            _materialRepository.Update(material);
                        }
                    }
                    
                }

                foreach (var item in entityDetailNew)
                {
                    var buyMaterialDetail = new PurchaseMaterial()
                    {
                        Purchase = entity,
                        MaterialId = item.MaterialId,
                        PriceUnit = item.PriceUnit,
                        Quantity = item.Quantity,
                        CreateBy = user,
                        CreationDate = DateTime.Now,
                        IsActive = true,
                        DeletionDate = null
                    };

                    _purchaseMaterialRepository.Add(buyMaterialDetail);

                    var material = await _materialRepository.GetById(item.MaterialId);
                    if (material is not null)
                    {
                        material.Stock += item.Quantity;
                        material.StockReal += item.Quantity * item.UnitQuantity;
                        _materialRepository.Update(material);
                    }

                }

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la Compra de Material");
            }
        }


        public async Task<PostPurchaseViewModel> GetById(int id)
        {
            var entity = await _purchaseRepository.GetById(id);
            if (entity == null) throw new Exception("La Compra de Material no existe");

            try
            {
                var model = new PostPurchaseViewModel
                {
                    Id = entity.Id,
                    BuyDate = entity.BuyDate,
                    CostTotal = entity.CostTotal,
                    NameBuyer = entity.NameBuyer,
                    QuantityMaterial = entity.QuantityMaterial,
                    CreateBy = entity.CreateBy,
                    CreationDate = entity.CreationDate,
                    ModifiedBy = entity.ModifiedBy,
                    ModificationDate = entity.ModificationDate                    
                };

                var entityDetail =  await _purchaseMaterialRepository.GetAllByBuyMaterialId(entity.Id);

                var materialDetail = entityDetail.Select(x => new PostPurchaseMaterialViewModel()
                {
                    Id = x.Id,
                    MaterialId = x.MaterialId,
                    MaterialName = x.Material?.Name ?? string.Empty,
                    Quantity = x.Quantity,
                    CreateBy = x.CreateBy,
                    CreationDate = x.CreationDate,                                      
                    ModifiedBy = x.ModifiedBy,
                    ModificationDate = x.ModificationDate,
                    PriceUnit = x.PriceUnit,
                    UnitMeasurement = x.Material?.UnitMeasurement ?? 0,
                    UnitQuantity = x.Material?.UnitQuantity ?? 0,
                    UnitMeasurementMaterial = x.Material?.UnitMeasurementMaterial ?? 0
                }).ToList();

                model.PostBuyMaterialDetail = materialDetail;

                return model;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la Compra de Material");
            }
        }

        public async Task ChangeStatus(int id, bool isActive, string user)
        {
            var entity = await _purchaseRepository.GetById(id);
            if (entity == null) throw new Exception("La Compra de Material no existe");

            try
            {
                entity.IsActive = isActive;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.Now;

                _purchaseRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Cambiar estado de la Compra de Material");
            }
        }

        public async Task Delete(int id, string user)
        {
            var entity = await _purchaseRepository.GetById(id);
            if (entity == null) throw new Exception("La Compra de Material no existe");

            try
            {
                entity.DeletionDate = DateTime.Now;

                _purchaseRepository.Update(entity);

                var entityDetail = await _purchaseMaterialRepository.GetAllByBuyMaterialId(entity.Id);

                foreach (var item in entityDetail)
                {
                    item.DeletionDate = DateTime.Now;

                    var material = await _materialRepository.GetById(item.MaterialId);
                    if (material is not null)
                    {
                        material.Stock -= item.Quantity;
                        material.StockReal -= item.Quantity * material.UnitQuantity;
                    }

                    _purchaseMaterialRepository.Update(item);
                }

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la Compra de Material");
            }
        }

    }
}
