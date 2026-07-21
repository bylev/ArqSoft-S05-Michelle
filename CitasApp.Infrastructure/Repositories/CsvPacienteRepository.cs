// CitasApp.Infrastructure/Repositories/CsvPacienteRepository.cs
// Adapter de salida — implementa IPacienteRepository leyendo un archivo CSV

using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class CsvPacienteRepository : IPacienteRepository
    {
        private readonly string _filePath;

        public CsvPacienteRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "Id,Nombre,Apellido,Email,Telefono\n");
        }

        private List<Paciente> LeerTodos()
        {
            var lista = new List<Paciente>();

            foreach (var linea in File.ReadAllLines(_filePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var p = linea.Split(',');
                if (p.Length < 5) continue;

                lista.Add(new Paciente
                {
                    Id = int.Parse(p[0]),
                    Nombre = p[1],
                    Apellido = p[2],
                    Email = p[3],
                    Telefono = p[4]
                });
            }

            return lista;
        }

        private void EscribirTodos(List<Paciente> pacientes)
        {
            var lineas = new List<string>
                { "Id,Nombre,Apellido,Email,Telefono" };

            foreach (var p in pacientes)
                lineas.Add($"{p.Id},{p.Nombre},{p.Apellido},{p.Email},{p.Telefono}");

            File.WriteAllLines(_filePath, lineas);
        }

        public IEnumerable<Paciente> ObtenerTodos() => LeerTodos();

        public Paciente ObtenerPorId(int id) =>
            LeerTodos().FirstOrDefault(p => p.Id == id);

        public void Agregar(Paciente paciente)
        {
            var pacientes = LeerTodos();
            paciente.Id = pacientes.Count > 0 ? pacientes.Max(p => p.Id) + 1 : 1;
            pacientes.Add(paciente);
            EscribirTodos(pacientes);
        }

        public void Editar(Paciente paciente)
        {
            var pacientes = LeerTodos();
            var index = pacientes.FindIndex(p => p.Id == paciente.Id);
            if (index >= 0) { pacientes[index] = paciente; EscribirTodos(pacientes); }
        }

        public void Eliminar(int id)
        {
            var pacientes = LeerTodos();
            pacientes.RemoveAll(p => p.Id == id);
            EscribirTodos(pacientes);
        }
    }
}