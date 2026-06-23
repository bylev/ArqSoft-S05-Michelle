using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Application.Services;

var builder = WebApplication.CreateBuilder(args);

var dataFolder = Path.Combine(builder.Environment.ContentRootPath, "Data");
Directory.CreateDirectory(dataFolder);

builder.Services.AddSingleton<IPacienteRepository>(sp =>
    new LoggingPacienteRepository(new JsonPacienteRepository()));
builder.Services.AddSingleton<IMedicoRepository, JsonMedicoRepository>();
builder.Services.AddSingleton<ICitaRepository, JsonCitaRepository>();

builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
