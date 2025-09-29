using Microsoft.AspNetCore.Mvc;
using HelloWorldApi.Application.UseCases;
using HelloWorldApi.Application.DTOs;

namespace HelloWorldApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GenGeneralController : ControllerBase
    {
        private readonly GenGeneralUseCases _useCases;

        public GenGeneralController(GenGeneralUseCases useCases)
        {
            _useCases = useCases;
        }

        /// <summary>
        /// Obtiene todos los registros de gen_general
        /// </summary>
        /// <returns>Lista de registros gen_general</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GenGeneralDto>>> GetAll()
        {
            var result = await _useCases.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtiene solo los registros activos de gen_general
        /// </summary>
        /// <returns>Lista de registros gen_general activos</returns>
        [HttpGet("activos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GenGeneralDto>>> GetActivos()
        {
            var result = await _useCases.GetActivosAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un registro de gen_general por ID
        /// </summary>
        /// <param name="id">ID del registro</param>
        /// <returns>Registro gen_general</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenGeneralDto>> GetById(int id)
        {
            var result = await _useCases.GetByIdAsync(id);
            if (result == null)
                return NotFound($"No se encontró el registro con ID {id}");

            return Ok(result);
        }

        /// <summary>
        /// Obtiene un registro de gen_general por código
        /// </summary>
        /// <param name="codigo">Código del registro</param>
        /// <returns>Registro gen_general</returns>
        [HttpGet("codigo/{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenGeneralDto>> GetByCodigo(string codigo)
        {
            var result = await _useCases.GetByCodigoAsync(codigo);
            if (result == null)
                return NotFound($"No se encontró el registro con código '{codigo}'");

            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo registro en gen_general
        /// </summary>
        /// <param name="dto">Datos del nuevo registro</param>
        /// <returns>Registro creado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenGeneralDto>> Create([FromBody] CreateGenGeneralDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _useCases.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Actualiza un registro existente en gen_general
        /// </summary>
        /// <param name="id">ID del registro a actualizar</param>
        /// <param name="dto">Datos actualizados</param>
        /// <returns>Registro actualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenGeneralDto>> Update(int id, [FromBody] UpdateGenGeneralDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _useCases.UpdateAsync(id, dto);
            if (result == null)
                return NotFound($"No se encontró el registro con ID {id}");

            return Ok(result);
        }

        /// <summary>
        /// Elimina un registro de gen_general
        /// </summary>
        /// <param name="id">ID del registro a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _useCases.ExistsAsync(id);
            if (!exists)
                return NotFound($"No se encontró el registro con ID {id}");

            await _useCases.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Verifica si existe un registro con el ID especificado
        /// </summary>
        /// <param name="id">ID del registro</param>
        /// <returns>Verdadero si existe, falso en caso contrario</returns>
        [HttpHead("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Exists(int id)
        {
            var exists = await _useCases.ExistsAsync(id);
            return exists ? Ok() : NotFound();
        }
    }
}