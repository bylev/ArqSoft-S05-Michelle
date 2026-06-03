using CitasApp.Models;

namespace CitasApp.Interfaces
{
    public interface ICitaRepository
    {
        IEnumerable<Cita> ObtenerTodos();
        IEnumerable<Cita> ObtenerPorPaciente(int pacienteId);
    }
}
