using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Interfaces;
using Venta.Data.Repository;
using Venta.Dto.Object.Purchase;
using Venta.Services.Interface;
using SistemaVenta.Entities.Enums;
using Venta.Dto.Object.ReportInOut;
using Venta.Dto.Object.Cross;

namespace Venta.Services.Bussiness
{
    public class ReportInOutService : IReportInOutService
    {
        private readonly IReportInOutRepository _reportInOutRepository;
        private readonly ISalesRepository _salesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReportInOutService(IReportInOutRepository reportInOutRepository,
            ISalesRepository salesRepository,
            IUnitOfWork unitOfWork)
        {
            _reportInOutRepository = reportInOutRepository;
            _salesRepository = salesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultsDTO<GetListReportInOutDTO>> GetAll(string filter, bool? isActive, int statusActivityType ,int offset, int limit, string sortBy, string orderBy)
        {
            try
            {
                var tuple = await _reportInOutRepository.GetAll(filter, isActive,(StatusActivityType) statusActivityType, offset, limit, sortBy, orderBy);

                var records = tuple.Item1
                            .Select(a => new GetListReportInOutDTO
                            {
                                Id = a.Id,
                                Name = a.Name,
                                InitialDate = a.InitialDate,
                                EndDate= a.EndDate,
                                StatusActivityType = a.StatusActivityType,
                                CreateBy = a.CreateBy,
                                CreationDate = a.CreationDate,                                
                                ModifiedBy = a.ModifiedBy,
                                ModificationDate = a.ModificationDate,
                                IsActive = a.IsActive,
                                DeletionDate = a.DeletionDate
                            });

                var result = new ResultsDTO<GetListReportInOutDTO>()
                {
                    Results = records,
                    Rows = tuple.Item2
                };

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar Campañas");
            }
        }

        public async Task<int> Create(PostReportInOutViewModel model, string user)
        {

            try
            {
                var entity = new ReportInOut()
                {
                    Name= model.Name,
                    InitialDate = model.InitialDate ?? DateTime.Now,
                    EndDate= model.EndDate ?? DateTime.Now,
                    StatusActivityType = StatusActivityType.Initial,
                    PurchaseId = model.PurchaseId,
                    CreateBy = user,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                };

                _reportInOutRepository.Add(entity);

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar Campaña");
            }
        }

        public async Task<int> Update(PostReportInOutViewModel model, string user)
        {
            var entity = await _reportInOutRepository.GetById(model.Id);
            if (entity == null) throw new Exception("La Campaña no existe");

            try
            {
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.InitialDate = model.InitialDate ?? DateTime.Now;
                entity.EndDate = model.EndDate ?? DateTime.Now;
                entity.PurchaseId = model.PurchaseId;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.Now;

                _reportInOutRepository.Update(entity);

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la Campaña");
            }
        }


        public async Task<PostReportInOutViewModel> GetById(int id)
        {
            var entity = await _reportInOutRepository.GetById(id);
            if (entity == null) throw new Exception("La Campaña no existe");

            try
            {
                var campaign = new PostReportInOutViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    InitialDate = entity.InitialDate,
                    EndDate = entity.EndDate,
                    PurchaseId = entity.PurchaseId,
                    CreateBy = entity.CreateBy,
                    CreationDate = entity.CreationDate,
                    ModifiedBy = entity.ModifiedBy,
                    ModificationDate = entity.ModificationDate                 
                };

                return campaign;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la Campaña");
            }
        }

        public async Task ChangeStatus(int id, bool isActive, string user)
        {
            var entity = await _reportInOutRepository.GetById(id);
            if (entity == null) throw new Exception("La Campaña no existe");

            try
            {
                entity.IsActive = isActive;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.Now;

                _reportInOutRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Cambiar estado de la Campaña");
            }
        }

        public async Task Delete(int id, string user)
        {
            var entity = await _reportInOutRepository.GetById(id);
            if (entity == null) throw new Exception("La Campaña no existe");

            try
            {
                entity.DeletionDate = DateTime.Now;

                _reportInOutRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la Campaña");
            }
        }

        public async Task<GetReportInOutFinalDTO> GetInOut(int id)
        {
            var report = await _reportInOutRepository.GetById(id);
            var purchaseTotal = await _reportInOutRepository.GetCostTotalOfPurchase(id);

            if (report is null) throw new Exception("El registro no existe");

            var salesTotal = await _salesRepository.GetSaleBetweenDates(report.InitialDate.Date, report.EndDate.Date);

            var result = new GetReportInOutFinalDTO()
            {
                PurchaseTotal = purchaseTotal,
                SaleTotal = salesTotal
            };

            return result; 
        }

    }
}
