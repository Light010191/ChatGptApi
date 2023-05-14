using api;
using ChatGPTApi;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;


var openAiKey =Environment.GetEnvironmentVariable("OpenAI_token");
string userMassage="" ;
Console.WriteLine("Введите Ваш Вопрос или нажмите 0 для выхода : ");
while (true)
{
    userMassage = Console.ReadLine();
    if (userMassage != "0" && !string.IsNullOrEmpty(userMassage))
    {
        ChatGptClient chatGptClient = new ChatGptClient(openAiKey);
        
        Console.WriteLine(await chatGptClient.GetChatGptMessage(userMassage));            
    }
    else
    {
        break;
    }    
}