using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders
{
    public class GetOrderByNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
                => app.MapPost("/{number}", HandleAsync)
                      .WithName("Orders: Get by number")
                      .WithSummary("Buscar pedido pelo numero")
                      .WithDescription("Buscar pedido pelo numero")
                      .WithOrder(6)
                      .Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user, 
            IOrderHandler handler, 
            string number)
        {
            var request = new GetOrderByNumberRequest
            {
                Number = number,
                UserId = user.Identity!.Name ?? string.Empty
            };

            var result = await handler.GetByNumberAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
