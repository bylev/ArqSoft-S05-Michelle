using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Observers
{
    public class EmailObserver : IObservador
    {
        public void Notificar(Cita cita)
        {
            Console.WriteLine($"[EMAIL] Confirmación enviada al paciente {cita.PacienteId} - motivo: {cita.Motivo} - estado: {cita.Estado}");
        }
    }
}
