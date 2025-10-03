using Microsoft.EntityFrameworkCore;
using WebHook.DataAccess;
using WebHook.Domain.IService.Ordes;
using WebHook.Domain.Models.Orders;

namespace WebHooks.BusinessLogic.Service.Orders;



public class OrderRepository : IOrderRepository
{
    private readonly WebHookDbContext _context;

    public OrderRepository(WebHookDbContext context)
    {
        _context = context;
    }
    
    public async Task<Order> CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        
        await _context.SaveChangesAsync();
        
        return order;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.AsNoTracking().ToListAsync();
    }
}
