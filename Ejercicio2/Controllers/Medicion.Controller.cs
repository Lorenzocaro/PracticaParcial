using Microsoft.AspNetCore.Mvc;

namespace EstacionesMeteorologicas.Controllers;

[ApiController]
[Route("[controller]")]
public class MedicionController : ControllerBase
{
    private readonly ILogger<MedicionController> _logger;

    private static readonly List<Medicion> mediciones = new()
    {
        new Medicion
        {
            Id= 1,
            EstacionId= 1,
            Temperatura= 30,
            Humedad= 70,
            VelocidadViento=20,
            FechaHora= new DateTime(2026-09-28-12-00-00)

        }
    };

    public MedicionController(ILogger<MedicionController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Crear([FromBody] Medicion nuevaMedicion)
    {
        try
        {
            if (nuevaMedicion.Id <= 0)
            {
                return BadRequest("Ingresa un id valido.");
            }

            bool idExiste = mediciones.Any(medicion => medicion.Id == nuevaMedicion.Id);

            if (idExiste)
            {
                return Conflict("El ID ya existe.");
            }

            if (nuevaMedicion.EstacionId <= 0)
            {
                return BadRequest("Ingresa un id valido.");
            }

            if (nuevaMedicion.FechaHora > DateTime.Now)
            {
                return BadRequest("La fecha del registro no puede ser futura.");
            }

            if (nuevaMedicion.VelocidadViento < 0)
            {
                return BadRequest("La velocidad del viento no puede ser negativa.");
            }

            if (nuevaMedicion.Humedad < 0 || nuevaMedicion.Humedad > 100)
            {
                return BadRequest("La humedad debe estar entre 0 y 100.");
            }

            mediciones.Add(nuevaMedicion);

            return Ok("Medicion tomada exitosamente.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ha ocurrido un error en el servidor {ex.Message}");
        }
    }

    [HttpGet]
    public IActionResult ObtenerTodas()
    {
        try
        {

            if (mediciones.Count == 0)
            {
                return NotFound("No hay mediciones registradas aun.");
            }

            return Ok(mediciones);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ha ocurrido un error en el servidor {ex.Message}");
        }
    }
    

    [HttpGet("Temperature")]
    public IActionResult ObtenerPorTemperatura(double temperatura)
    {
        try
        {
            if (mediciones.Count == 0)
            {
                return NotFound("No hay mediciones registradas aun.");
            }

            var temperaturas = mediciones
                .Where(medicion => medicion.Temperatura >= temperatura)
                .ToList();

            if (temperaturas.Count == 0)
            {
                return NotFound($"No se registraron temperaturas mayores a {temperatura} grados.");
            }

            return Ok(temperaturas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ha ocurrido un error en el servidor {ex.Message}");
        }
    }

    [HttpGet("Order")]
    public IActionResult ObtenerPorOrden()
    {
        try
        {
            var orden = mediciones
                .OrderByDescending(medicion => medicion.Temperatura)
                .ToList();

            if (orden.Count == 0)
            {
                return NotFound("No hay mediciones registradas aun.");
            }

            return Ok(orden);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ha ocurrido un error en el servidor {ex.Message}");
        }
    }

    [HttpGet("Promedio")]
    public IActionResult ObtenerPromedio()
    {
        try
        {
            var promedio = mediciones.Average(medicion => medicion.Temperatura);

            if (promedio == 0)
            {
                return NotFound("No hay temperaturas registradas aun.");
            }

            return Ok(promedio);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ha ocurrido un error en el servidor {ex.Message}");
        }
    }

    [HttpGet("MaximaTemp")]
    public IActionResult ObtenerMaxima()
    {
        try
        {
            var temperaturaMaxima = mediciones
                .Max(medicion => medicion.Temperatura);

            if (temperaturaMaxima == 0)
            {
                return NotFound("No hay temperaturas registradas aun.");
            }

            return Ok(temperaturaMaxima);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ha ocurrido un error en el servidor {ex.Message}");
        }
    }

    [HttpGet("MinimaTemp")]
    public IActionResult ObtenerMinima()
    {
        try
        {
            var temperaturaMinima = mediciones
                .Min(medicion => medicion.Temperatura);

            if (temperaturaMinima == 0)
            {
                return NotFound("No hay temperaturas registradas aun.");
            }

            return Ok(temperaturaMinima);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ha ocurrido un error en el servidor {ex.Message}");
        }
    }
}