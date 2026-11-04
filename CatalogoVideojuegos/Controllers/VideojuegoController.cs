// Este controlador es la entrada HTTP para las operaciones de videojuegos.
// Recibe solicitudes, llama al servicio y devuelve códigos HTTP.
// La lógica de acceso a la base de datos está en VideojuegoService.
// [ApiController]: habilita comportamientos propios de API, como validación.
// [Route]: define la ruta base: /api/videojuegos.
// ControllerBase: aporta métodos como Ok(), NotFound(), etc.
using Microsoft.AspNetCore.Mvc;
using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Services;

namespace CatalogoVideojuegos.Controllers
{
    [ApiController]
    [Route("api/videojuegos")]
    public class VideojuegoController : ControllerBase
    {
// Servicio que el controlador utilizará. readonly evita reasignarlo después del constructor.
        private readonly IVideojuegoService _videojuegoService;
 // Constructor: ASP.NET Core entrega el servicio registrado en Program.cs.
        public VideojuegoController(IVideojuegoService videojuegoService)
        {
            _videojuegoService = videojuegoService;
        }
// GET /api/videojuegos: solicita todos los juegos al servicio y devuelve 200 OK.
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var juegos = await _videojuegoService.ObtenerTodosAsync();
            return Ok(juegos);
        }
// GET /api/videojuegos/{id}: busca un juego específico. Si no existe, devuelve 404.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var juego = await _videojuegoService.ObtenerPorIdAsync(id);
            if (juego == null)
                return NotFound($"El videojuego con ID {id} no existe.");
            return Ok(juego);
        }
// POST /api/videojuegos: recibe el JSON en el DTO y crea un registro. CreatedAtAction devuelve 201.
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VideojuegoDTO dto)
        {
            if (dto == null)
                return BadRequest("Los datos enviados no son válidos.");
            var nuevoJuego = await _videojuegoService.CrearAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = nuevoJuego.Id }, nuevoJuego);
        }
 // PUT /api/videojuegos/{id}: reemplaza/actualiza los datos del juego indicado
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] VideojuegoDTO dto)
        {
            if (dto == null) return BadRequest();
            var actualizado = await _videojuegoService.ActualizarAsync(id, dto);
            if (!actualizado)
                return NotFound($"No se pudo actualizar. El videojuego con ID {id} no existe.");
            return NoContent();
        }
// DELETE /api/videojuegos/{id}: solicita la eliminación. Devuelve 404 si no se encontró - 204 si se eliminó.
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