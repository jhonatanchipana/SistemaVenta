using SistemaVenta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Interfaces;
using Venta.Data.Repository;
using Venta.Dto.Object.Purchase;
using Venta.Dto.Object;
using Venta.Services.Interface;
using SistemaVenta.Entities.Enums;
using Venta.Dto.Object.Campaign;

namespace Venta.Services.Bussiness
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CampaignService(ICampaignRepository campaignRepository,
            IUnitOfWork unitOfWork)
        {
            _campaignRepository = campaignRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultsDTO<GetListCampaignDTO>> GetAll(string filter, bool? isActive, int statusCampaignType ,int offset, int limit, string sortBy, string orderBy)
        {
            try
            {
                var tuple = await _campaignRepository.GetAll(filter, isActive,(StatusCampaignType) statusCampaignType, offset, limit, sortBy, orderBy);

                var records = tuple.Item1
                            .Select(a => new GetListCampaignDTO
                            {
                                Id = a.Id,
                                Name = a.Name,
                                InitialDate = a.InitialDate,
                                EndDate= a.EndDate,
                                StatusCampaignType = a.StatusCampaignType,
                                CreateBy = a.CreateBy,
                                CreationDate = a.CreationDate,                                
                                ModifiedBy = a.ModifiedBy,
                                ModificationDate = a.ModificationDate,
                                IsActive = a.IsActive,
                                DeletionDate = a.DeletionDate
                            }).ToList();

                var result = new ResultsDTO<GetListCampaignDTO>()
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

        public async Task<int> Create(PostCampaignViewModel modelo)
        {

            try
            {
                var entity = new Campaign()
                {
                    Name= modelo.Name,
                    InitialDate = modelo.InitialDate,
                    EndDate= modelo.EndDate,
                    CreateBy = modelo.CreateBy,
                    CreationDate = modelo.CreationDate,
                    ModifiedBy = modelo.ModifiedBy,
                    ModificationDate = modelo.ModificationDate,
                    IsActive = true,
                    DeletionDate = null
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

        public async Task<int> Update(PostCampaignViewModel modelo)
        {
            var entity = await _campaignRepository.GetById(modelo.Id);
            if (entity == null) throw new Exception("La Campaña no existe");

            try
            {
                entity.Id = modelo.Id;
                entity.Name = modelo.Name;
                entity.InitialDate = modelo.InitialDate;
                entity.EndDate = modelo.EndDate;
                entity.ModifiedBy = modelo.ModifiedBy;
                entity.ModificationDate = DateTime.UtcNow;

                _campaignRepository.Update(entity);

                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la Campaña");
            }
        }


        public async Task<GetCampaignDTO> GetById(int id)
        {
            var entity = await _campaignRepository.GetById(id);
            if (entity == null) throw new Exception("La Campaña no existe");

            try
            {
                var campaign = new GetCampaignDTO
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    InitialDate = entity.InitialDate,
                    EndDate = entity.EndDate,
                    StatusCampaignType = entity.StatusCampaignType,
                    CreateBy = entity.CreateBy,
                    CreationDate = entity.CreationDate,
                    ModifiedBy = entity.ModifiedBy,
                    ModificationDate = entity.ModificationDate,
                    IsActive = entity.IsActive,
                    DeletionDate = entity.DeletionDate
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
                entity.ModificationDate = DateTime.UtcNow;

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
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.UtcNow;
                entity.DeletionDate = DateTime.UtcNow;

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
