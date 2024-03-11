using KnowWeatherApp.Common.Interfaces;
using KnowWeatherApp.Common.Repositories;
using KnowWeatherApp.Domain.Repositories;
using KnowWeatherApp.Persistence;
using KnowWeatherApp.Persistence.Repositories;
using KnowWeatherApp.Services.Configurations;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using KnowWeatherApp.Services.Helpers;
using KnowWeatherApp.Services.Abstractions;
using KnowWeatherApp.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((builder, services) =>
    {
        services.AddScoped<ICityRepository, CitytRepository>();
        services.AddScoped<IOpenWeatherService, OpenWeatherService>();
        services.AddScoped<IWeatherReportRepository, WeatherReportRepository>();
        services.AddScoped<ITriggerRepository, TriggerRepository>();
        services.AddScoped<IAzureQueueService, AzureQueueService>();
        services.AddScoped<ITriggerAnalyzer, TriggerAnalyzer>();

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.Configure<OpenWeatherSettings>(
            builder.Configuration.GetSection("OpenWeatherSettings"));

        services.AddDbContext<KnowWeatherDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));

        services.AddHttpClient("WeatherAPI", options =>
        {
            var weatherApi = builder.Configuration.GetValue<string>("OpenWeatherSettings:API");
            if (string.IsNullOrWhiteSpace(weatherApi)) throw new ArgumentException("Weather API was not provided");

            options.BaseAddress = new Uri(uriString: weatherApi);
        });

        services.RegisterMapsterConfiguration();
    })
    .Build();

host.Run();
