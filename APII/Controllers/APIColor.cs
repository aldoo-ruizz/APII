using APII.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace APII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIColor : ControllerBase
    {
        public static List<CatalogoColores> listcolores = new List<CatalogoColores>();

        [HttpGet("Obtenersaludo/{nombre}/{edad}")]
        public IActionResult ObtenSaludo(string nombre, int edad)
        {
            if (edad < 0)
            {
                return BadRequest("La edad no puede ser negativa.");
            }
            return Ok($"Hola {nombre} esta es la API, tienes {edad} años");
        }

        [HttpGet("ObtenColores")]
        public IActionResult ObtenerColores()
        {
            return Ok(listcolores);
        }

        [HttpGet("ObtenerColor/{id}")]
        public IActionResult ObtenerColor(int id)
        {
            var color = listcolores.FirstOrDefault(c => c.id == id);
            if (color == null)
            {
                return NotFound("No se encontro el color.");
            }
            return Ok(color);
        }

        [HttpPost("AgregarColor")]
        public IActionResult AgregarColor([FromBody] CatalogoColores color)
        {
            if (color.id <= 0 || string.IsNullOrWhiteSpace(color.NombreColor))
            {
                return BadRequest("El id debe ser positivo y el nombre es obligatorio.");
            }

            var colorExistente = listcolores.FirstOrDefault(c => c.id == color.id);
            if (colorExistente != null)
            {
                return Conflict("Ya existe un color con ese id.");
            }

            listcolores.Add(color);
            return Created("/api/APIColor/ObtenerColor/" + color.id, color);
        }

        [HttpDelete("EliminarColor/{id}")]
        public IActionResult EliminarColor(int id)
        {
            var color = listcolores.FirstOrDefault(c => c.id == id);
            if (color == null)
            {
                return NotFound("No se encontro el color.");
            }
            listcolores.Remove(color);
            return NoContent();
        }

        [HttpPut("ActualizarColor/{id}")]
        public IActionResult ActualizarColor(int id, [FromBody] CatalogoColores colorActualizado)
        {
            if (id <= 0 || id != colorActualizado.id || string.IsNullOrWhiteSpace(colorActualizado.NombreColor))
            {
                return BadRequest("Los ids deben coincidir y ser positivos; el nombre es obligatorio.");
            }

            var color = listcolores.FirstOrDefault(c => c.id == id);
            if (color == null)
            {
                return NotFound("No se encontro el color.");
            }
            color.NombreColor = colorActualizado.NombreColor;
            return Ok(color);
        }

        [HttpGet("SimularAcceso")]
        public IActionResult SimularAcceso(bool autenticado = false)
        {
            if (!autenticado)
            {
                return Unauthorized("Simulacion: se requiere autenticacion.");
            }
            return Ok("Simulacion: acceso permitido.");
        }
    }
}

