using KnowWeatherApp.API.Configurations;
using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Helpers;
using KnowWeatherApp.API.Interfaces;
using KnowWeatherApp.API.Repositories;
using KnowWeatherApp.API.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IWeatherReportRepository, UserWeatherReportRepository>();
builder.Services.AddScoped<IOpenWeatherRepository, OpenWeatherRepository>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

builder.Services.RegisterMapsterConfiguration();
builder.Services.AddHostedService<WeatherReportService>();

builder.Services.AddDbContext<KnowWeatherDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<KnowWeatherDbContext>();

TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
builder.Services.AddMapster();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.Configure<OpenWeatherSettings>(
    builder.Configuration.GetSection("OpenWeatherSettings"));

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "KnowWeather.API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

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

app.UseAuthentication();
app.UseAuthorization();

app.SetCurrentUser();

app.MapIdentityApi<AppUser>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Addresses}/{action=Index}");

app.Run();