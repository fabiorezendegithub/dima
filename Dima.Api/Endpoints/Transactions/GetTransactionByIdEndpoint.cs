﻿using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                  .WithName("Transactions: Get by Id")
                  .WithSummary("Buscar movimentação por id")
                  .WithDescription("Buscar movimentação por id")
                  .WithOrder(4)
                  .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, long id)
    {
        var request = new GetTransactionByIdRequest
        {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };

        var result = await handler.GetByIdAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}