
using GeminiAiBotApi.Dtos;

namespace GeminiAiBotApi.Services.Interfaces
{
    public interface ICacheservice
    {
        void AddToCache(string key, object value, Guid ChatId);
        void Clear();
        List<ChatMessageModel>? GetAllChatByConnectionId(string key);
        List<object>? GetFromCache(string key, Guid chatId);
        bool UpdateChatNameCache(string key, Guid chatId, string name);
    }
}
