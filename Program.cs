using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Tescat.Services;
using Tescat.Models;
using Tescat.Services.UserCredentials;
using Tescat.Services.Users;
using CurrieTechnologies.Razor.SweetAlert2;
using Tescat.Services.Emails;
using Tescat.Services.Pcs;
using Tescat.Services.Storages;
using Tescat.Services.MemoryRams;
using Tescat.Services.Cpus;
using Tescat.Services.Motherboards;
using Tescat.Services.PowerSupplys;
using Tescat.Services.Pc_Credentials;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Tescat.Areas.Identity;
using Microsoft.Extensions.Options;
//using Tescat.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<TescatDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDbContext<TescatDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TescatDbContext>();
builder.Services.AddRazorPages(options =>{
    //options.Conventions.AuthorizeAreaPage("Identity", "/Account/Register");
});
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();


//builder.Services.AddSingleton<WeatherForecastService>();

//Se a�ade como inyeccion de dependencias los servicios
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserCredentialService, UserCredentialService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPcService, PcService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IMemoryRamService, MemoryRamService>();
builder.Services.AddScoped<ICpuService, CpuService>();
builder.Services.AddScoped<IMotherboardService, MotherboardService>();
builder.Services.AddScoped<IPowerSupplyService, PowerSupplyService>();
builder.Services.AddScoped<IPcCredentialService, PcCredentialService>();
builder.Services.AddScoped<OtherServices>();
builder.Services.AddScoped<SaveTempID>();
builder.Services.AddSweetAlert2();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
