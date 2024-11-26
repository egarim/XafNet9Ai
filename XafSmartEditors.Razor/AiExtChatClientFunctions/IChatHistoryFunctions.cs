﻿using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafSmartEditors.Razor.RagChat;

namespace XafSmartEditors.Razor.AiExtChatClientFunctions
{
    public interface IChatHistoryFunctions
    {

        List<ChatMessage> Messages { get; set; }
        ShoppingCart ShoppingCart { get; set; }

    }
}
