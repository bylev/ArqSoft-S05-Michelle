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
            _filePath = Path.Combine(env.ContentRootPath, "Data", "citas.json");
        }

        public IEnumerable<Paciente> ObtenerTodos()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return Enumerable.Empty<Paciente>();

                var json = File.ReadAllText(_filePath);
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var pacientes = new List<Paciente>();
                    var pacientesArray = doc.RootElement.GetProperty("pacientes");

                    foreach (var pacienteElement in pacientesArray.EnumerateArray())
                    {
                        var paciente = new Paciente
                        {
                            Id = pacienteElement.GetProperty("id").GetInt32(),
                            Nombre = pacienteElement.GetProperty("nombre").GetString() ?? string.Empty,
                            Apellido = pacienteElement.GetProperty("apellido").GetString() ?? string.Empty,
                            Email = pacienteElement.GetProperty("email").GetString() ?? string.Empty,
                            Telegono = pacienteElement.GetProperty("telegono").GetString() ?? string.Empty
                        };
                        pacientes.Add(paciente);
                    }

                    return pacientes;
                }
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
