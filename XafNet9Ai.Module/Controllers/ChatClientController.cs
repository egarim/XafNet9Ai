using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafNet9Ai.Module.Controllers
{
    public class ChatClientController : ViewController
    {
        SimpleAction TestNewChatClient;
        public ChatClientController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            TestNewChatClient = new SimpleAction(this, "Test New Chat Client", "View");
            TestNewChatClient.Execute += TestNewChatClient_Execute;
            
        }
        private async void TestNewChatClient_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IChatClient chatClient = new OllamaChatClient(new Uri("http://127.0.0.1:11434"), modelId: "phi3:latest");
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
