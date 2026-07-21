using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CitasApp.Infrastructure.Repositories
{
    public class EfPacienteRepository : IPacienteRepository
    {
        private readonly AppDbContext _context;

        public EfPacienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Paciente> ObtenerTodos()
        {
            return _context.Pacientes
                .AsNoTracking()
                .ToList();
        }

        public Paciente ObtenerPorId(int id)
        {
            return _context.Pacientes
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id) ?? new Paciente();
        }

        public void Agregar(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
        }

        public void Editar(Paciente paciente)
        {
            var existente = _context.Pacientes.FirstOrDefault(p => p.Id == paciente.Id);
            if (existente == null)
            {
                return;
            }

            existente.Nombre = paciente.Nombre;
            existente.Apellido = paciente.Apellido;
            existente.Email = paciente.Email;
            existente.Telefono = paciente.Telefono;

            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var paciente = _context.Pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente == null)
            {
                return;
            }

            _context.Pacientes.Remove(paciente);
            _context.SaveChanges();
        }
    }
}
