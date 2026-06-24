using CitasApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly CitaService _citaService;
        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;

        public CitasController(CitaService citaService, PacienteService pacienteService, MedicoService medicoService)
        {
            _citaService = citaService;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var (citas, pacientes, medicos) = _citaService.ObtenerTodasLasCitas();
            return Ok(new { citas, pacientes, medicos });
        }

        [HttpGet("porpaciente/{pacienteId}")]
        public IActionResult PorPaciente(int pacienteId)
        {
            var (citas, pacientes, medicos) = _citaService.ObtenerCitasPorPaciente(pacienteId);
            return citas.Count == 0 ? NotFound() : Ok(new { citas, pacientes, medicos });
        }

        [HttpPost("confirmar/{citaId}")]
        public IActionResult Confirmar(int citaId)
        {
            var cita = _citaService.ConfirmarCita(citaId);
            if (cita == null)
                return NotFound(new { mensaje = "Cita no encontrada" });

            return Ok(new { mensaje = "Cita confirmada", cita });
        }
    }
}
