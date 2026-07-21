using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CitasApp.Infrastructure.Repositories
{
    public class EfMedicoRepository : IMedicoRepository
    {
        private readonly AppDbContext _context;

        public EfMedicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Medico> ObtenerTodos()
        {
            return _context.Medicos
                .AsNoTracking()
                .ToList();
        }

        public Medico ObtenerPorId(int id)
        {
            return _context.Medicos
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id) ?? new Medico();
        }

        public void Agregar(Medico medico)
        {
            _context.Medicos.Add(medico);
            _context.SaveChanges();
        }

        public void Editar(Medico medico)
        {
            var existente = _context.Medicos.FirstOrDefault(m => m.Id == medico.Id);
            if (existente == null)
            {
                return;
            }

            existente.Nombre = medico.Nombre;
            existente.Apellido = medico.Apellido;
            existente.Especialidad = medico.Especialidad;
            existente.NumeroLicencia = medico.NumeroLicencia;

            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var medico = _context.Medicos.FirstOrDefault(m => m.Id == id);
            if (medico == null)
            {
                return;
            }

            _context.Medicos.Remove(medico);
            _context.SaveChanges();
        }
    }
}
