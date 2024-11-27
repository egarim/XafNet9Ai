using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafNet9Ai.Module.BusinessObjects;
using XafSmartEditors.Razor.AiExtChatClient;
using XafSmartEditors.Razor.AiExtChatClientFunctions;
using XafSmartEditors.Razor.RagChat;

namespace XafNet9Ai.Module.Controllers
{
    public class ChatUiController : ViewController
    {
        SimpleAction ChatClientFunctionsUi;
        SimpleAction ChatClient;
        public ChatUiController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            ChatClient = new SimpleAction(this, "Chat client with U.I", "View");
            ChatClient.Execute += ChatClient_Execute;

            ChatClientFunctionsUi = new SimpleAction(this, "Chat Client Functions U.I", "View");
            ChatClientFunctionsUi.Execute += ChatClientFunctionsUi_Execute;

            this.TargetObjectType = typeof(ApplicationUser);
        }
        private void ChatClientFunctionsUi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            var os = this.Application.CreateObjectSpace(typeof(AiExtChatViewFunctions));
            var ChatView = os.CreateObject<AiExtChatViewFunctions>();
            ChatView.ChatHistory = os.CreateObject<IChatHistoryFunctionsImp>();
            ChatView.ChatHistory.ShoppingCart = new XafSmartEditors.Razor.ShoppingCart(ChatView);

               DetailView detailView = this.Application.CreateDetailView(os, ChatView);

            e.ShowViewParameters.CreatedView = detailView;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        private void ChatClient_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
          
            var os = this.Application.CreateObjectSpace(typeof(AiExtChatView));
            var ChatView = os.CreateObject<AiExtChatView>();
            ChatView.ChatHistory= os.CreateObject<IChatHistoryImp>();


            DetailView detailView = this.Application.CreateDetailView(os, ChatView);

            e.ShowViewParameters.CreatedView = detailView;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
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
