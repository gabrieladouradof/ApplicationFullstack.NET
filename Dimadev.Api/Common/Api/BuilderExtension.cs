﻿using Dimadev.Api.Data;
using Microsoft.EntityFrameworkCore;
using Dimadev.Core;
using Microsoft.AspNetCore.Identity;
using Dima.Api.Handlers;
using Dima.Core.Handlers;

namespace Dimadev.Api.Common.Api
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder) 
        {
            //Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x  =>
            { x.CustomSchemaIds(n => n.FullName);
            });
        }

        public static void AddSecurity (this WebApplicationBuilder builder) 
        {
            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();
            builder.Services.AddAuthorization();
        }
        public static void AddDataContexts (this WebApplicationBuilder builder) 
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
            new MySqlServerVersion(new Version(8, 4, 0))));

        }

        public static void AddCrossOrigin (this WebApplicationBuilder builder) 
        {
            //CORS
            builder.Services.AddCors(
               options => options.AddPolicy(
                   ApiConfiguration.CorsPolicyName,
                   policy => policy.WithOrigins([
                       Configuration.BackendUrl,
                       Configuration.FrontendUrl
                       ])
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
                   ));
        }
        public static void AddServices (this WebApplicationBuilder builder) 
        {
            builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
            builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

        } 

    }
}
