using KnowWeatherApp.WebUI;
using KnowWeatherApp.WebUI.Interfaces;
using KnowWeatherApp.WebUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<IKnowWeatherCitiesService, KnowWeatherCitiesService>();

builder.Services.AddHttpClient("KnowWeatherAppAPI", options =>
{
    options.BaseAddress = new Uri("http://localhost:5039");
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
