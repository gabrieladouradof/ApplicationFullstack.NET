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

builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
builder.Services.AddTransient<IReportHandler, ReportHandler>();

builder.Services.AddMudServices();

builder.Services.AddHttpClient(Configuration.HttpClientName, opt =>
{
    opt.BaseAddress = new Uri("http://localhost:5252");

}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddLocalization();
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

await builder.Build().RunAsync();
