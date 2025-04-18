﻿using GeminiAiBotApi.Handler.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GeminiAiBotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(IChatHistoryHandler chatHistoryHandler) : ControllerBase
    {

        [HttpGet("GetAllChats")]
        public async Task<IActionResult> GetAllChats(string userId)
        {
            var data = await Task.Run(() => chatHistoryHandler.GetAllChatByConnectionId(userId));
            return Ok(data);
        }
        [HttpPost("UpdateChatName")]
        public async Task<IActionResult> UpdateChatName(string userId, Guid chatId)
        {
            var data = await Task.Run(() => chatHistoryHandler.UpdateChatName(userId, chatId));
            return Ok(data);
        }

    }
}
