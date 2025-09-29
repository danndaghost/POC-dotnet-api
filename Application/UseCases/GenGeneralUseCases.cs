using HelloWorldApi.Domain.Entities;
using HelloWorldApi.Domain.Repositories;
using HelloWorldApi.Application.DTOs;

namespace HelloWorldApi.Application.UseCases
{
    public class GenGeneralUseCases
    {
        private readonly IGenGeneralRepository _repository;

        public GenGeneralUseCases(IGenGeneralRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GenGeneralDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(MapToDto);
        }

        public async Task<GenGeneralDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<GenGeneralDto?> GetByCodigoAsync(string codigo)
        {
            var entity = await _repository.GetByCodigoAsync(codigo);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<IEnumerable<GenGeneralDto>> GetActivosAsync()
        {
            var entities = await _repository.GetActivosAsync();
            return entities.Select(MapToDto);
        }

        public async Task<GenGeneralDto> CreateAsync(CreateGenGeneralDto dto)
        {
            var entity = new GenGeneral(dto.Codigo, dto.Nombre, dto.Descripcion, dto.Valor)
            {
                Activo = dto.Activo
            };

            var createdEntity = await _repository.CreateAsync(entity);
            return MapToDto(createdEntity);
        }

        public async Task<GenGeneralDto?> UpdateAsync(int id, UpdateGenGeneralDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            // Actualizar solo los campos que no son nulos
            if (dto.Nombre != null) entity.Nombre = dto.Nombre;
            if (dto.Descripcion != null) entity.Descripcion = dto.Descripcion;
            if (dto.Valor != null) entity.Valor = dto.Valor;
            if (dto.Activo != null) entity.Activo = dto.Activo;

            var updatedEntity = await _repository.UpdateAsync(entity);
            return MapToDto(updatedEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }

        private static GenGeneralDto MapToDto(GenGeneral entity)
        {
            return new GenGeneralDto
            {
                Id = entity.Id,
                Codigo = entity.Codigo,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
                Valor = entity.Valor,
                Activo = entity.Activo,
                FechaCreacion = entity.FechaCreacion,
                FechaModificacion = entity.FechaModificacion
            };
        }
    }
}