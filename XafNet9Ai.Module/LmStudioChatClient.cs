using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafNet9Ai.Module
{
    //public class LmStudioChatClient : IChatClient
    //{
    //    public ChatClientMetadata Metadata => throw new NotImplementedException();

    //    public Task<ChatCompletion> CompleteAsync(IList<ChatMessage> chatMessages, ChatOptions options = null, CancellationToken cancellationToken = default)
    //    {

    //        //var url = "http://localhost:1234/v1/completions"; // LM Studio server URL
    //        //var apiKey = "your-api-key"; // Replace with your LM Studio API key

    //        //var requestBody = new
    //        //{
    //        //    prompt = "Hello, how are you?",
    //        //    max_tokens = 50
    //        //};

    //        //var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
    //        //var content = new StringContent(json, Encoding.UTF8, "application/json");

    //        //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

    //        //var response = await client.PostAsync(url, content);
    //        //var responseString = await response.Content.ReadAsStringAsync();

    //        //Console.WriteLine(responseString);
    //        //throw new NotImplementedException();
    //    }

    //    public IAsyncEnumerable<StreamingChatCompletionUpdate> CompleteStreamingAsync(IList<ChatMessage> chatMessages, ChatOptions options = null, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public object GetService(Type serviceType, object serviceKey = null)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
