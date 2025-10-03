using WebHook.Domain.Models.Orders;

namespace WebHook.Domain.IService.Ordes;

public interface IOrderRepository
{
    public Task<Order> CreateOrderAsync(Order order);
    public Task<IEnumerable<Order>> GetAllOrdersAsync();
}