using CitasApp.Interfaces;
using CitasApp.Models;
using System.Text.Json;

namespace CitasApp.Repositories
{
    public class JsonMedicoRepository : IMedicoRepository
    {
        private readonly string _filePath;

        public JsonMedicoRepository(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Data", "Medicos.json");
        }

        public IEnumerable<Medico> ObtenerTodos()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return Enumerable.Empty<Medico>();

                var json = File.ReadAllText(_filePath);
                var medicos = JsonSerializer.Deserialize<List<Medico>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    ?? new List<Medico>();

                return medicos;
            }
            catch
            {
                return Enumerable.Empty<Medico>();
            }
        }

        public Medico ObtenerPorId(int id)
        {
            var medicos = ObtenerTodos();
            return medicos.FirstOrDefault(m => m.Id == id) ?? new Medico();
        }
    }
}
