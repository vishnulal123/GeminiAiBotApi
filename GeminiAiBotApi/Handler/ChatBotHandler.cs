using GeminiAiBotApi.Constants;
using GeminiAiBotApi.Handler.Interface;
using GeminiAiBotApi.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using static GeminiAiBotApi.Dtos.GeminiChatModel.GeminiChat;

namespace GeminiAiBotApi.Handler
{
    public class ChatBotHandler(ICacheservice cacheservice) : IChatBotHandler
    {
        public async Task<string> GeminiAiBot(string query, string connectionId, Guid chatId, bool isTempChat = false)
        {
            string apiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY", EnvironmentVariableTarget.Machine) ?? ""; // add your gemini  api key
            string baseUrl = GeminiApiConstants.GeminiApiBaseUrl;
            bool isFirstChat = false;
            var queryParams = new Dictionary<string, string?>()
            {
               {"key",apiKey}
            };
            string uri = QueryHelpers.AddQueryString(baseUrl, queryParams);
            var clent = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string input = "Answer in less that 100 words " + query;
            var datas = new List<Object>();

            if (isTempChat)
            {
                AddDataToList(datas, input, "user");
            }
            else
            {
                datas = cacheservice.GetFromCache(connectionId, chatId);
                if (datas == null || datas.Count == 0) // First Chat
                {
                    isFirstChat = true;
                }
                AddDataToCache(connectionId, input, "user", chatId);
                datas = cacheservice.GetFromCache(connectionId, chatId);
            }
            if (isFirstChat)
            {
                cacheservice.UpdateChatNameCache(connectionId, chatId, query);
            }

            var request = new
            {
                Contents = datas,
                GenerationConfig = new
                {
                    GeminiApiConstants.Temperature,
                    GeminiApiConstants.TopK,
                    GeminiApiConstants.TopP,
                    GeminiApiConstants.MaxOutputTokens,
                    GeminiApiConstants.ResponseMimeType,
                }
            };

            string jsonData = System.Text.Json.JsonSerializer.Serialize(request, options: options);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                // Send the POST request
                HttpResponseMessage response = await clent.PostAsync(uri, content);
                // Ensure success status code
                response.EnsureSuccessStatusCode();
                // Read the response content

                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(responseContent) ?? new();
                string output = json.candidates[0].content.parts[0].text;
                string outputData = output.Replace("{", "").Replace("}", "");
                if (!isTempChat)
                {
                    AddDataToCache(connectionId, outputData, "model", chatId);
                }
                return outputData;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            return string.Empty;
        }

        private static void AddDataToList(List<Object> data, string message, string role)
        {

            data.Add
                (new ChatMessage
                {
                    Role = role,
                    Parts = new List<Part>()
                    {
                        new Part
                        {
                            Text = message,
                        }
                    }
                });
        }

        private void AddDataToCache(string connectionId, string message, string role, Guid chatId)
        {
            cacheservice.AddToCache
            (connectionId, new ChatMessage
            {
                Role = role,
                Parts = new List<Part>()
                    {
                        new Part
                        {
                            Text = message,
                        }
                    }
            }, chatId);
        }
    }









}
