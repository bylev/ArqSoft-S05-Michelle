using System;
using System.Collections.Generic;
using System.Text;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class MemoriaPacienteRepository : IPacienteRepository
    {
        private readonly List<Paciente> _pacientes = new()
        {
            new Paciente { Id = 1, Nombre = "Juan Pérez" },
            new Paciente { Id = 2, Nombre = "María García" },
            new Paciente { Id = 3, Nombre = "Carlos López" }
        };

        public IEnumerable<Paciente> ObtenerTodos() => _pacientes;

        public Paciente ObtenerPorId(int id) =>
            _pacientes.FirstOrDefault(p => p.Id == id);

        public void Agregar(Paciente paciente)
        {
            paciente.Id = _pacientes.Max(p => p.Id) + 1;
            _pacientes.Add(paciente);
        }

        public void Editar(Paciente paciente)
        {
            var index = _pacientes.FindIndex(p => p.Id == paciente.Id);
            if (index >= 0) _pacientes[index] = paciente;
        }

        public void Eliminar(int id)
        {
            var paciente = _pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente != null) _pacientes.Remove(paciente);
        }
    }
}