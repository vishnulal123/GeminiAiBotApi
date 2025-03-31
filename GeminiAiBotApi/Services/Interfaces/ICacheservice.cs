
namespace GeminiAiBotApi.Services.Interfaces
{
    public interface ICacheservice
    {
        void AddToCache(string key, object value, Guid ChatId);
        void Clear();
        List<object>? GetFromCache(string key, Guid chatId);
    }
}
