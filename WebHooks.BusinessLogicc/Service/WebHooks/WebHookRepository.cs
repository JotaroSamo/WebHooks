using Microsoft.EntityFrameworkCore;
using WebHook.DataAccess;
using WebHook.Domain.IService.WeebHooks;
using WebHook.Domain.Models.WebHooks;

namespace WebHooks.BusinessLogic.Service.WebHooks;


public class WebHookRepository : IWebHookRepository
{
    private readonly WebHookDbContext _context;

    public WebHookRepository(WebHookDbContext context)
    {
        _context = context;
    }
    
    public async Task<WebhookSub> CreateWebhookSubAsync(WebhookSub webhookSub)
    {
        
        await _context.WebhookSubs.AddAsync(webhookSub);
        
        await _context.SaveChangesAsync();
        
        return await Task.FromResult(webhookSub);
    }

    public async Task<IReadOnlyList<WebhookSub>> GetByEventTypeWebhookSubsAsync(string eventType)
    {
        return await _context.WebhookSubs.Where(x=>x.EventType == eventType).AsNoTracking().ToListAsync();
    }
}