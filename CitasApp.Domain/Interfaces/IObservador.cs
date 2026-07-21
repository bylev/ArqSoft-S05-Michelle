using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface IObservador
    {
        void Notificar(Cita cita);
    }
}
