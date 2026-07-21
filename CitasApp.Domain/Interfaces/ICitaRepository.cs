using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface ICitaRepository
    {
        IEnumerable<Cita> ObtenerTodos();
        IEnumerable<Cita> ObtenerPorPaciente(int pacienteId);
        Cita ObtenerPorId(int id);
        void Agregar(Cita cita);
        void Editar(Cita cita);
        void Eliminar(int id);
    }
}
