using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafSmartEditors.Razor.Middleware;

namespace XafSmartEditors.Razor
{
    public static class ChatClientHelper
    {
        //private static IChatClient GetChatClient(string endpoint, string modelId)
        //{

        //    return new OllamaChatClient(endpoint, modelId: modelId)
        //        .AsBuilder()
        //        .UseFunctionInvocation()
        //        .UserLanguage("spanish")
        //        .UseRateLimitThreading(TimeSpan.FromSeconds(5))
        //        .Build();
        //}
        public static IChatClient GetChatClient()
        {
            //GPT 4 min does not do a good count
            //string modelId = "gpt-4o-mini"; 
            string modelId = "gpt-4o";
            string OpenAiKey = Environment.GetEnvironmentVariable("OpenAiTestKey");

        
            var client = new OpenAIClient(new System.ClientModel.ApiKeyCredential(OpenAiKey));

            return new OpenAIChatClient(client, modelId)
                .AsBuilder()
                .UseFunctionInvocation()
                .UserLanguage("spanish")
                //.UseRateLimitThreading(TimeSpan.FromSeconds(5))
                .Build();
        }
    }
}
