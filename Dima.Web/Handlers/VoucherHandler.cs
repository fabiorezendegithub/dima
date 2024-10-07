using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;

namespace Dima.Web.Handlers;

public class VoucherHandler : IVoucherHandler
{
    public Task<Response<Voucher?>> GetByNumberAsync(GetVoucherBynumberRequest request)
    {
        throw new NotImplementedException();
    }
}
