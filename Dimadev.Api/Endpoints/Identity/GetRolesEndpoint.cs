using Dimadev.Api.Common.Api;
using Dimadev.Core.Models.Account;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Cms;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Identity
{
    public class GetRolesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app
            .MapGet("/roles", Handle)
            .RequireAuthorization();

        private static Task<IResult> Handle(ClaimsPrincipal user)
        {
            if (user.Identity is null || !user.Identity.IsAuthenticated) 
            {
                return Task.FromResult(Results.Unauthorized());

            }

            var identity = (ClaimsIdentity)user.Identity;
            var roles = identity
                .FindAll(identity.RoleClaimType)
                .Select(c => new RoleClaim
                {
                    Issuer = c.Issuer,
                    OriginalIssuer = c.OriginalIssuer,
                    Type = c.Type,
                    Value = c.Value,
                    ValueType = c.ValueType
                }
                );

            return Task.FromResult<IResult>(TypedResults.Json(roles));
        }
    }
}
