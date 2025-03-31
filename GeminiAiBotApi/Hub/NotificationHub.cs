namespace GeminiAiBotApi.Hub
{
    using GeminiAiBotApi.Handler.Interface;
    using Microsoft.AspNetCore.SignalR;
    public class NotificationHub(IChatBotHandler chatBotHandler) : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            var connectrionId = Context.ConnectionId;
            var chatId = new Guid("c0dd342c-bc93-4145-8ea9-16ae739a06e2");
            var result = await chatBotHandler.GeminiAiBot(message, connectrionId, chatId);
            await Clients.Caller.SendAsync("ReceiveMessage", user, result);
        }
    }
}
