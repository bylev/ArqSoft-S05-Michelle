using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories
{
    public static class RepositoryFactory
    {
        public static IPacienteRepository CrearPacienteRepository(string entorno)
        {
            return entorno switch
            {
                "Production" => new MemoriaPacienteRepository(),
                _ => new JsonPacienteRepository()
            };
        }

        public static IMedicoRepository CrearMedicoRepository(string entorno)
        {
            return entorno switch
            {
                "Production" => new JsonMedicoRepository(),
                _ => new JsonMedicoRepository()
            };
        }

        public static ICitaRepository CrearCitaRepository(string entorno)
        {
            return entorno switch
            {
                "Production" => new JsonCitaRepository(),
                _ => new JsonCitaRepository()
            };
        }
    }
}