using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dimadev.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Dimadev.Core.Responses;
using Dima.Core.Handlers;
using Dima.Api.Handlers;
using Dimadev.Api.Endpoints;
using Microsoft.AspNetCore.Identity;
using Dimadev.Api.Models;
using Dimadev.Api.Common.Api;
using Dimadev.Api;
using Dimadev.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;

builder.AddConfiguration();
builder.AddDocumentation();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddServices();
builder.AddCrossOrigin();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.ConfigureDevEnvironmet();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();

app.Run();