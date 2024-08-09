﻿using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Identity;

public class GetRolesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/roles", Handle)
              .RequireAuthorization();

    private static IResult Handle(ClaimsPrincipal user)
    {
        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Results.Unauthorized();

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType)
                            .Select(c => new RoleClaim
                            {
                                Issuer = c.Issuer,
                                OriginalIssuer = c.OriginalIssuer,
                                Type = c.Type,
                                Value =c.Value,
                                ValueType = c.ValueType
                            });

        return TypedResults.Json(roles);
    }
}

/*
 app.MapGroup("v1/identity")
           .WithTags("Identity")
           .MapPost("/roles", (
               ClaimsPrincipal user) =>
           {
               if (user.Identity is null || !user.Identity.IsAuthenticated)
                   return Results.Unauthorized();

               var identity = (ClaimsIdentity)user.Identity;
               var roles = identity.FindAll(identity.RoleClaimType)
                                   .Select(c => new
                                   {
                                       c.Issuer,
                                       c.OriginalIssuer,
                                       c.Type,
                                       c.Value,
                                       c.ValueType
                                   });

               return TypedResults.Json(roles);
           })
           .RequireAuthorization();
 */
