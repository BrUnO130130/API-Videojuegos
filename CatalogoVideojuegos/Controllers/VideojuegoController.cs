using Microsoft.AspNetCore.Mvc;
using CatalogoVideojuegos.DTOs;
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
        public async Task<IActionResult> Get()
        {
            var juegos = await _videojuegoService.ObtenerTodosAsync();
            return Ok(juegos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var juego = await _videojuegoService.ObtenerPorIdAsync(id);
            if (juego == null)
                return NotFound($"El videojuego con ID {id} no existe.");
            return Ok(juego);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VideojuegoDTO dto)
        {
            if (dto == null)
                return BadRequest("Los datos enviados no son válidos.");
            var nuevoJuego = await _videojuegoService.CrearAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = nuevoJuego.Id }, nuevoJuego);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] VideojuegoDTO dto)
        {
            if (dto == null) return BadRequest();
            var actualizado = await _videojuegoService.ActualizarAsync(id, dto);
            if (!actualizado)
                return NotFound($"No se pudo actualizar. El videojuego con ID {id} no existe.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _videojuegoService.EliminarAsync(id);
            if (!eliminado)
                return NotFound($"No se pudo eliminar. El videojuego con ID {id} no existe.");
            return NoContent();
        }
    }
}