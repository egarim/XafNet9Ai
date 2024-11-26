using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;

namespace XafSmartEditors.Razor.AiExtChatClient
{
    public class ChatHistoryComponentModel : ComponentModelBase
    {
        public IChatHistory Value
        {
            get => GetPropertyValue<IChatHistory>();
            set => SetPropertyValue(value);
        }

        public EventCallback<IChatHistory> ValueChanged
        {
            get => GetPropertyValue<EventCallback<IChatHistory>>();
            set => SetPropertyValue(value);
        }


        public override Type ComponentType => typeof(AiExChatComponent);
    }
}
