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
            _filePath = Path.Combine(env.ContentRootPath, "Data", "citas.json");
        }

        public IEnumerable<Medico> ObtenerTodos()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return Enumerable.Empty<Medico>();

                var json = File.ReadAllText(_filePath);
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var medicos = new List<Medico>();
                    var medicosArray = doc.RootElement.GetProperty("medicos");

                    foreach (var medicoElement in medicosArray.EnumerateArray())
                    {
                        var medico = new Medico
                        {
                            Id = medicoElement.GetProperty("id").GetInt32(),
                            Nombre = medicoElement.GetProperty("nombre").GetString() ?? string.Empty,
                            Apellido = medicoElement.GetProperty("apellido").GetString() ?? string.Empty,
                            Especialidad = medicoElement.GetProperty("especialidad").GetString() ?? string.Empty,
                            NumeroLicencia = medicoElement.GetProperty("numeroLicencia").GetInt32()
                        };
                        medicos.Add(medico);
                    }

                    return medicos;
                }
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
