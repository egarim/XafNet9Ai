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

namespace XafNet9Ai.Module.Controllers
{
    public class Cart
    {
        public object NumPairOfSocks { get; set; }

        public void AdSocksToCart(int NumOfPairs)
        {
            
        }
        [Description("Computes the price of socks, returning a value in dollars.")]
        public float GetPrice([Description("The number of pairs of socks to calculate the price for")] int Count)
        {
            return Count * 10f;
        }
        public Cart()
        {
            
        }
    }
    public class ChatClientController : ViewController
    {
        SimpleAction ChatWithFunctions;
        SimpleAction ChatWithStreaming;
        SimpleAction TestNewChatClient;
        public ChatClientController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            TestNewChatClient = new SimpleAction(this, "Test New Chat Client", "View");
            TestNewChatClient.Execute += TestNewChatClient_Execute;

            ChatWithStreaming = new SimpleAction(this, "Chat With Streaming", "View");
            ChatWithStreaming.Execute += ChatWithStreaming_Execute;

            ChatWithFunctions = new SimpleAction(this, "Chat With Functions", "View");
            ChatWithFunctions.Execute += ChatWithFunctions_Execute;
            


        }
        private void action_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
       
        private async void ChatWithStreaming_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            IChatClient chatClient = new OllamaChatClient(new Uri("http://127.0.0.1:11434"), modelId: "phi3:mini");
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

            Cart cart = new Cart();
            var GetPriceTool = AIFunctionFactory.Create(cart.GetPrice);
            var AddCartTook = AIFunctionFactory.Create(cart.AdSocksToCart);

            var ChatOptions = new ChatOptions()
            {
                Tools = [GetPriceTool, AddCartTook]
            };
            //var ChatOptions = new ChatOptions()
            //{
            //    Tools = [aIFunction]
            //};

            IChatClient chatClient = new OllamaChatClient(new Uri("http://127.0.0.1:11434"), modelId: "phi3:mini");
            messages.Add(new ChatMessage(ChatRole.User, "how much for 10 pairs fo socks ?"));
            var StreamResponse = await chatClient.CompleteAsync(messages, ChatOptions);

            Debug.Write(StreamResponse);

        }
        private async void TestNewChatClient_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IChatClient chatClient = new OllamaChatClient(new Uri("http://127.0.0.1:11434"), modelId: "phi3:mini");
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
