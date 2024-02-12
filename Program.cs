using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using System.Net.Http.Headers;
using DebtTracker.Services;
using DebtTracker.Services.Contracts;
using DebtTracker;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient("Debt.Processor", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Endpoints:Debt.Processor"]);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<IFileProcessor, FileProcessor>();

builder.Services.AddRadzenComponents();




await builder.Build().RunAsync();
