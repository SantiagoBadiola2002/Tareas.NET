using Consigna2.Controllers;
using Consigna2.Infraestructure.Data;
using Consigna2.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IApartamentoRepository, ApartamentoRepository>();
builder.Services.AddSingleton<ICasaRepository, CasaRepository>();
builder.Services.AddSingleton<IChacraTuristaRepository, ChacraTuristaRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
