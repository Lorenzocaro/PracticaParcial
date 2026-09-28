using Microsoft.AspNetCore.Mvc;

namespace Ejercicio3.Controllers;

[ApiController]
[Route("[controller]")]
public class ParticipanteController : ControllerBase
{
    private static readonly List<Participante> participantes = new()
    {
        new Participante
        {
            Id = 1,
            Nombre = "Lorenzo",
            Email = "asd@gmail.com",
            Nivel = 2
        }
    };

    private static readonly List<Desafio> desafios = new()
    {
        new Desafio
        {
            Id = 1,
            Titulo = "c#",
            Dificultad = "alta",
            PuntajeMaximo = 100,
            Activo = true
        }
    };

    private readonly ILogger<ParticipanteController> _logger;

    public ParticipanteController(ILogger<ParticipanteController> logger)
    {
        _logger = logger;
    }

    [HttpPost]

    public IActionResult Create([FromBody] Participante nuevoParticipante)
    {
        if(nuevoParticipante.Id <= 0)
        {
            return BadRequest("El id no puede ser menor o igual a 0");
        }

        if (string.IsNullOrEmpty(nuevoParticipante.Nombre))
        {
            return BadRequest("El campo nombre es obligatorio");
        }

        if (string.IsNullOrEmpty(nuevoParticipante.Email))
        {
            return BadRequest("El campo email es obligatorio");
        }

        if(nuevoParticipante.Nivel <= 0)
        {
            return BadRequest("El nivel no puede ser menor o igual a 0");
        }

        return Ok(nuevoParticipante);
    }
}