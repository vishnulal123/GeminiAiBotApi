using GeminiAiBotApi.Dtos;
using GeminiAiBotApi.Handler.Interface;
using GeminiAiBotApi.Services.Interfaces;
using Newtonsoft.Json;
using static GeminiAiBotApi.Dtos.GeminiChatModel.GeminiChat;

namespace GeminiAiBotApi.Handler
{
    public class ChatHistoryHandler(ICacheservice cacheservice) : IChatHistoryHandler
    {
        public async Task<List<ChatModel>> GetAllChatByConnectionId(string connectionId)
        {
            var chats = await Task.Run(() => cacheservice.GetAllChatByConnectionId(connectionId)) ?? [];
            var chatsMessage = chats.Select(x => new ChatModel()
            {
                ChatName = x.ChatName,
                Messages = x.Message.Select(e => JsonConvert.DeserializeObject<ChatMessage>(JsonConvert.SerializeObject(e))).Select(y => new ChatHistoryModel()
                {
                    UserName = y.Role == "user" ? "User" : "AiBot",
                    Message = y.Parts.Select(z => z.Text).First(),
                }).ToList()
            }).ToList();
            return chatsMessage;
        }
    }
}
