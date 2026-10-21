using APII.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace APII.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
       
        public class BlancoAPiController : ControllerBase
        {
            public static List<CatalogoColores> listcolores = new List<CatalogoColores>();


            [HttpGet("Obtenersaludo/{nombre}/{edad}")]
            public string ObtenSaludo(string nombre, int edad)
            {
                return $"Hola {nombre} esta es la API,tienes {edad} años";
            }

            [HttpGet("ObtenColores")]
            public List<CatalogoColores> ObtenerColores()
            {
                return listcolores;
            }

            [HttpPost("AgregarColor")]
            public IActionResult AgregarColor([FromBody] CatalogoColores color)
            {
                listcolores.Add(color);
                return Ok();
            }

            [HttpDelete("EliminarColor/{id}")]
            public IActionResult EliminarColor(int id)
            {
                var color = listcolores.FirstOrDefault(c => c.id == id);
                if (color == null)
                {
                    return NotFound();
                }
                listcolores.Remove(color);
                return Ok();
            }

            [HttpPut("ActualizarColor/{id}")]
            public IActionResult ActualizarColor(int id, [FromBody] CatalogoColores colorActualizado)
            {
                var color = listcolores.FirstOrDefault(c => c.id == id);
                if (color == null)
                {
                    return NotFound();
                }
                color.NombreColor = colorActualizado.NombreColor;
                return Ok();
            }
        }
}
