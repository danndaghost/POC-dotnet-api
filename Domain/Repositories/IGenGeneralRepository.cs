using HelloWorldApi.Domain.Entities;

namespace HelloWorldApi.Domain.Repositories
{
    public interface IGenGeneralRepository
    {
        Task<IEnumerable<GenGeneral>> GetAllAsync();
        Task<GenGeneral?> GetByIdAsync(int id);
        Task<GenGeneral?> GetByCodigoAsync(string codigo);
        Task<GenGeneral> CreateAsync(GenGeneral entity);
        Task<GenGeneral> UpdateAsync(GenGeneral entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<GenGeneral>> GetActivosAsync();
    }
}