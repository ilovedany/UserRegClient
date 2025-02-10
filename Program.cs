using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UserRegClient.Services;
using UserRegClient;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped<ThemeService>();

builder.Services.AddHttpClient("TestApp", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));


builder.Services.AddScoped<GetUserService>();
builder.Services.AddScoped<PostUserService>();
builder.Services.AddScoped<DeleteUserService>();
builder.Services.AddScoped<PutUserService>();

await builder.Build().RunAsync();