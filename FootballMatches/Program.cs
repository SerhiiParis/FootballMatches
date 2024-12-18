using FootballMatches.DataAccess;
using FootballMatches.DataAccess.Repositories;
using FootballMatches.Models.Contracts.DataAccess;
using FootballMatches.Models.Contracts.Services;
using FootballMatches.Models.Contracts.Services.DataApi;
using FootballMatches.Services;
using FootballMatches.Services.DataApi;
using FootballMatches.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

AddWebOptimizer(builder.Services);
AddDbContext(builder.Services);
AddServices(builder.Services);

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseWebOptimizer();
app.MapStaticAssets();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}")
    .WithStaticAssets();

app.Run();
return;


void AddServices(IServiceCollection services)
{
    AddApplicationConfig(services);
    AddFootballDataApiIntegration(services);
    services.AddScoped<IMatchService, MatchService>();
}

void AddApplicationConfig(IServiceCollection services)
{
    var config = new ApplicationConfig();
    builder.Configuration.GetSection("Application").Bind(config);
    services.AddSingleton<IOptions<ApplicationConfig>>(new OptionsWrapper<ApplicationConfig>(config));
}

void AddFootballDataApiIntegration(IServiceCollection services)
{
    var config = new DataApiConfig();
    builder.Configuration.GetSection("DataApi").Bind(config);
    services.AddSingleton<IOptions<DataApiConfig>>(new OptionsWrapper<DataApiConfig>(config));
    services.AddHttpClient<IDataApiClient, DataApiClient>();
    services.AddScoped<IDataApiService, DataApiService>();
}

void AddDbContext(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(o =>
    {
        o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    services.AddScoped<IMatchRepository, MatchRepository>();
}

void AddWebOptimizer(IServiceCollection services)
{
    services.AddWebOptimizer(pipeline =>
    {
        pipeline.AddCssBundle(
            "/css/styles.css",
            "css/global.css", "css/header.css");
        pipeline.AddJavaScriptBundle(
            "/js/scripts.js",
            "js/match-card.js", "js/match-carousel.js");
    });
}