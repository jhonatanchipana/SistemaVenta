using SistemaVenta.Entities;
using Venta.Data.Interfaces;
using Venta.Data.Repository;
using Venta.Dto.Object.Clothing;
using Venta.Dto.Object.Cross;
using Venta.Dto.Object.Others;
using Venta.Dto.Object.Sales;
using Venta.Entities;
using Venta.Services.Interface;

namespace Venta.Services.Bussiness
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly ISalesClothingRepository _salesClothingRepository;
        private readonly IClothingRepository _clothingRepository;
        private readonly IClothingSizeRepository _clothingSizeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SalesService(ISalesRepository salesRepository,
            ISalesClothingRepository salesClothingRepository,
            IClothingRepository clothingRepository,
            IClothingSizeRepository clothingSizeRepository,
            IUnitOfWork unitOfWork)
        {
            _salesRepository = salesRepository;
            _salesClothingRepository = salesClothingRepository;
            _clothingRepository = clothingRepository;
            _clothingSizeRepository = clothingSizeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultsDTO<GetListSalesDTO>> GetAll(string filter, bool? isActive, int offset, int limit, string sortBy, string orderBy)
        {
            try
            {
                var tuple = await _salesRepository.GetAll(filter, isActive, offset, limit, sortBy, orderBy);

                var records = tuple.Item1
                            .Select(a => new GetListSalesDTO
                            {
                                Id = a.Id,
                                SaleDate = a.SaleDate,
                                PriceTotal = a.PriceTotal,
                                QuantityTotal = a.QuantityTotal,
                                Investment = a.Investment,
                                CreateBy = a.CreateBy,
                                CreationDate = a.CreationDate,
                                ModifiedBy = a.ModifiedBy,
                                ModificationDate = a.ModificationDate,
                                IsActive = a.IsActive,
                                DeletionDate = a.DeletionDate
                            });

                var result = new ResultsDTO<GetListSalesDTO>()
                {
                    Results = records,
                    Rows = tuple.Item2
                };

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar ventas");
            }
        }


        public async Task<int> Create(PostSalesViewModel model, string user)
        {

            try
            {
                var entity = new Sales()
                {
                    Id = model.Id,
                    SaleDate = DateTime.Now,
                    QuantityTotal = QuantityTotalClothing(model.PostSalesClothesSize),
                    PriceTotal = PriceTotalClothing(model.PostSalesClothesSize),
                    Investment = InvestmentTotalClothing(model.PostSalesClothesSize),
                    CreateBy = user,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                };

                _salesRepository.Add(entity);

                foreach (var item in model.PostSalesClothesSize)
                {
                    var entityDetail = new SalesClothingSize()
                    {
                        Sales = entity,
                        ClothingId = item.ClothingId,
                        ClothingSizeId = item.ClothingSizeId,
                        Quantity = item.Quantity,
                        PriceUnit = item.PriceUnit,
                        CreateBy = user,
                        CreationDate = DateTime.Now,
                        IsActive = true,
                    };

                    _salesClothingRepository.Add(entityDetail);


                    var clothingSize = await _clothingSizeRepository.GetById(item.ClothingSizeId);

                    if (clothingSize is not null)
                    {
                        clothingSize.Stock -= item.Quantity;
                        clothingSize.ModifiedBy = user;
                        clothingSize.ModificationDate = DateTime.Now;

                        _clothingSizeRepository.Update(clothingSize);
                    }
                }

                var clothingIds = model.PostSalesClothesSize
                                       .GroupBy(g => g.ClothingId)
                                       .Select(x => new { clothingId = x.Key, quantityClothing = x.Select(x => x.Quantity).Sum() })
                                       .Distinct()
                                       .ToList();

                foreach (var item in clothingIds)
                {
                    var clothing = await _clothingRepository.GetById(item.clothingId);

                    if (clothing is not null)
                    {
                        clothing.Stock -= item.quantityClothing;
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
                throw new Exception("Error al registrar Venta");
            }
        }        

        public async Task<int> Update(PostSalesViewModel model, string user)
        {
            var entity = await _salesRepository.GetById(model.Id);
            if (entity == null) throw new Exception("El registro no existe");

            try
            {
                entity.QuantityTotal = QuantityTotalClothing(model.PostSalesClothesSize);
                entity.PriceTotal = PriceTotalClothing(model.PostSalesClothesSize);
                entity.Investment = InvestmentTotalClothing(model.PostSalesClothesSize);
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.Now;

                _salesRepository.Update(entity);

                var entityDetail = await _salesClothingRepository.GetAllBySalesId(entity.Id);

                var entityDetailDelete = entityDetail
                                            .Where(x => !model.PostSalesClothesSize
                                                                    .Select(y => y.Id)
                                                                    .Contains(x.Id))
                                            .ToList();

                var entityDetailUpdate = entityDetail
                                           .Where(x => model.PostSalesClothesSize
                                                                   .Select(y => y.Id)
                                                                   .Contains(x.Id))
                                           .ToList();

                var entityDetailNew = model.PostSalesClothesSize.Where(x => x.Id == 0).ToList();

                foreach (var item in entityDetailDelete)
                {
                    item.DeletionDate = DateTime.Now;

                    _salesClothingRepository.Update(item);
                }

                foreach (var item in entityDetailUpdate)
                {
                    var modelDetailUpdate = model.PostSalesClothesSize.Where(x => x.Id == item.Id).FirstOrDefault();

                    if (modelDetailUpdate is not null)
                    {
                        item.Quantity = modelDetailUpdate.Quantity;
                        item.PriceUnit = modelDetailUpdate.PriceUnit;                        
                        item.ModifiedBy = user;
                        item.ModificationDate = DateTime.Now;

                        _salesClothingRepository.Update(item);
                    }

                }

                foreach (var item in entityDetailNew)
                {
                    var buyMaterialDetail = new SalesClothingSize()
                    {
                        Sales = entity,
                        ClothingId = item.ClothingId,
                        Quantity = item.Quantity,
                        PriceUnit = item.PriceUnit,
                        CreateBy = user,
                        CreationDate = DateTime.Now,
                        IsActive = true
                    };

                    _salesClothingRepository.Add(buyMaterialDetail);

                }

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar Venta");
            }
        }

        public async Task<PostSalesViewModel> GetById(int id)
        {
            var entity = await _salesRepository.GetById(id);
            if (entity == null) throw new Exception("El registro no existe");
            var entityDetail = await _salesClothingRepository.GetAllBySalesId(entity.Id);

            try
            {
                var material = new PostSalesViewModel
                {
                    Id = entity.Id,                    
                    PostSalesClothesSize = entityDetail.Select(x => new PostSalesClothingSizeViewModel()
                    {
                        Id = x.Id,
                        ClothingId = x.ClothingId,
                        ClothingName = x.Clothing?.Name ?? string.Empty,
                        Quantity = x.Quantity,
                        PriceUnit= x.PriceUnit                  
                    }).ToList()
                };

                return material;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Venta");
            }
        }

        public async Task ChangeStatus(int id, bool isActive, string user)
        {
            var entity = await _salesRepository.GetById(id);
            if (entity == null) throw new Exception("EL registro no existe");

            try
            {
                entity.IsActive = isActive;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.Now;

                _salesRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Cambiar estado de la Venta");
            }
        }

        public async Task Delete(int id, string user)
        {
            var entity = await _salesRepository.GetById(id);
            if (entity == null) throw new Exception("El registro no existe");

            try
            {
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.Now;
                entity.DeletionDate = DateTime.Now;

                _salesRepository.Update(entity);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la Venta");
            }
        }

        private static decimal InvestmentTotalClothing(IEnumerable<PostSalesClothingSizeViewModel> postSalesClothes)
        {
            return postSalesClothes.Select(x => x.InvestmentUnit * x.Quantity ).Sum();
        }

        private static decimal PriceTotalClothing(IEnumerable<PostSalesClothingSizeViewModel> postSalesClothes)
        {
            return postSalesClothes.Select(x => x.PriceUnit).Sum();
        }

        private static int QuantityTotalClothing(IEnumerable<PostSalesClothingSizeViewModel> postSalesClothes)
        {
            return postSalesClothes.Select(x => x.Quantity).Sum();
        }

    }
}
