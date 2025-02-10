using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UserRegClient.Services;
using UserRegClient;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient("getUsers", client => client.BaseAddress = new Uri("http://localhost:5257/"));
builder.Services.AddScoped<GetUserService>();

builder.Services.AddHttpClient("addUser", client => client.BaseAddress = new Uri("http://localhost:5257/"));
builder.Services.AddScoped<PostUserService>();

builder.Services.AddHttpClient("deleteUser", client => client.BaseAddress = new Uri("http://localhost:5257/"));
builder.Services.AddScoped<DeleteUserService>();

builder.Services.AddHttpClient("putUser", client => client.BaseAddress = new Uri("http://localhost:5257/"));
builder.Services.AddScoped<PutUserService>();


await builder.Build().RunAsync();
