using CitasApp.Application.Services;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly PacienteService _service;
        public PacientesController(PacienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.ObtenerTodos());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var paciente = _service.ObtenerPorId(id);
            return paciente == null ? NotFound() : Ok(paciente);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Paciente paciente)
        {
            if (paciente == null)
                return BadRequest("Paciente no puede ser nulo");

            var resultado = _service.CrearPaciente(paciente);
            return resultado ? CreatedAtAction(nameof(GetById), new { id = paciente.Id }, paciente)
                           : BadRequest("Error al crear paciente");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Paciente paciente)
        {
            if (paciente == null)
                return BadRequest("Paciente no puede ser nulo");

            paciente.Id = id;
            var resultado = _service.EditarPaciente(paciente);
            return resultado ? Ok(paciente) : BadRequest("Error al editar paciente");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.EliminarPaciente(id);
            return NoContent();
        }
    }
}