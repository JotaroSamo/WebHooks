namespace WebHook.Domain.IService.WeebHooks;

public interface IWebHookDispatcher
{
    Task DispatchAsync<T>(string eventType, T data);
}
