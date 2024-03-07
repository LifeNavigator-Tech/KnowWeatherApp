using KnowWeatherApp.API.Configurations;
using KnowWeatherApp.API.Helpers;
using KnowWeatherApp.API.Interfaces;
using KnowWeatherApp.API.Repositories;
using KnowWeatherApp.API.Services;
using Mapster;
using WeatherPass.FunctionApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICityCosmoRepository, CityCosmoRepository>();
builder.Services.AddScoped<IUserWeatherReportRepository, UserWeatherReportRepository>();
builder.Services.AddScoped<IOpenWeatherRepository, OpenWeatherRepository>();
builder.Services.RegisterMapsterConfiguration();
builder.Services.AddHostedService<WeatherReportService>();

TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
builder.Services.AddMapster();

builder.Services.AddControllers();

builder.Services.Configure<MongoDatabaseSettings>(
    builder.Configuration.GetSection("MongoDatabaseSettings"));

builder.Services.Configure<OpenWeatherSettings>(
    builder.Configuration.GetSection("OpenWeatherSettings"));

builder.Services.AddHttpClient("WeatherAPI", options =>
{
    var openWeatherConfig = builder.Configuration.GetSection("OpenWeatherSettings").Get<OpenWeatherSettings>();
    if (openWeatherConfig == null) throw new ArgumentException("Weather API was not provided");

    options.BaseAddress = new Uri(uriString: openWeatherConfig.API);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Addresses}/{action=Index}");

app.Run();