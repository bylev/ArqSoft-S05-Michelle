// CitasApp.Infrastructure/Repositories/CsvMedicoRepository.cs
// Adapter de salida — implementa IMedicoRepository leyendo un archivo CSV

using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class CsvMedicoRepository : IMedicoRepository
    {
        private readonly string _filePath;

        public CsvMedicoRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "Id,Nombre,Apellido,Especialidad,NumeroLicencia\n");
        }

        private List<Medico> LeerTodos()
        {
            var lista = new List<Medico>();

            foreach (var linea in File.ReadAllLines(_filePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var p = linea.Split(',');
                if (p.Length < 5) continue;

                lista.Add(new Medico
                {
                    Id = int.Parse(p[0]),
                    Nombre = p[1],
                    Apellido = p[2],
                    Especialidad = p[3],
                    NumeroLicencia = p[4]
                });
            }

            return lista;
        }

        private void EscribirTodos(List<Medico> medicos)
        {
            var lineas = new List<string>
                { "Id,Nombre,Apellido,Especialidad,NumeroLicencia" };

            foreach (var m in medicos)
                lineas.Add($"{m.Id},{m.Nombre},{m.Apellido},{m.Especialidad},{m.NumeroLicencia}");

            File.WriteAllLines(_filePath, lineas);
        }

        public IEnumerable<Medico> ObtenerTodos() => LeerTodos();

        public Medico ObtenerPorId(int id) =>
            LeerTodos().FirstOrDefault(m => m.Id == id);

        public void Agregar(Medico medico)
        {
            var medicos = LeerTodos();
            medico.Id = medicos.Count > 0 ? medicos.Max(m => m.Id) + 1 : 1;
            medicos.Add(medico);
            EscribirTodos(medicos);
        }

        public void Editar(Medico medico)
        {
            var medicos = LeerTodos();
            var index = medicos.FindIndex(m => m.Id == medico.Id);
            if (index >= 0) { medicos[index] = medico; EscribirTodos(medicos); }
        }

        public void Eliminar(int id)
        {
            var medicos = LeerTodos();
            medicos.RemoveAll(m => m.Id == id);
            EscribirTodos(medicos);
        }
    }
}