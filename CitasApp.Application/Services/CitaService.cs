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

     
        public (List<Cita> citas, List<Paciente> pacientes, List<Medico> medicos) ObtenerTodasLasCitas()
        {
            return (_citaRepo.ObtenerTodos().ToList(), _pacienteRepo.ObtenerTodos().ToList(), _medicoRepo.ObtenerTodos().ToList());
        }

 
        public (List<Cita> citas, List<Paciente> pacientes, List<Medico> medicos) ObtenerCitasPorPaciente(int pacienteId)
        {
            return (_citaRepo.ObtenerPorPaciente(pacienteId).ToList(), _pacienteRepo.ObtenerTodos().ToList(), _medicoRepo.ObtenerTodos().ToList());
        }

        public (List<Paciente> pacientes, List<Medico> medicos) ObtenerCatalogos()
        {
            return (_pacienteRepo.ObtenerTodos().ToList(), _medicoRepo.ObtenerTodos().ToList());
        }


        public (Cita cita, List<Paciente> pacientes, List<Medico> medicos) ObtenerCitaPorId(int id)
        {
            return (_citaRepo.ObtenerPorId(id), _pacienteRepo.ObtenerTodos().ToList(), _medicoRepo.ObtenerTodos().ToList());
        }


        public bool CrearCita(Cita cita)
        {
            if (cita == null)
                return false;

            _citaRepo.Agregar(cita);
            return true;
        }

        public bool EditarCita(Cita cita)
        {
            if (cita == null || cita.Id == 0)
                return false;

            _citaRepo.Editar(cita);
            return true;
        }


        public void EliminarCita(int id)
        {
            _citaRepo.Eliminar(id);
        }


        public Cita? ConfirmarCita(int id) // Long Method
        {
            var cita = ValidarCita(id);
            if (cita == null) return null;
            Confirmar(cita);
            GuardarCita(cita);
            RegistrarNotificacion(cita); 
            return cita;
        }

        public Cita? ValidarCita(int id)
        {
            var cita = _citaRepo.ObtenerPorId(id);
            return cita == null || cita.Id == 0 ? null : cita;
        }

        private void Confirmar(Cita cita)
        {
            cita.Estado = "Confirmado";
        }

        private void GuardarCita(Cita cita) 
        {
            _citaRepo.Editar(cita);
        }

        private void RegistrarNotificacion(Cita cita
            )
        { Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita {cita.Id} confirmada"); }
    }
}