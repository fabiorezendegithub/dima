namespace Dima.Core.Requests.Orders;

public class GetVoucherBynumberRequest: Request
{
    public string Number { get; set; } = string.Empty;
}
