using GeminiAiBotApi.Dtos;
using GeminiAiBotApi.Handler.Interface;
using GeminiAiBotApi.Services.Interfaces;
using Newtonsoft.Json;
using static GeminiAiBotApi.Dtos.GeminiChatModel.GeminiChat;

namespace GeminiAiBotApi.Handler
{
    public class ChatHistoryHandler(ICacheservice cacheservice, IChatBotHandler chatBotHandler) : IChatHistoryHandler
    {
        public async Task<List<ChatModel>> GetAllChatByConnectionId(string connectionId)
        {
            var chats = await Task.Run(() => cacheservice.GetAllChatByConnectionId(connectionId)) ?? [];
            var chatsMessage = chats.Select(x => new ChatModel()
            {
                ChatId = x.ChatId,
                ChatName = x.ChatName,
                Messages = x.Message.Select(e => JsonConvert.DeserializeObject<ChatMessage>(JsonConvert.SerializeObject(e))).Select(y => new ChatHistoryModel()
                {
                    UserName = y.Role == "user" ? connectionId : "AiBot",
                    Message = y.Parts.Select(z => z.Text).First().Replace("Answer in less that 100 words ", ""),
                }).ToList()
            }).ToList();
            return chatsMessage;
        }

        public async Task<bool> UpdateChatName(string connectionId, Guid chatId)
        {
            var chatName = await chatBotHandler.GeminiAiBot("Give a Title To This whole Chat in less than 5 words", connectionId, chatId, isUpdateName: true);
            var result = await Task.Run(() => cacheservice.UpdateChatNameCache(connectionId, chatId, chatName));
            return result;
        }


    }
}
