using System.Configuration;
using Alpha.Core;
using Alpha.Infrastructure;
using Alpha.Infrastructure.Data;
using Alpha.Core.Interfaces;
using Alpha.Infrastructure.Services;
using Alpha.Web;
using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NuGet.Protocol;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication("CookieAuth")
//.AddCookie("CookieAuth", config => {
//  config.Cookie.Name = "Grandmas.Cookies";
//  config.LoginPath = "/Home/Authenticate";
// });
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    //.AddCookie();
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
    {
      config.Cookie.Name = "Grandmas.Cookies";
      config.LoginPath = "/Home/Index";
    });
//builder.Services.ConfigureApiAuthentication(builder.Configuration);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

//string connectionString = builder.Configuration.GetConnectionString("SqliteConnection");  //Configuration.GetConnectionString("DefaultConnection");
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");  //Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext(connectionString);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser , IdentityRole>()
  .AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.EnableAnnotations();
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
    config.Services = new List<ServiceDescriptor>(builder.Services);

    // optional - default path to view services is /listallservices - recommended to choose your own path
    config.Path = "/listservices";
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
//builder.Logging.AddAzureWebAppDiagnostics(); add this if deploying to Azure

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseShowAllServicesMiddleware();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        //                    context.Database.Migrate();
        context.Database.EnsureCreated();
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
