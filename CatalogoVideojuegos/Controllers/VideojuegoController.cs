using Microsoft.AspNetCore.Mvc;
using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;
using CatalogoVideojuegos.Services;

namespace CatalogoVideojuegos.Controllers
{
    [ApiController]
    [Route("api/videojuegos")]
    public class VideojuegoController : ControllerBase
    {
        private readonly IVideojuegoService _videojuegoService;

        public VideojuegoController(IVideojuegoService videojuegoService)
        {
            _videojuegoService = videojuegoService;
        }
    

        [HttpGet]
        public IActionResult Get()
        {
            var juegos = _videojuegoService.ObtenerTodos();
            return Ok(juegos);
        }

        [HttpPost]
        public IActionResult Post([FromBody] VideojuegoDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Los datos enviados no son válidos.");
            }

            var nuevoJuego = _videojuegoService.Crear(dto);
            
            return CreatedAtAction(nameof(Get), nuevoJuego);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var juego = _videojuegoService.ObtenerPorId(id);
            
            if (juego == null)
            {
                return NotFound($"El videojuego con ID {id} no existe."); // Estado 404
            }
            
            return Ok(juego); 
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VideojuegoDTO dto)
        {
            if (dto == null) return BadRequest();

            var actualizado = _videojuegoService.Actualizar(id, dto);
            
            if (!actualizado)
            {
                return NotFound($"No se pudo actualizar. El videojuego con ID {id} no existe.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var eliminado = _videojuegoService.Eliminar(id);
            
            if (!eliminado)
            {
                return NotFound($"No se pudo eliminar. El videojuego con ID {id} no existe.");
            }

            return NoContent();
        }
    }
}