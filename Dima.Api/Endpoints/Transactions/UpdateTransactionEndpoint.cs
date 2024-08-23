﻿using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
                  .WithName("Transactions: Update")
                  .WithSummary("Atualiza a movimentação")
                  .WithDescription("Atualiza a movimentação")
                  .WithOrder(2)
                  .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, UpdateTransactionRequest request, long id)
    {
        request.Id = id;
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.UpdateAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
