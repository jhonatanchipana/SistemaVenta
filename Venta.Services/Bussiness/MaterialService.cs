using SistemaVenta.Entities;
using SistemaVenta.Entities.Enums;
using Venta.Dto.Object.Material;
using Venta.Services.Interface;
using System.Linq.Dynamic.Core;
using Venta.Data.Interfaces;
using LaTinka.Common;
using Venta.Dto.Object.Cross;
using Venta.Dto.Object.Others;

namespace Venta.Services.Bussiness
{
    public class MaterialService : IMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IUnitOfWork unitOfWork,
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
                                UnitMeasurementId = (int)a.UnitMeasurement,
                                UnitMeasurementName = EnumManager.GetEnumDescription(a.UnitMeasurement),
                                Stock = a.Stock,
                                CreateBy = a.CreateBy,
                                CreationDate = a.CreationDate,
                                ModifiedBy = a.ModifiedBy,
                                ModificationDate = a.ModificationDate,
                                IsActive = a.IsActive,
                                DeletionDate = a.DeletionDate
                            });

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

        public async Task<int> Create(PostMaterialViewModel material, string user)
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
                    UnitMeasurement = material.UnitMeasurement,
                    Stock = material.Stock,
                    CreateBy = user,
                    CreationDate = DateTime.Now,
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

        public async Task<int> Update(PostMaterialViewModel material, string user)
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
                entity.ModifiedBy = user;
                entity.ModificationDate = DateTime.Now;

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
                    UnitMeasurement = entity.UnitMeasurement,
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
                entity.ModificationDate = DateTime.Now;

                _materialRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Cambiar estado del Material");
            }
        }

        public async Task<bool> ValidateMaterialInUse(int id)
        {
            try
            {
                return await _materialRepository.MaterialInUse(id);                
            }
            catch (Exception )
            {
                throw new Exception("Error la validar eliminación de material");
            }
        }

        public async Task Delete(int id, string user)
        {
            var entity = await _materialRepository.GetById(id);
            if (entity == null) throw new Exception("El material no existe");          

            try
            {
                entity.DeletionDate = DateTime.Now;

                _materialRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el Material");
            }
        }

        public async Task<IEnumerable<ItemClothingCategoryDTO>> GetAutocomplete(string filter, int limit, int[] ignoreIds)
        {            
            try
            {
                var records = await _materialRepository.GetAll(filter, limit, ignoreIds);

                var result = records.Select(x => new ItemClothingCategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UnitMeasurementId = (int)x.UnitMeasurement,
                    UnitMeasurementDescription = EnumManager.GetEnumDescription(x.UnitMeasurement),
                    Cost = x.Cost,
                    UnitQuantity = x.UnitQuantity
                });

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autocompletar el material");
            }
            
        }

    }
}
