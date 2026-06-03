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
            _filePath = Path.Combine(env.ContentRootPath, "Data", "citas.json");
        }

        public IEnumerable<Cita> ObtenerTodos()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return Enumerable.Empty<Cita>();

                var json = File.ReadAllText(_filePath);
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var citas = new List<Cita>();
                    var citasArray = doc.RootElement.GetProperty("citas");

                    foreach (var citaElement in citasArray.EnumerateArray())
                    {
                        var cita = new Cita
                        {
                            Id = citaElement.GetProperty("id").GetInt32(),
                            PacienteId = citaElement.GetProperty("pacienteId").GetInt32(),
                            MedicoId = citaElement.GetProperty("medicoId").GetInt32(),
                            Fecha = DateOnly.Parse(citaElement.GetProperty("fecha").GetString() ?? ""),
                            Hora = TimeOnly.Parse(citaElement.GetProperty("hora").GetString() ?? ""),
                            Motivo = citaElement.GetProperty("motivo").GetString() ?? string.Empty,
                            Estado = citaElement.GetProperty("estado").GetString() ?? "Pendiente"
                        };
                        citas.Add(cita);
                    }

                    return citas;
                }
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
