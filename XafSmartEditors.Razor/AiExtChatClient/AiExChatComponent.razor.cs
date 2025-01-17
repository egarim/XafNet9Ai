using DevExpress.AIIntegration.Blazor.Chat;
using DevExpress.AIIntegration.Services.Chat;
using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Memory;
using System.Diagnostics;

namespace XafSmartEditors.Razor.AiExtChatClient
{
    public partial class AiExChatComponent
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        IChatHistory _value;
        IChatClient? client = ChatClientHelper.GetChatClient();
        [Parameter]
        public IChatHistory Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        async Task MessageSent(MessageSentEventArgs args)
        {
            Value.Messages.Add(new Microsoft.Extensions.AI.ChatMessage(ChatRole.User, args.Content));
            ChatCompletion result = await client.CompleteAsync(Value.Messages);

            Value.Messages.AddRange(result.Message);

            //TODO fix after update

            //var message = new Message(MessageRole.Assistant, (result.Message.Text));
            //args.SendMessage(message);
          
        }

        MarkupString ToHtml(string text)
        {
            return (MarkupString)Markdown.ToHtml(text);
        }

    }
}