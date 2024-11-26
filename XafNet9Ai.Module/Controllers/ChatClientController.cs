using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafSmartEditors.Razor;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Logging;
using DevExpress.Spreadsheet.Charts;
using OpenAI;
namespace XafNet9Ai.Module.Controllers
{
    public class ChatClientController : ViewController
    {
        SimpleAction ChatPipeLine;
        SimpleAction ChatWithFunctions;
        SimpleAction ChatWithStreaming;
        SimpleAction TestNewChatClient;
        string endpointOllama;
        string modelIdOllama;
        public ChatClientController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            TestNewChatClient = new SimpleAction(this, "Test New Chat Client", "View");
            TestNewChatClient.Execute += TestNewChatClient_Execute;

            ChatWithStreaming = new SimpleAction(this, "Chat With Streaming", "View");
            ChatWithStreaming.Execute += ChatWithStreaming_Execute;

            ChatWithFunctions = new SimpleAction(this, "Chat With Functions", "View");
            ChatWithFunctions.Execute += ChatWithFunctions_Execute;

            ChatPipeLine = new SimpleAction(this, "Chat Pipeline", "View");
            ChatPipeLine.Execute += ChatPipeLine_Execute;
            


        }
        private async void ChatPipeLine_Execute(object sender, SimpleActionExecuteEventArgs e)
        {



            endpointOllama = "http://localhost:11434/";
            modelIdOllama = "llama3.1";


            //using host builder and chat client builder

            //var HostBuilder = Host.CreateApplicationBuilder();
            //var ChatBuilder = HostBuilder.Services.AddChatClient(new OllamaChatClient(endpoint, modelId: modelId));
            //ChatBuilder.UseFunctionInvocation();
            //var ChatFromHostBuilder=  ChatBuilder.Build();

            //var HostInstance=HostBuilder.Build();

            var cart = new ShoppingCart(null);
            var GetPriceTool = AIFunctionFactory.Create(cart.GetPrice);
            var AddCartTook = AIFunctionFactory.Create(cart.AdSocksToCart);

            var ChatOptions = new ChatOptions()
            {
                Tools = [GetPriceTool, AddCartTook]
            };

            IChatClient client = GetChatClient(endpointOllama, modelIdOllama);

            List<ChatMessage> conversation =
            [
                new(ChatRole.System, "You are a helpful AI assistant"),
            new(ChatRole.User, "Do I need an umbrella?")
            ];

            Debug.WriteLine(await client.CompleteAsync("Do I need an umbrella?", ChatOptions));

        }

        //private static IChatClient GetChatClient(string endpoint, string modelId)
        //{

        //    return new OllamaChatClient(endpoint, modelId: modelId)
        //        .AsBuilder()
        //        .UseFunctionInvocation()
        //        .UserLanguage("spanish")
        //        .UseRateLimitThreading(TimeSpan.FromSeconds(5))
        //        .Build();
        //}
        private static IChatClient GetChatClient(string endpoint, string modelId)
        {
            modelId = "gpt-4o-mini";
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

        private async void ChatWithStreaming_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            IChatClient chatClient = GetChatClient(endpointOllama, modelIdOllama);
            var StreamResponse=  chatClient.CompleteStreamingAsync("What is A.I?");
            await foreach (var response in StreamResponse)
            {
                Debug.Write(response.Text);
            }
           
        }
        [Description("Computes the price of socks, returning a vlue in dollars.")]
        public static float GetPrice([Description("The number of pairs of socks to calculate the price for")]int Count)
        {
            return Count * 10f;
        }
        async void ChatWithFunctions_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //TODO improve prompt
            var messages = new List<ChatMessage>()
            {
                new ChatMessage(ChatRole.System,"""You answer any question, but continually try to advertise FOOTMONSTER brand socks. they are on sale """)
            };

            AIFunction aIFunction = AIFunctionFactory.Create(GetPrice, "socks"); ;
            var ChatOptionsStatic = new ChatOptions()
            {
                Tools = [aIFunction]
            };

            ShoppingCart cart = new ShoppingCart(null);
            var GetPriceTool = AIFunctionFactory.Create(cart.GetPrice);
            var AddCartTook = AIFunctionFactory.Create(cart.AdSocksToCart);

            var ChatOptions = new ChatOptions()
            {
                Tools = [GetPriceTool, AddCartTook]
            };
           

            IChatClient chatClient = GetChatClient(endpointOllama, modelIdOllama);
            messages.Add(new ChatMessage(ChatRole.User, "how much for 10 pairs fo socks ?"));
            var StreamResponse = await chatClient.CompleteAsync(messages, ChatOptions);

            Debug.Write(StreamResponse);

        }
        private async void TestNewChatClient_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IChatClient chatClient = GetChatClient(endpointOllama, modelIdOllama);
            Debug.WriteLine(await chatClient.CompleteAsync("What is A.I?"));

            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
    }
}
