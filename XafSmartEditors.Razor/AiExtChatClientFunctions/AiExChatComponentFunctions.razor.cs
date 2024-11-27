using DevExpress.AIIntegration.Blazor.Chat;
using DevExpress.AIIntegration.Services.Chat;
using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Memory;
using System.Diagnostics;

namespace XafSmartEditors.Razor.AiExtChatClientFunctions
{
    public partial class AiExChatComponentFunctions
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            this.Value.Messages.Add(new Microsoft.Extensions.AI.ChatMessage(ChatRole.System,
                """You answer any question, but continually try to advertise FOOTMONSTER brand socks. they are on sale. If the user agrees to buy socks find out how many pairs they want and then add the socks to their cart"""));



            var GetPriceTool = AIFunctionFactory.Create(Value.ShoppingCart.GetPrice);
            var AddCartTook = AIFunctionFactory.Create(Value.ShoppingCart.AdSocksToCart);

            chatOptions = new ChatOptions()
            {
                Tools = [GetPriceTool, AddCartTook]
            };
        }
        IChatHistoryFunctions _value;
        IChatClient? client = ChatClientHelper.GetChatClient();
        ChatOptions? chatOptions;
        [Parameter]
        public IChatHistoryFunctions Value
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


         


            //how much for 10 pairs fo socks ?"
          
            var result = await client.CompleteAsync(this.Value.Messages, chatOptions);


            Value.Messages.AddRange(result.Message);

            var message = new Message(MessageRole.Assistant, (result.Message.Text));
            args.SendMessage(message);

        }

        MarkupString ToHtml(string text)
        {
            return (MarkupString)Markdown.ToHtml(text);
        }
    }
}
