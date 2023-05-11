using ChatGPTApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace api
{
    class ChatGptClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        private string _openAiToken;
        
        public ChatGptClient(string apiKey)
        {
            _openAiToken = apiKey;
            _httpClient = new HttpClient() 
            { DefaultRequestHeaders = { Authorization = AuthenticationHeaderValue.Parse($"Bearer {_openAiToken}") } };
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public async Task<string> GetChatGptMessage(string userMessage)
        {
            var chatGptSend = new GptSend
            {
                Model = "gpt-3.5-turbo",
                Messages = new Message[] { new Message() { Role = "user", Content = userMessage } }
            };

            var chatCompletionUri = "https://api.pawan.krd/v1/chat/completions";   
            
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(chatCompletionUri, chatGptSend, CancellationToken.None);
            ChatGPTResponse? chatGptResponse = await response.Content.ReadFromJsonAsync<ChatGPTResponse>();
            return chatGptResponse.Choices[0].Message.Content.ToString();
        }
    }
}
