using CitasApp.Models;

namespace CitasApp.Interfaces
{
    public interface IPacienteRepository
    {
        IEnumerable<Paciente> ObtenerTodos();
        Paciente ObtenerPorId(int id);
    }
}
