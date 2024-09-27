using Microsoft.AspNetCore.SignalR;
using Radzen;
using Tarea04_Consigna3.Client.Pages;
using Tarea04_Consigna3.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents(); // Añadir WebAssembly

// Para que funcionen los componentes
builder.Services.AddRadzenComponents();

// Agregar servicios de SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Añadir soporte para ambos modos de renderizado
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode(); // Añadir renderizado WebAssembly

// Configura el hub de SignalR
app.MapHub<LoginHub>("/loginHub");

app.Run();
