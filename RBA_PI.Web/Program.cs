using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RBA_PI.Application.Interfaces;
using RBA_PI.Application.Interfaces.Repositories;
using RBA_PI.Application.Services.Implementations;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Data.ConnectionStrings;
using RBA_PI.Infrastructure.Email.Smtp;
using RBA_PI.Infrastructure.Identity;
using RBA_PI.Infrastructure.Identity.Constants;
using RBA_PI.Infrastructure.Identity.Entities;
using RBA_PI.Infrastructure.Identity.Errors;
using RBA_PI.Infrastructure.Persistence;
using RBA_PI.Infrastructure.Repositories.Implementations;
using RBA_PI.Infrastructure.Services;
using RBA_PI.Infrastructure.Services.Excel;
using RBA_PI.Web.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("AppConnection") ?? throw new InvalidOperationException("Connection string 'AppConnection' not found.");
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
builder.Services.AddScoped<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, IdentityEmailSenderAdapter>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IConfiguracionRepository, ConfiguracionRepository>();
builder.Services.AddScoped<IEstadosComprobantesRepository, EstadosComprobantesRepository>();
builder.Services.AddScoped<IComprobanteArcaRepository, ComprobanteArcaRepository>();
builder.Services.AddScoped<IAnalisisDatosRepository, AnalisisDatosRepository>();
builder.Services.AddScoped<IFechaCierreRepository, FechaCierreRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IConceptoRepository, ConceptoRepository>();
builder.Services.AddScoped<IAuxiliarRepository, AuxiliarRepository>();
builder.Services.AddScoped<IDepositoRepository, DepositoRepository>();
builder.Services.AddScoped<IImputarARepository, ImputarARepository>();
builder.Services.AddScoped<IArticulosRemitoRepository, ArticulosRemitoRepository>();
builder.Services.AddScoped<IComprobanteVerificacionRepository, ComprobanteVerificacionRepository>();
builder.Services.AddScoped<IComprobanteProcesadorRepository, ComprobanteProcesadorRepository>();
builder.Services.AddScoped<IComprobantesRepository, ComprobantesRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IConnectionStringFactory, ConnectionStringFactory>();
builder.Services.AddScoped<IClienteContext, ClienteContext>();
builder.Services.AddScoped<IFacturasConceptosService, FacturasConceptosService>();
builder.Services.AddScoped<IFacturasRemitosService, FacturasRemitosService>();
builder.Services.AddScoped<IEstadosComprobantesService, EstadosComprobantesService>();
builder.Services.AddScoped<IComprobanteArcaService, ComprobanteArcaService>();
builder.Services.AddScoped<IExcelFileService, ExcelFileService>();
builder.Services.AddScoped<IExcelParserService, ExcelParserService>();
builder.Services.AddScoped<IAnalisisDatosService, AnalisisDatosService>();
builder.Services.AddScoped<IExcelExportService, ExcelExportService>();
builder.Services.AddScoped<ILookupsService, LookupsService>();
builder.Services.AddScoped<IProveedorDefaultsService, ProveedorDefaultsService>();
builder.Services.AddScoped<IComprobanteVerificationService, ComprobanteVerificationService>();
builder.Services.AddScoped<IComprobantesQueryService, ComprobantesQueryService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders()
.AddErrorDescriber<MyErrorDescriber>();

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    ILoggerFactory loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        AppIdentityDbContext context = services.GetRequiredService<AppIdentityDbContext>();
        RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DefaultRoles.SeedRolesAsync(roleManager);
    }
    catch (Exception ex)
    {
        ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
