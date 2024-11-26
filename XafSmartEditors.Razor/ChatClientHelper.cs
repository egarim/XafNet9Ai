using Microsoft.Extensions.AI;
using OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string modelId = "gpt-4o-mini";
            string OpenAiKey = Environment.GetEnvironmentVariable("OpenAiTestKey");

            //Adding semantic kernel
            var client = new OpenAIClient(new System.ClientModel.ApiKeyCredential(OpenAiKey));

            return new OpenAIChatClient(client, modelId)
                .AsBuilder()
                .UseFunctionInvocation()
                //.UserLanguage("spanish")
                //.UseRateLimitThreading(TimeSpan.FromSeconds(5))
                .Build();
        }
    }
}
