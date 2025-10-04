using Microsoft.EntityFrameworkCore;
using CooperaGame.Data;
using CooperaGame.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuracion conexion base de datos
builder.Services.AddDbContext<ApplicationDbContext>(
    opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<PartidaService>(); // services donde estan los metodos que calculan cosas de la partida
builder.Services.AddScoped<EstadisticasService>(); // service donde se calcula de muy mala manera las estadistica de c/jugador en la partida

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
