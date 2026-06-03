using CitasApp.Interfaces;
using CitasApp.Models;
using System.Text.Json;

namespace CitasApp.Repositories
{
    public class JsonCitaRepository : ICitaRepository
    {
        private readonly string _filePath;

        public JsonCitaRepository(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Data", "Citas.json");
        }

        public IEnumerable<Cita> ObtenerTodos()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return Enumerable.Empty<Cita>();

                var json = File.ReadAllText(_filePath);
                var citas = JsonSerializer.Deserialize<List<Cita>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    ?? new List<Cita>();

                return citas;
            }
            catch
            {
                return Enumerable.Empty<Cita>();
            }
        }

        public IEnumerable<Cita> ObtenerPorPaciente(int pacienteId)
        {
            var citas = ObtenerTodos();
            return citas.Where(c => c.PacienteId == pacienteId);
        }
    }
}
