using GeminiAiBotApi.Dtos;
using GeminiAiBotApi.Services.Interfaces;

namespace GeminiAiBotApi.Services
{
    public class Cacheservice : ICacheservice
    {
        private readonly Dictionary<string, List<ChatMessageModel>> chatCache = [];

        public void AddToCache(string key, Object value, Guid ChatId)
        {
            if (chatCache.ContainsKey(key))
            {
                var data = chatCache[key].Find(x => x.ChatId == ChatId);
                if (data != null)
                {
                    data.Message.Add(value);
                }
                else
                {
                    chatCache[key].Add(new ChatMessageModel { ChatId = ChatId, Message = [value] });
                }
            }
            else
            {

                chatCache.Add(key, [new() { ChatId = ChatId, Message = [value] }]);
            }
        }


        public List<Object>? GetFromCache(string key, Guid chatId)
        {
            if (chatCache.TryGetValue(key, out List<ChatMessageModel>? value))
            {
                return value.Find(x => x.ChatId == chatId)?.Message;
            }
            return null;
        }

        public List<ChatMessageModel>? GetAllChatByConnectionId(string key)
        {
            if (chatCache.TryGetValue(key, out List<ChatMessageModel>? value))
            {
                return value;
            }
            return null;
        }

        public bool UpdateChatNameCache(string key, Guid chatId, string name)
        {
            if (chatCache.TryGetValue(key, out List<ChatMessageModel>? value))
            {
                var data = value.Find(x => x.ChatId == chatId);
                if (data != null)
                {
                    data.ChatName = name;
                    return true;
                }
            }
            return false;
        }



        public void Clear()
        {

            chatCache.Clear();
        }
    }
}
