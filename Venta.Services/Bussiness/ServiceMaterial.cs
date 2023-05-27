using Microsoft.EntityFrameworkCore;
using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Connection;
using Venta.Dto.Object;
using Venta.Dto.Object.Material;
using Venta.Services.Interface;
using System.Linq.Dynamic.Core;
using Venta.Data.Interfaces;
using Venta.Data.Repository;
using System.Security.Cryptography;

namespace Venta.Services.Bussiness
{
    public class ServiceMaterial : IServiceMaterial
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMaterialRepository _materialRepository;

        public ServiceMaterial(IUnitOfWork unitOfWork,
            IMaterialRepository materialRepository)
        {
            _unitOfWork = unitOfWork;
            _materialRepository = materialRepository;
        }

        public async Task<ResultsDTO<GetListMaterialDTO>> GetAll(string filter, bool? isActive, int unitMeasurement, int offset, int limit, string sortBy, string orderBy)
        {
            try
            {
                var tuple = await _materialRepository.GetAll(filter, isActive, (UnitMeasurementType)unitMeasurement, offset, limit, sortBy, orderBy);

                var records = tuple.Item1
                            .Select(a => new GetListMaterialDTO
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Description = a.Description,
                                Cost = a.Cost,
                                UnitQuantity = a.UnitQuantity,
                                UnitMeasurement = (int)a.UnitMeasurement,
                                Stock = a.Stock,
                                CreateBy = a.CreateBy,
                                CreationDate = a.CreationDate,
                                ModifiedBy = a.ModifiedBy,
                                ModificationDate = a.ModificationDate,
                                IsActive = a.IsActive,
                                DeletionDate = a.DeletionDate
                            }).ToList();

                var result = new ResultsDTO<GetListMaterialDTO>()
                {
                    Results = records,
                    Rows = tuple.Item2
                };

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar material");
            }
        }

        public async Task<int> Create(PostMaterialViewModel material)
        {

            try
            {
                var entity = new Material()
                {
                    Id = material.Id,
                    Name = material.Name,
                    Description = material.Description,
                    Cost = material.Cost,
                    UnitQuantity = material.UnitQuantity,
                    UnitMeasurement = (UnitMeasurementType)material.UnitMeasurement,
                    Stock = material.Stock,
                    CreateBy = material.CreatedBy,
                    CreationDate = material.CreationDate,
                    ModifiedBy = null,
                    ModificationDate = null,
                    IsActive = true,
                    DeletionDate = null
                };

                _materialRepository.Add(entity);
                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch(Exception ex)
            {
                throw new Exception("Error al registrar material");
            }

        }

        public async Task<int> Update(PostMaterialViewModel material)
        {
            var entity = await _materialRepository.GetById(material.Id);
            if (entity == null) throw new Exception("El material no existe");

            try
            {

                entity.Id = material.Id;
                entity.Name = material.Name;
                entity.Description = material.Description;
                entity.Cost = material.Cost;
                entity.UnitQuantity = material.UnitQuantity;
                entity.UnitMeasurement = (UnitMeasurementType)material.UnitMeasurement;
                entity.Stock = material.Stock;
                entity.ModifiedBy = material.ModifiedBy;
                entity.ModificationDate = DateTime.UtcNow;

                _materialRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar material");
            }
        }


        public async Task<GetMaterialDTO> GetById(int id)
        {
            var entity = await _materialRepository.GetById(id);
            if (entity == null) throw new Exception("El material no existe");

            try
            {
                var material = new GetMaterialDTO
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    Cost = entity.Cost,
                    UnitQuantity = entity.UnitQuantity,
                    UnitMeasurement = (int)entity.UnitMeasurement,
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
                throw new Exception("Error al obtener Material");
            }
        }

        public async Task ChangeStatus(int id, bool isActive, string user)
        {
            var entity = await _materialRepository.GetById(id);
            if (entity == null) throw new Exception("El material no existe");

            try
            {
                entity.IsActive = isActive;
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.UtcNow;

                _materialRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Cambiar estado del Material");
            }
        }

        public async Task Delete(int id, string user)
        {
            var entity = await _materialRepository.GetById(id);
            if (entity == null) throw new Exception("El material no existe");

            try
            {
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.UtcNow;
                entity.DeletionDate = DateTime.UtcNow;

                _materialRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el Material");
            }
        }

    }
}
