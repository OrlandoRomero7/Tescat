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
using Radzen;
using System.Net.NetworkInformation;
//using Tescat.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContextFactory<TescatDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContextFactory<TescatDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });
});

//builder.Services.AddDbContextFactory<TescatDbContext>(options =>
//{
//    var connectionString = Environment.GetEnvironmentVariable("TescatConnection");
//    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
//    {
//        sqlOptions.EnableRetryOnFailure();
//    });
//});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TescatDbContext>();
builder.Services.AddRazorPages(options =>{
    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Register");
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(14);
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
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<PictureStateContainer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500; // Internal Server Error
            context.Response.ContentType = "text/plain";

            var errorFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
            if (errorFeature != null)
            {
                var errorMessage = $"Error: {errorFeature.Error.Message}\n{errorFeature.Error.StackTrace}";
                await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
            }
        });
        app.UseHsts();
    });
}

//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler(errorApp =>
//    {
//        errorApp.Run(async context =>
//        {
//            context.Response.StatusCode = 500; // Internal Server Error
//            context.Response.ContentType = "text/plain";

//            var errorFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
//            if (errorFeature != null)
//            {
//                var errorMessage = $"Error: {errorFeature.Error.Message}\n{errorFeature.Error.StackTrace}";
//                await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
//            }
//        });
//        app.UseHsts();
//    });
//}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
