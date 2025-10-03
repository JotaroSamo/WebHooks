using WebHook.Domain.IService.Ordes;
using WebHook.Domain.IService.WeebHooks;
using WebHook.Domain.Models.Orders;
using WebHook.Domain.Models.WebHooks;
using WebHooks.BusinessLogic.Service.WebHooks;

namespace WebHools.Endpoints.Orders;

public static class OrderEndPoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/order");
        
        group.MapPost("", Create);
        group.MapGet("", GetAll);
    }
    
    private static async Task<IResult> Create(CreateOrderRequest request, 
        IOrderRepository orderRepository, 
        IWebHookDispatcher dispatcher)
    {
        var order = new Order(Guid.NewGuid(), request.Name, DateTime.UtcNow);
    
        await orderRepository.CreateOrderAsync(order);
    
        await dispatcher.DispatchAsync("order.create", order);
    
        return Results.Ok(order);
    
    }

    private static async Task<IResult>  GetAll(IOrderRepository orderRepository) 
    {
        return Results.Ok(await orderRepository.GetAllOrdersAsync());
    }
}