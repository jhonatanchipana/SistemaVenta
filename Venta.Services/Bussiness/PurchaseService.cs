using SistemaVenta.Entities.Enums;
using SistemaVenta.Entities;
using Venta.Data.Interfaces;
using Venta.Dto.Object.Material;
using Venta.Dto.Object;
using Venta.Services.Interface;
using Venta.Dto.Object.Purchase;
using System.Linq;

namespace Venta.Services.Bussiness
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _buyMaterialRepository;
        private readonly IPurchaseMaterialRepository _buyMaterialDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IPurchaseRepository buyMaterialRepository,
            IPurchaseMaterialRepository buyMaterialDetailRepository,
            IMaterialRepository materialRepository,
            IUnitOfWork unitOfWork)
        {
            _buyMaterialRepository = buyMaterialRepository;
            _buyMaterialDetailRepository = buyMaterialDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultsDTO<GetListPurchaseDTO>> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            try
            {
                var tuple = await _buyMaterialRepository.GetAll(filter, isActive, offset, limit, sortBy, orderBy);

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
                            }).ToList();

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

        public async Task<int> Create(PostPurchaseViewModel modelo)
        {

            try
            {
                var entity = new Purchase()
                {
                    Id = modelo.Id,
                    BuyDate = modelo.BuyDate,
                    CostTotal = modelo.PostBuyMaterialDetail.Select(x => x.Price ?? 0).Sum(),
                    NameBuyer = modelo.NameBuyer,
                    QuantityMaterial = modelo.PostBuyMaterialDetail.Count(),
                    CreateBy = modelo.CreateBy,
                    CreationDate = modelo.CreationDate,
                    ModifiedBy = modelo.ModifiedBy,
                    ModificationDate = modelo.ModificationDate,
                    IsActive = true,
                    DeletionDate = null
                };

                _buyMaterialRepository.Add(entity);

                foreach (var item in modelo.PostBuyMaterialDetail)
                {
                    var entityDetail = new PurchaseMaterial()
                    {
                        Purchase = new Purchase() { Id = item.Id },
                        Material = new Material() { Id = item.MaterialId },
                        Price = item.Price ?? 0,
                        Quantity = item.Quantity ?? 0,
                        CreateBy = item.CreateBy,
                        CreationDate = item.CreationDate,
                        ModifiedBy = item.ModifiedBy,
                        ModificationDate = item.ModificationDate,
                        IsActive = true,
                        DeletionDate = null
                    };

                    _buyMaterialDetailRepository.Add(entityDetail);
                }

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la Compra de Material");
            }
        }

        public async Task<int> Update(PostPurchaseViewModel modelo)
        {
            var entity = await _buyMaterialRepository.GetById(modelo.Id);
            if (entity == null) throw new Exception("La Compra de Material no existe");

            try
            {
                entity.Id = modelo.Id;
                entity.BuyDate = modelo.BuyDate;
                entity.CostTotal = modelo.PostBuyMaterialDetail.Select(x => x.Price ?? 0).Sum();
                entity.NameBuyer = modelo.NameBuyer;
                entity.QuantityMaterial = modelo.PostBuyMaterialDetail.Count();
                entity.ModifiedBy = modelo.ModifiedBy;
                entity.ModificationDate = DateTime.UtcNow;

                _buyMaterialRepository.Update(entity);

                var entityDetail = await _buyMaterialDetailRepository.GetAllByBuyMaterialId(entity.Id);

                var entityDetailDelete = entityDetail
                                            .Where(x => !modelo.PostBuyMaterialDetail
                                                                    .Select(y => y.Id)
                                                                    .Contains(x.Id))
                                            .ToList();

                var entityDetailUpdate = entityDetail
                                            .Where(x => modelo.PostBuyMaterialDetail
                                                                    .Select(y => y.Id)
                                                                    .Contains(x.Id))
                                            .ToList();

                var entityDetailNew = modelo.PostBuyMaterialDetail.Where(x => x.Id == 0).ToList();

                foreach (var item in entityDetailDelete)
                {
                    item.DeletionDate = DateTime.UtcNow;
                }

                foreach (var item in entityDetailUpdate)
                {
                    item.Price = item.Price;
                    item.Quantity = item.Quantity;
                    item.ModifiedBy = item.ModifiedBy;
                    item.ModificationDate = item.ModificationDate;
                }

                foreach (var item in entityDetailNew)
                {

                    var buyMaterialDetail = new PurchaseMaterial()
                    {
                        Purchase = new Purchase() { Id = item.Id },
                        Material = new Material() { Id = item.MaterialId },
                        Price = item.Price ?? 0,
                        Quantity = item.Quantity ?? 0,
                        CreateBy = item.CreateBy,
                        CreationDate = item.CreationDate,
                        ModifiedBy = item.ModifiedBy,
                        ModificationDate = item.ModificationDate,
                        IsActive = true,
                        DeletionDate = null
                    };

                    _buyMaterialDetailRepository.Add(buyMaterialDetail);

                }

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la Compra de Material");
            }
        }


        public async Task<GetPurchaseDTO> GetById(int id)
        {
            var entity = await _buyMaterialRepository.GetById(id);
            if (entity == null) throw new Exception("La Compra de Material no existe");

            try
            {
                var material = new GetPurchaseDTO
                {
                    Id = entity.Id,
                    BuyDate = entity.BuyDate,
                    CostTotal = entity.CostTotal,
                    NameBuyer = entity.NameBuyer,
                    QuantityMaterial = entity.QuantityMaterial,
                    CreateBy = entity.CreateBy,
                    CreationDate = entity.CreationDate,
                    ModifiedBy = entity.ModifiedBy,
                    ModificationDate = entity.ModificationDate,
                    IsActive = entity.IsActive,
                    DeletionDate = entity.DeletionDate
                };

                var entityDetail = await _buyMaterialDetailRepository.GetAllByBuyMaterialId(entity.Id);

                foreach (var detail in entityDetail)
                {
                    var buyMaterialDetail = new GetPurchaseMaterialDTO()
                    {
                        Id = detail.Id,
                        MaterialId = detail.MaterialId,
                        MaterialName = detail.Material?.Name ?? string.Empty,
                        Price = detail.Price,
                        Quantity = detail.Quantity,
                        CreateBy = detail.CreateBy,
                        CreationDate = detail.CreationDate,
                        ModifiedBy = detail.ModifiedBy,
                        ModificationDate = detail.ModificationDate,
                        IsActive = detail.IsActive,
                        DeletionDate = detail.DeletionDate
                    };

                    material.BuyMaterialDetailDTO.ToList().Add(buyMaterialDetail);
                }

                return material;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la Compra de Material");
            }
        }

        public async Task ChangeStatus(int id, bool isActive, string user)
        {
            var entity = await _buyMaterialRepository.GetById(id);
            if (entity == null) throw new Exception("La Compra de Material no existe");

            try
            {
                entity.IsActive = isActive;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.UtcNow;

                _buyMaterialRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Cambiar estado de la Compra de Material");
            }
        }

        public async Task Delete(int id, string user)
        {
            var entity = await _buyMaterialRepository.GetById(id);
            if (entity == null) throw new Exception("La Compra de Material no existe");

            try
            {
                entity.DeletionDate = DateTime.UtcNow;

                _buyMaterialRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la Compra de Material");
            }
        }

    }
}
