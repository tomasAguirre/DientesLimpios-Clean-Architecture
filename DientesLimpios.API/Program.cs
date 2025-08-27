using DientesLimpios.API.Middleware;
using DientesLimpios.Aplicacion;
using DientesLimpios.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AgregarServicioDeAplicacion();
builder.Services.AgregarServiciosDePersistencia();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseManejadorExcepciones();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
