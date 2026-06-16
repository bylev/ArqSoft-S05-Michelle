using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class PacienteService
    {
        private readonly IPacienteRepository _pacienteRepo;

        public PacienteService(IPacienteRepository pacienteRepo)
        {
            _pacienteRepo = pacienteRepo;
        }

        public List<Paciente> ObtenerTodos()
        {
            return _pacienteRepo.ObtenerTodos().ToList();
        }

        public Paciente? ObtenerPorId(int id)
        {
            return _pacienteRepo.ObtenerPorId(id);
        }

        public bool CrearPaciente(Paciente paciente)
        {
            if (paciente == null)
                return false;

            _pacienteRepo.Agregar(paciente);
            return true;
        }

        public bool EditarPaciente(Paciente paciente)
        {
            if (paciente == null || paciente.Id == 0)
                return false;

            _pacienteRepo.Editar(paciente);
            return true;
        }

        public void EliminarPaciente(int id)
        {
            _pacienteRepo.Eliminar(id);
        }
    }
}
