using System.Security.Claims;
using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Tests.Controllers;

public class CitaControllerTests
{
    private static CitaController CreateController(
        List<Cita>? citas = null,
        List<Paciente>? pacientes = null,
        List<Medico>? medicos = null,
        string userName = "admin@citasapp.com")
    {
        citas ??= new List<Cita>
        {
            new() { Id = 1, PacienteId = 10, MedicoId = 1, Estado = "Pendiente", Motivo = "Consulta" },
            new() { Id = 2, PacienteId = 20, MedicoId = 1, Estado = "Confirmado", Motivo = "Control" },
            new() { Id = 3, PacienteId = 10, MedicoId = 1, Estado = "Pendiente", Motivo = "Chequeo" }
        };

        pacientes ??= new List<Paciente>
        {
            new() { Id = 10, Nombre = "Ana", Apellido = "Pérez", Email = "ana@correo.com", Telefono = "111111111" },
            new() { Id = 20, Nombre = "Luis", Apellido = "Gómez", Email = "luis@correo.com", Telefono = "222222222" }
        };

        medicos ??= new List<Medico>
        {
            new() { Id = 1, Nombre = "Dr. Pérez", Apellido = "Lopez", Especialidad = "Medicina general", NumeroLicencia = "LIC-001" }
        };

        var citaRepo = new FakeCitaRepository(citas);
        var pacienteRepo = new FakePacienteRepository(pacientes);
        var medicoRepo = new FakeMedicoRepository(medicos);

        var controller = new CitaController(citaRepo, pacienteRepo, medicoRepo);

        var claims = new List<Claim> { new(ClaimTypes.Name, userName) };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) }
        };

        return controller;
    }

    [Fact]
    public void Index_DevuelveTodasLasCitas()
    {
        var controller = CreateController();

        var result = controller.Index() as ViewResult;

        Assert.NotNull(result);
        var modelo = Assert.IsAssignableFrom<IEnumerable<Cita>>(result!.Model);
        Assert.Equal(3, modelo.Count());
    }

    [Fact]
    public void Index_CargaCatalogosEnViewBag()
    {
        var controller = CreateController();

        controller.Index();

        Assert.NotNull(controller.ViewBag.Pacientes);
        Assert.NotNull(controller.ViewBag.Medicos);
    }

    [Fact]
    public void PorPaciente_FiltraPorPacienteId()
    {
        var controller = CreateController();

        var result = controller.PorPaciente(10) as ViewResult;

        Assert.NotNull(result);
        var modelo = Assert.IsAssignableFrom<IEnumerable<Cita>>(result!.Model);
        Assert.All(modelo, cita => Assert.Equal(10, cita.PacienteId));
    }

    [Fact]
    public void Create_Post_Valido_RedireccionaAIndex()
    {
        var controller = CreateController();
        var nuevaCita = new Cita
        {
            PacienteId = 10,
            MedicoId = 1,
            Fecha = DateOnly.FromDateTime(DateTime.Today),
            Hora = new TimeOnly(10, 30),
            Motivo = "Nueva cita",
            Estado = "Pendiente"
        };

        var result = controller.Create(nuevaCita);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
    }

    [Fact]
    public void ConfirmarCita_CambiaEstadoAYGuarda()
    {
        var citas = new List<Cita>
        {
            new() { Id = 1, PacienteId = 10, MedicoId = 1, Estado = "Pendiente", Motivo = "Consulta" }
        };
        var controller = CreateController(citas: citas);

        var service = new CitaService(
            new FakeCitaRepository(citas),
            new FakePacienteRepository(new List<Paciente>()),
            new FakeMedicoRepository(new List<Medico>()));

        var confirmada = service.ConfirmarCita(1);

        Assert.NotNull(confirmada);
        Assert.Equal("Confirmado", confirmada!.Estado);
    }

    private sealed class FakeCitaRepository : ICitaRepository
    {
        private readonly List<Cita> _data;

        public FakeCitaRepository(List<Cita> data) => _data = data;

        public IEnumerable<Cita> ObtenerTodos() => _data;

        public IEnumerable<Cita> ObtenerPorPaciente(int pacienteId) => _data.Where(c => c.PacienteId == pacienteId).ToList();

        public Cita ObtenerPorId(int id) => _data.FirstOrDefault(c => c.Id == id) ?? new Cita();

        public void Agregar(Cita cita) => _data.Add(cita);

        public void Editar(Cita cita)
        {
            var existente = _data.FirstOrDefault(c => c.Id == cita.Id);
            if (existente is null)
            {
                return;
            }

            existente.PacienteId = cita.PacienteId;
            existente.MedicoId = cita.MedicoId;
            existente.Fecha = cita.Fecha;
            existente.Hora = cita.Hora;
            existente.Motivo = cita.Motivo;
            existente.Estado = cita.Estado;
        }

        public void Eliminar(int id)
        {
            var cita = _data.FirstOrDefault(c => c.Id == id);
            if (cita is not null)
            {
                _data.Remove(cita);
            }
        }
    }

    private sealed class FakePacienteRepository : IPacienteRepository
    {
        private readonly List<Paciente> _data;

        public FakePacienteRepository(List<Paciente> data) => _data = data;

        public IEnumerable<Paciente> ObtenerTodos() => _data;
        public Paciente ObtenerPorId(int id) => _data.FirstOrDefault(p => p.Id == id) ?? new Paciente();
        public void Agregar(Paciente paciente) => _data.Add(paciente);
        public void Editar(Paciente paciente) { }
        public void Eliminar(int id) { }
    }

    private sealed class FakeMedicoRepository : IMedicoRepository
    {
        private readonly List<Medico> _data;

        public FakeMedicoRepository(List<Medico> data) => _data = data;

        public IEnumerable<Medico> ObtenerTodos() => _data;
        public Medico ObtenerPorId(int id) => _data.FirstOrDefault(m => m.Id == id) ?? new Medico();
        public void Agregar(Medico medico) => _data.Add(medico);
        public void Editar(Medico medico) { }
        public void Eliminar(int id) { }
    }
}
