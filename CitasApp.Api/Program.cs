using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Infrastructure.Observers;
using CitasApp.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar entorno
var entorno = builder.Environment.IsProduction() ? "Production" : "Development";

// Registrar Repositorios con Factory y Decorator
builder.Services.AddScoped<IPacienteRepository>(sp =>
    new LoggingPacienteRepository(RepositoryFactory.CrearPacienteRepository(entorno)));
builder.Services.AddScoped<IMedicoRepository, JsonMedicoRepository>();
builder.Services.AddScoped<ICitaRepository, JsonCitaRepository>();

// Registrar ObservadoresSystem.IO.FileLoadException
builder.Services.AddScoped<IObservador, SmsObserver>();
builder.Services.AddScoped<IObservador, EmailObserver>();

// Registrar Servicios
builder.Services.AddScoped<CitaService>(sp =>
{
    var citaService = new CitaService(
        sp.GetRequiredService<ICitaRepository>(),
        sp.GetRequiredService<IPacienteRepository>(),
        sp.GetRequiredService<IMedicoRepository>()
    );
    var observadores = sp.GetServices<IObservador>();
    foreach (var observador in observadores)
        citaService.AgregarObservador(observador);
    return citaService;
});
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("PermitirTodo");
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();
app.Run();
