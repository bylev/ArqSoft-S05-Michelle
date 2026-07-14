using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaRepository _citaRepo;
        private readonly IPacienteRepository _pacienteRepo;
        private readonly IMedicoRepository _medicoRepo;

        public CitaController(ICitaRepository citaRepo,
                              IPacienteRepository pacienteRepo,
                              IMedicoRepository medicoRepo)
        {
            _citaRepo = citaRepo;
            _pacienteRepo = pacienteRepo;
            _medicoRepo = medicoRepo;
        }

        private void CargarCatologos() // Extract Method
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
        }

        public IActionResult Index()
        {
            CargarCatologos();
            return View(_citaRepo.ObtenerTodos());
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            CargarCatologos();
            return View(_citaRepo.ObtenerPorPaciente(pacienteId));
        }

        public IActionResult Create()
        {
            CargarCatologos();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cita cita)
        {
            if (ModelState.IsValid)
            {
                _citaRepo.Agregar(cita);
                return RedirectToAction("Index");
            }

            CargarCatologos();
            return View(cita);
        }

        public IActionResult Edit(int id)
        {
            var cita = _citaRepo.ObtenerPorId(id);
            if (cita.Id == 0)
                return NotFound();

            CargarCatologos();
            return View(cita);
        }

        [HttpPost]
        public IActionResult Edit(Cita cita)
        {
            if (ModelState.IsValid)
            {
                _citaRepo.Editar(cita);
                return RedirectToAction("Index");
            }

            CargarCatologos();
            return View(cita);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _citaRepo.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}