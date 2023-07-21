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
using Venta.Dto.Object.Activity;
using Venta.Dto.Object.Cross;

namespace Venta.Services.Bussiness
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _campaignRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivityService(IActivityRepository campaignRepository,
            IUnitOfWork unitOfWork)
        {
            _campaignRepository = campaignRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultsDTO<GetListActivityDTO>> GetAll(string filter, bool? isActive, int statusActivityType ,int offset, int limit, string sortBy, string orderBy)
        {
            try
            {
                var tuple = await _campaignRepository.GetAll(filter, isActive,(StatusActivityType) statusActivityType, offset, limit, sortBy, orderBy);

                var records = tuple.Item1
                            .Select(a => new GetListActivityDTO
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

                var result = new ResultsDTO<GetListActivityDTO>()
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

        public async Task<int> Create(PostActivityViewModel model, string user)
        {

            try
            {
                var entity = new Activity()
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

                _campaignRepository.Add(entity);

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar Campaña");
            }
        }

        public async Task<int> Update(PostActivityViewModel model, string user)
        {
            var entity = await _campaignRepository.GetById(model.Id);
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

                _campaignRepository.Update(entity);

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la Campaña");
            }
        }


        public async Task<PostActivityViewModel> GetById(int id)
        {
            var entity = await _campaignRepository.GetById(id);
            if (entity == null) throw new Exception("La Campaña no existe");

            try
            {
                var campaign = new PostActivityViewModel
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
            var entity = await _campaignRepository.GetById(id);
            if (entity == null) throw new Exception("La Campaña no existe");

            try
            {
                entity.IsActive = isActive;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.Now;

                _campaignRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Cambiar estado de la Campaña");
            }
        }

        public async Task Delete(int id, string user)
        {
            var entity = await _campaignRepository.GetById(id);
            if (entity == null) throw new Exception("La Campaña no existe");

            try
            {
                entity.DeletionDate = DateTime.Now;

                _campaignRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la Campaña");
            }
        }

    }
}
