using Microsoft.AspNetCore.Mvc;

namespace EstacionesMeteorologicas.Controllers;

[ApiController]
[Route("[controller]")]
public class SeasonController : ControllerBase
{
    private readonly ILogger<SeasonController> _logger;
    public static readonly List<Estacion> estaciones = new()
    {
        new Estacion
        {
            Id = 1,
            Nombre = "Verano",
            Localidad = "Cordoba",
            Activa = true,
        }
    };
    public SeasonController(ILogger<SeasonController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Estacion nuevaEstacion)
    {
        try
        {
            if (nuevaEstacion.Id <= 0)
            {
                return BadRequest("El id no puede ser menor o igual a 0");
            }

            bool idExists = estaciones.Any(E => E.Id == nuevaEstacion.Id);

            if (idExists)
            {
                return Conflict("El ID ya existe.");
            }

            if (string.IsNullOrWhiteSpace(nuevaEstacion.Nombre))
            {
                return BadRequest("Nombre es un campo obligatorio");
            }

            estaciones.Add(nuevaEstacion);
            return Ok("Estacion registrada correctamente.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ha ocurrido un error en el servidor {ex.Message}");
        }
    }

    [HttpGet("{id}/mediciones")]
    public IActionResult GetMeasuringsBySeason(int id)
    {
        try
        {
            var resultado = estaciones.FirstOrDefault(S => S.Id == id);

            if (resultado is null)
            {
                return NotFound("La estacion con el id ingresado no existe.");
            }

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ha ocurrido un error en el servidor {ex.Message}");
        }
    }

    [HttpGet("Localidad")]
    public IActionResult GetByPlace(string localidad)
    {
        try
        {
            if (estaciones.Count == 0)
            {
                return NotFound("No se encuentran estaciones registradas.");
            }

            var localidadEstacion = estaciones.Where(S => S.Localidad == localidad)
                                    .ToList();

            if (localidadEstacion.Count == 0)
            {
                return NotFound("No se encuentran estaciones registradas en esa localidad.");
            }

            return Ok(localidadEstacion);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ha ocurrido un error en el servidor {ex.Message}");
        }
    }

    
}