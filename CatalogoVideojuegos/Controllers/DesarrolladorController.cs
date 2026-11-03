using Microsoft.AspNetCore.Mvc;
using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Services;

namespace CatalogoVideojuegos.Controllers
{
    [ApiController]
    [Route("api/desarrolladores")]
    public class DesarrolladorController : ControllerBase
    {
        private readonly IDesarrolladorService _desarrolladorService;

        public DesarrolladorController(IDesarrolladorService desarrolladorService)
        {
            _desarrolladorService = desarrolladorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var desarrolladores = await _desarrolladorService.ObtenerTodosAsync();
            return Ok(desarrolladores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var desarrollador = await _desarrolladorService.ObtenerPorIdAsync(id);
            if (desarrollador == null)
                return NotFound("El desarrollador no existe.");
            return Ok(desarrollador);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DesarrolladorDTO dto)
        {
            if (dto == null)
                return BadRequest("Los datos enviados no son válidos.");
            var nuevoDesarrollador = await _desarrolladorService.CrearAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = nuevoDesarrollador.Id }, nuevoDesarrollador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DesarrolladorDTO dto)
        {
            if (dto == null) return BadRequest();
            var modificado = await _desarrolladorService.ActualizarAsync(id, dto);
            if (!modificado)
                return NotFound("No se encontró el desarrollador para actualizar.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var borrado = await _desarrolladorService.EliminarAsync(id);
            if (!borrado)
                return NotFound("No se encontró el desarrollador.");
            return NoContent();
        }
    }
}