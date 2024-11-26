using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafSmartEditors.Razor.AiExtChatClient;
using XafSmartEditors.Razor.RagChat;

namespace XafNet9Ai.Module.Controllers
{
    public class ChatUiController : ViewController
    {
        SimpleAction ChatClient;
        public ChatUiController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            ChatClient = new SimpleAction(this, "Chat client with U.I", "View");
            ChatClient.Execute += ChatClient_Execute;
            
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
