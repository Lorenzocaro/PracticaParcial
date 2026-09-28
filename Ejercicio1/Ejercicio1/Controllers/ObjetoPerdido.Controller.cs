using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Microsoft.OpenApi.MicrosoftExtensions;

namespace Ejercicio1.Controllers;

[ApiController]
[Route("[controller]")]
public class ObjetoPerdidoController : ControllerBase
{
    private static readonly List<ObjetoPerdido> objetosPerdidos = new()
    {
        new ObjetoPerdido
        {
            Id = 1 ,
            Description = "Camara",
            Categoria = "Tecnologia",
            LugarEncontrado = "Embalse",
            FechaEncontrado = DateOnly.Parse("2026-09-26"),
            Reclamado = false,
            NombrePersonaQueRetiro = "Juan"
        }
    };

    private readonly ILogger<ObjetoPerdidoController> _logger;

    public ObjetoPerdidoController(ILogger<ObjetoPerdidoController> logger)
    {
        _logger = logger;
    }

    [HttpPost]

    public IActionResult Create([FromBody] ObjetoPerdido objetoNuevo)
    {
        try
        {
            bool idExist = objetosPerdidos.Any(x => x.Id == objetoNuevo.Id);

            if (idExist)
            {
                return BadRequest("Ya existe un objeto con ese Id");
            }

            if (objetoNuevo.Reclamado)
            {
                return BadRequest("Este objeto ya fue reclamado");
            }

            if (objetoNuevo.FechaEncontrado > DateOnly.FromDateTime(DateTime.Now))
            {
                return BadRequest("La fecha tiene que ser menor al dia de hoy");
            }

            objetosPerdidos.Add(objetoNuevo);

            return Ok(objetoNuevo);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error del servidor: {ex.Message}");
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            if (objetosPerdidos.Count <= 0)
            {
                return Ok("La lista se encuentra vacia");
            }

            return Ok(objetosPerdidos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrio un problema en el servidor {ex.Message}.");
        }
    }

    [HttpGet("{id}")]

    public IActionResult GetById(int id)
    {
        try
        {
            ObjetoPerdido resultado = objetosPerdidos.FirstOrDefault(x => x.Id == id);

            if (resultado is null)
            {
                return BadRequest("Ningun objeto cuenta con este id");
            }

            if (id == 0)
            {
                return BadRequest("El id no puede ser 0");
            }

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrio un problema en el servidor {ex.Message}.");
        }
    }

    [HttpPut]

    public IActionResult Update([FromBody] ObjetoPerdido objetoActualizado)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(objetoActualizado.Description))
            {
                return BadRequest("Campo obligatorio");
            }

            if (string.IsNullOrWhiteSpace(objetoActualizado.Categoria))
            {
                return BadRequest("Campo obligatorio");
            }

            if (string.IsNullOrWhiteSpace(objetoActualizado.LugarEncontrado))
            {
                return BadRequest("Campo obligatorio");
            }

            if (string.IsNullOrWhiteSpace(objetoActualizado.NombrePersonaQueRetiro))
            {
                return BadRequest("Campo obligatorio");
            }

            if (objetoActualizado.FechaEncontrado > DateOnly.FromDateTime(DateTime.Now))
            {
                return BadRequest("La fecha ingresada tiene que ser menor a la actual");
            }

            var resultado = objetosPerdidos.FirstOrDefault(op => op.Id == objetoActualizado.Id);

            if (resultado is null)
            {
                return BadRequest("No existe un objeto con este id");
            }

            resultado.Categoria = objetoActualizado.Categoria;
            resultado.Description = objetoActualizado.Description;
            resultado.FechaEncontrado = objetoActualizado.FechaEncontrado;
            resultado.LugarEncontrado = objetoActualizado.LugarEncontrado;
            resultado.NombrePersonaQueRetiro = objetoActualizado.NombrePersonaQueRetiro;
            resultado.Reclamado = objetoActualizado.Reclamado;
            resultado.Id = objetoActualizado.Id;

            return Ok("Objeto modificado correctamente");

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en el servidor {ex.Message}");
        }
    }


    [HttpDelete]

    public IActionResult Delete(int id)
    {
        try
        {
            ObjetoPerdido resultado = objetosPerdidos.FirstOrDefault(OP => OP.Id == id);

            if (resultado is null)
            {
                return BadRequest("No se encontro ningun objeto perdido con este id");
            }

            objetosPerdidos.Remove(resultado);

            return Ok("Objeto eliminado con exito");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error del servidor {ex.Message}");
        }
    }

    [HttpGet("Descripcion")]

    public IActionResult GetByDescription([FromBody] string descripcionObjeto)
    {
        try
        {
            ObjetoPerdido resultado = objetosPerdidos.FirstOrDefault(x => x.Description == descripcionObjeto);

            if (resultado is null)
            {
                return BadRequest("Ingrese un objeto");
            }

            return Ok(resultado);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en el servidor {ex.Message}");
        }
    }

    [HttpGet("Categoria")]

    public IActionResult GetByCategory([FromBody] string categoriaObjeto)
    {
        try
        {
            var resultado = objetosPerdidos.Where(OP => OP.Categoria == categoriaObjeto).ToList();

            if (resultado is null)
            {
                return BadRequest("No existen objetos de esta categoria");
            }

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en el servidor {ex.Message}");
        }
    }

    [HttpGet("No Reclamados")]

    public IActionResult NotClaimed()
    {
        try
        {
            var resultado = objetosPerdidos.Where(OP => OP.Reclamado == false).ToList();

            if (resultado.Count == 0)
            {
                return BadRequest("No hay objetos sin reclamar");
            }

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en el servidor {ex.Message}");
        }
    }

    [HttpGet("Fecha Reclamada")]

    public IActionResult GetByDate([FromBody] string fecha)
    {
        try
        {
            var resultado = objetosPerdidos.Where(OP => OP.Reclamado == true)
                                           .Where(OP => OP.FechaEncontrado >= DateOnly.Parse(fecha))
                                           .OrderDescending()
                                           .ToList();

            if (resultado.Count == 0)
            {
                return BadRequest("No hay objetos reclamados despues de esta fecha");
            }

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en el servidor {ex.Message}");
        }
    }

    [HttpPatch]

    public IActionResult ChangeStatus(int id)
    {
        ObjetoPerdido objetoNoReclamados = objetosPerdidos.Where(OP => OP.Reclamado == false)
                                                .FirstOrDefault(OP => OP.Id == id);
        
        if(objetoNoReclamados is null)
        {
            return BadRequest("No hay objetos sin reclamar con este id");
        }

        objetoNoReclamados.Reclamado = true;

        return Ok("Estado cambiado con exito");
    }
    
    



}