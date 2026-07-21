using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        IEnumerable<Paciente> ObtenerTodos();
        Paciente ObtenerPorId(int id);
        void Agregar(Paciente paciente);
        void Editar(Paciente paciente);
        void Eliminar(int id);
    }
}
