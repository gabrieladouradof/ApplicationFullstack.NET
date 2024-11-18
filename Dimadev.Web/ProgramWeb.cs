using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Dimadev.Web;
using System.Globalization;
using Dimadev.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Dimadev.Core.Handlers;
using Dimadev.Web.Handlers;
using Dima.Core.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x => (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped<IAccountHandler, AccountHandler>();
builder.Services.AddScoped<ICategoryHandler, CategoryHandler>();
builder.Services.AddScoped<ITransactionHandler, TransactionHandler>();

builder.Services.AddMudServices();

builder.Services.AddHttpClient(Configuration.HttpClientName, opt =>
{
    opt.BaseAddress = new Uri(Configuration.BackendUrl);

}).AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();
