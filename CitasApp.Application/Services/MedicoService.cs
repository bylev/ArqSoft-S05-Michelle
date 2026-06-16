using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class MedicoService
    {
        private readonly IMedicoRepository _medicoRepo;

        public MedicoService(IMedicoRepository medicoRepo)
        {
            _medicoRepo = medicoRepo;
        }

        public List<Medico> ObtenerTodos()
        {
            return _medicoRepo.ObtenerTodos().ToList();
        }

        public Medico? ObtenerPorId(int id)
        {
            return _medicoRepo.ObtenerPorId(id);
        }

        public bool CrearMedico(Medico medico)
        {
            if (medico == null)
                return false;

            _medicoRepo.Agregar(medico);
            return true;
        }

        public bool EditarMedico(Medico medico)
        {
            if (medico == null || medico.Id == 0)
                return false;

            _medicoRepo.Editar(medico);
            return true;
        }

        public void EliminarMedico(int id)
        {
            _medicoRepo.Eliminar(id);
        }
    }
}
