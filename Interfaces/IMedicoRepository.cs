using CitasApp.Models;

namespace CitasApp.Interfaces
{
    public interface IMedicoRepository
    {
        IEnumerable<Medico> ObtenerTodos();
        Medico ObtenerPorId(int id);
    }
}
