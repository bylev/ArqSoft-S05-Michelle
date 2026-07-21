using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    // DECORATOR — agrega logging sin modificar el repositorio original
    public class LoggingPacienteRepository : IPacienteRepository
    {
        private readonly IPacienteRepository _inner;

        public LoggingPacienteRepository(IPacienteRepository inner)
        {
            _inner = inner;
        }

        public IEnumerable<Paciente> ObtenerTodos()
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerTodos — inicio");
            var resultado = _inner.ObtenerTodos();
            var lista = resultado.ToList();
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerTodos — {lista.Count} registros");
            return lista;
        }

        public Paciente ObtenerPorId(int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerPorId({id}) — inicio");
            var resultado = _inner.ObtenerPorId(id);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerPorId({id}) — encontrado");
            return resultado;
        }

        public void Agregar(Paciente paciente)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Agregar paciente — inicio");
            _inner.Agregar(paciente);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Agregar paciente — completado");
        }

        public void Editar(Paciente paciente)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Editar paciente — inicio");
            _inner.Editar(paciente);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Editar paciente — completado");
        }

        public void Eliminar(int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Eliminar paciente({id}) — inicio");
            _inner.Eliminar(id);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Eliminar paciente({id}) — completado");
        }
    }
}