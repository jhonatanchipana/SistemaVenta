using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Venta.CMS.Middleware;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Venta.Data.Repository;
using Venta.Dto.Object.Account;
using Venta.Services.Bussiness;
using Venta.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

//
builder.Services.AddTransient<SignInManager<UserDTO>>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentityCore<UserDTO>();

//Inyeccion de servicios
builder.Services.AddApplicationDependencies(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews(opciones =>
{
    //opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
});

builder.Services.AddDbContext<ApplicationContext>(opciones =>
{
    opciones.UseSqlServer("name=DefaultConnection");
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, opciones =>
{
    opciones.LoginPath = "/account/login";
});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
