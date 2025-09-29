using Microsoft.EntityFrameworkCore;
using HelloWorldApi.Domain.Entities;
using HelloWorldApi.Domain.Repositories;
using HelloWorldApi.Infrastructure.Data;

namespace HelloWorldApi.Infrastructure.Repositories
{
    public class PostgreSqlGenGeneralRepository : IGenGeneralRepository
    {
        private readonly ApplicationDbContext _context;

        public PostgreSqlGenGeneralRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenGeneral>> GetAllAsync()
        {
            return await _context.GenGeneral
                .OrderBy(g => g.Id)
                .ToListAsync();
        }

        public async Task<GenGeneral?> GetByIdAsync(int id)
        {
            return await _context.GenGeneral
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<GenGeneral?> GetByCodigoAsync(string codigo)
        {
            return await _context.GenGeneral
                .FirstOrDefaultAsync(g => g.Codigo == codigo);
        }

        public async Task<GenGeneral> CreateAsync(GenGeneral entity)
        {
            entity.FechaCreacion = DateTime.UtcNow;
            _context.GenGeneral.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<GenGeneral> UpdateAsync(GenGeneral entity)
        {
            entity.FechaModificacion = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.GenGeneral.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.GenGeneral
                .AnyAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<GenGeneral>> GetActivosAsync()
        {
            return await _context.GenGeneral
                .Where(g => g.Activo == true)
                .OrderBy(g => g.Id)
                .ToListAsync();
        }
    }
}