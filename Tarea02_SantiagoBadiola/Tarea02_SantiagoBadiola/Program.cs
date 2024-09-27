using Tarea02_SantiagoBadiola.Controllers;

// Nuevo Proyecto -> ASP.NET Core MVC
// Click derecho -> Administrador de packetes NuGet -> Swashbuckle.AspNetCore
// Agregar swagger en launchSettings.json
// https://localhost:7034/api/tareas

var builder = WebApplication.CreateBuilder(args);


builder.Logging.AddConsole();

builder.Services.AddSingleton<TareaController>();

builder.Services.AddSingleton<TareaController>();


builder.Services.AddMvc().AddControllersAsServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
