using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CitasApp.Infrastructure.Repositories
{
    public class EfCitaRepository : ICitaRepository
    {
        private readonly AppDbContext _context;

        public EfCitaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Cita> ObtenerTodos()
        {
            return _context.Citas
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Cita> ObtenerPorPaciente(int pacienteId)
        {
            return _context.Citas
                .AsNoTracking()
                .Where(c => c.PacienteId == pacienteId)
                .ToList();
        }

        public Cita ObtenerPorId(int id)
        {
            return _context.Citas
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id) ?? new Cita();
        }

        public void Agregar(Cita cita)
        {
            _context.Citas.Add(cita);
            _context.SaveChanges();
        }

        public void Editar(Cita cita)
        {
            var existente = _context.Citas.FirstOrDefault(c => c.Id == cita.Id);
            if (existente == null)
            {
                return;
            }

            existente.PacienteId = cita.PacienteId;
            existente.MedicoId = cita.MedicoId;
            existente.Fecha = cita.Fecha;
            existente.Hora = cita.Hora;
            existente.Motivo = cita.Motivo;
            existente.Estado = cita.Estado;

            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var cita = _context.Citas.FirstOrDefault(c => c.Id == id);
            if (cita == null)
            {
                return;
            }

            _context.Citas.Remove(cita);
            _context.SaveChanges();
        }
    }
}
