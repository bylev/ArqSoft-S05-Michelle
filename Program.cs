using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddScoped<ICitaRepository, JsonCitaRepository>();
builder.Services.AddScoped<IMedicoRepository, JsonMedicoRepository>();
// Implementación de un nuevo Port para comprobar la arquitectura hexagonal funciona.
//builder.Services.AddScoped<IPacienteRepository, MemoriaPacienteRepository>();
builder.Services.AddScoped<IPacienteRepository, JsonPacienteRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();