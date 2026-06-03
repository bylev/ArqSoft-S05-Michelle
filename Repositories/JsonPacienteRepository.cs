using CitasApp.Interfaces;
using CitasApp.Models;
using System.Text.Json;

namespace CitasApp.Repositories
{
    public class JsonPacienteRepository : IPacienteRepository
    {
        private readonly string _filePath;

        public JsonPacienteRepository(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Data", "Pacientes.json");
        }

        public IEnumerable<Paciente> ObtenerTodos()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return Enumerable.Empty<Paciente>();

                var json = File.ReadAllText(_filePath);
                var pacientes = JsonSerializer.Deserialize<List<Paciente>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    ?? new List<Paciente>();

                return pacientes;
            }
            catch
            {
                return Enumerable.Empty<Paciente>();
            }
        }

        public Paciente ObtenerPorId(int id)
        {
            var pacientes = ObtenerTodos();
            return pacientes.FirstOrDefault(p => p.Id == id) ?? new Paciente();
        }
    }
}
