using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UserRegClient.Services;
using UserRegClient;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddRadzenComponents();



builder.Services.AddHttpClient("UserRegClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddHttpClient("getUsers", client => client.BaseAddress = new Uri("http://localhost:5257/"));
builder.Services.AddScoped<GetUserService>();

builder.Services.AddHttpClient("searchRank", client => client.BaseAddress = new Uri("http://localhost:5257/"));
builder.Services.AddScoped<GetRankToIDService>();
await builder.Build().RunAsync();