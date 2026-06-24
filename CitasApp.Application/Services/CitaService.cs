using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _citaRepo;
        private readonly IPacienteRepository _pacienteRepo;
        private readonly IMedicoRepository _medicoRepo;
        private readonly List<IObservador> _observadores = new();

        public CitaService(ICitaRepository citaRepo,
                          IPacienteRepository pacienteRepo,
                          IMedicoRepository medicoRepo)
        {
            _citaRepo = citaRepo;
            _pacienteRepo = pacienteRepo;
            _medicoRepo = medicoRepo;
        }

        public void AgregarObservador(IObservador observador)
        {
            _observadores.Add(observador);
        }

        private void NotificarObservadores(Cita cita)
        {
            foreach (var observador in _observadores)
                observador.Notificar(cita);
        }

        /// <summary>
        /// Obtiene todas las citas junto con los catálogos de pacientes y médicos
        /// </summary>
        public (List<Cita> citas, List<Paciente> pacientes, List<Medico> medicos) ObtenerTodasLasCitas()
        {
            return (_citaRepo.ObtenerTodos().ToList(), _pacienteRepo.ObtenerTodos().ToList(), _medicoRepo.ObtenerTodos().ToList());
        }

        /// <summary>
        /// Obtiene citas filtradas por paciente junto con los catálogos
        /// </summary>
        public (List<Cita> citas, List<Paciente> pacientes, List<Medico> medicos) ObtenerCitasPorPaciente(int pacienteId)
        {
            return (_citaRepo.ObtenerPorPaciente(pacienteId).ToList(), _pacienteRepo.ObtenerTodos().ToList(), _medicoRepo.ObtenerTodos().ToList());
        }

        /// <summary>
        /// Obtiene los catálogos de pacientes y médicos para formularios
        /// </summary>
        public (List<Paciente> pacientes, List<Medico> medicos) ObtenerCatalogos()
        {
            return (_pacienteRepo.ObtenerTodos().ToList(), _medicoRepo.ObtenerTodos().ToList());
        }

        /// <summary>
        /// Obtiene una cita por ID junto con los catálogos para edición
        /// </summary>
        public (Cita cita, List<Paciente> pacientes, List<Medico> medicos) ObtenerCitaPorId(int id)
        {
            return (_citaRepo.ObtenerPorId(id), _pacienteRepo.ObtenerTodos().ToList(), _medicoRepo.ObtenerTodos().ToList());
        }

        /// <summary>
        /// Crea una nueva cita con validación básica
        /// </summary>
        public bool CrearCita(Cita cita)
        {
            if (cita == null)
                return false;

            _citaRepo.Agregar(cita);
            return true;
        }

        /// <summary>
        /// Edita una cita existente con validación
        /// </summary>
        public bool EditarCita(Cita cita)
        {
            if (cita == null || cita.Id == 0)
                return false;

            _citaRepo.Editar(cita);
            return true;
        }

        /// <summary>
        /// Elimina una cita por su ID
        /// </summary>
        public void EliminarCita(int id)
        {
            _citaRepo.Eliminar(id);
        }

        /// <summary>
        /// Confirma una cita por su ID
        /// </summary>
        public Cita? ConfirmarCita(int id)
        {
            var cita = _citaRepo.ObtenerPorId(id);
            if (cita == null || cita.Id == 0)
                return null;

            cita.Estado = "Confirmado";
            _citaRepo.Editar(cita);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita {cita.Id} confirmada");
            NotificarObservadores(cita);
            return cita;
        }
    }
}