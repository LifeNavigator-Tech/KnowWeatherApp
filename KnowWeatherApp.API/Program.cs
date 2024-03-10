using KnowWeatherApp.API.Helpers;
using KnowWeatherApp.Common.Interfaces;
using KnowWeatherApp.Common.Repositories;
using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Repositories;
using KnowWeatherApp.Persistence;
using KnowWeatherApp.Persistence.Repositories;
using KnowWeatherApp.Services.Configurations;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using KnowWeatherApp.Services.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICityRepository, CitytRepository>();
builder.Services.AddScoped<IOpenWeatherService, OpenWeatherService>();
builder.Services.AddScoped<ICurrentUserHelper, CurrentUserHelper>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<KnowWeatherDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<KnowWeatherDbContext>();

TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
builder.Services.AddMapster();
builder.Services.RegisterMapsterConfiguration();

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

await ApplyMigrations(app.Services);

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

app.MapIdentityApi<AppUser>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Addresses}/{action=Index}");

app.Run();


static async Task ApplyMigrations(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    await using KnowWeatherDbContext dbContext = scope.ServiceProvider.GetRequiredService<KnowWeatherDbContext>();
    await dbContext.Database.MigrateAsync();
}