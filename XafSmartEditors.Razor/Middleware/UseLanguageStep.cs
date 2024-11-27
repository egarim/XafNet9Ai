using Microsoft.Extensions.AI;
using System;
using System.Linq;
using System.Threading.RateLimiting;

namespace XafSmartEditors.Razor.Middleware
{
    public static class UseLanguageStep
    {
        public static ChatClientBuilder UserLanguage(this ChatClientBuilder chatClientBuilder, string language)
        {
            chatClientBuilder.Use(inner => new UseLanguageClient(inner, language));
            return chatClientBuilder;
        }
        private class UseLanguageClient(IChatClient chatClient, string language) : DelegatingChatClient(chatClient)
        {
            public override async Task<ChatCompletion> CompleteAsync(IList<ChatMessage> chatMessages, ChatOptions options = null, CancellationToken cancellationToken = default)
            {
                //HACK Chat augmentation
     
                ChatMessage promptAugmentation = new ChatMessage(ChatRole.System, $"User language is {language}");
                chatMessages.Add(promptAugmentation);
                try
                {
                    return await base.CompleteAsync(chatMessages, options, cancellationToken);
                }
                finally
                {
                    //cleanup
                    chatMessages.Remove(promptAugmentation);
                }


            }
        }
    }
}
