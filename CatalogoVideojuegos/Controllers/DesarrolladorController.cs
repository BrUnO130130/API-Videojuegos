using Microsoft.AspNetCore.Mvc;
using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;
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
        public IActionResult Get()
        {
            var desarrolladores = _desarrolladorService.ObtenerTodos();
            return Ok(desarrolladores);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var desarrollador = _desarrolladorService.ObtenerPorId(id);
            
            if (desarrollador == null)
            {
                return NotFound("El desarrollador no existe.");
            }
            
            return Ok(desarrollador);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DesarrolladorDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Los datos enviados no son válidos.");
            }

            var nuevoDesarrollador = _desarrolladorService.Crear(dto);
            
            return CreatedAtAction(nameof(Get), nuevoDesarrollador);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DesarrolladorDTO dto)
        {
            if (dto == null) return BadRequest();

            var modificado = _desarrolladorService.Actualizar(id, dto);
            
            if (!modificado)
            {
                return NotFound("No se encontró el desarrollador para actualizar.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var borrado = _desarrolladorService.Eliminar(id);
            
            if (!borrado)
            {
                return NotFound("No se encontró el desarrollador.");
            }

            return NoContent();
        }
    }
}