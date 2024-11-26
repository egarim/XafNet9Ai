using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using XafSmartEditors.Razor.AiExtChatClient;

namespace XafSmartEditors.Razor.AiExtChatClientFunctions
{
    public class ChatHistoryComponentModelFunctions : ComponentModelBase
    {
        public IChatHistoryFunctions Value
        {
            get => GetPropertyValue<IChatHistoryFunctions>();
            set => SetPropertyValue(value);
        }

        public EventCallback<IChatHistoryFunctions> ValueChanged
        {
            get => GetPropertyValue<EventCallback<IChatHistoryFunctions>>();
            set => SetPropertyValue(value);
        }


        public override Type ComponentType => typeof(AiExChatComponentFunctions);
    }
}
