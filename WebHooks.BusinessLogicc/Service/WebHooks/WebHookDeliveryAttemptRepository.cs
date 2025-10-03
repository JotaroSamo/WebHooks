using WebHook.DataAccess;
using WebHook.Domain.IService.WeebHooks;
using WebHook.Domain.Models.WebHooks;

namespace WebHooks.BusinessLogic.Service.WebHooks;


public class WebHookDeliveryAttemptRepository : IWebHookDeliveryAttemptRepository
{
    private readonly WebHookDbContext _context;

    public WebHookDeliveryAttemptRepository(WebHookDbContext context)
    {
        _context = context;
    }
    
    public async Task<WebHookDeliveryAttempt> Create(WebHookDeliveryAttempt webHookDeliveryAttempt)
    {
        await _context.WebHookDeliveryAttemptsDeliveryAttempts.AddAsync(webHookDeliveryAttempt);
        
        await _context.SaveChangesAsync();
        
        return webHookDeliveryAttempt;
    }
}