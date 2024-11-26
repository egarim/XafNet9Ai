using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using Microsoft.AspNetCore.Components;
using XafSmartEditors.Razor.AiExtChatClient;
using XafSmartEditors.Razor.RagChat;
using XafSmartEditors.Razor.AiExtChatClientFunctions;

namespace XafNet9Ai.Blazor.Server.Editors
{
    [PropertyEditor(typeof(IChatHistoryFunctions), nameof(IChatHistoryFunctions), true)]
    public class IChatHistoryWithFunctionsPropertyEditor : BlazorPropertyEditorBase, IComplexViewItem
    {
        public IChatHistoryWithFunctionsPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
        }

        IObjectSpace _objectSpace;
        XafApplication _application;

        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {
            _objectSpace = objectSpace;
            _application = application;
        }


        public override ChatHistoryComponentModelFunctions ComponentModel => (ChatHistoryComponentModelFunctions)base.ComponentModel;

        protected override IComponentModel CreateComponentModel()
        {
            var model = new ChatHistoryComponentModelFunctions();

            model.ValueChanged = EventCallback.Factory
                .Create<IChatHistoryFunctions>(
                    this,
                    value =>
                    {
                        model.Value = value;
                        OnControlValueChanged();
                        WriteValue();
                    });
            return model;
        }

        protected override void ReadValueCore()
        {
            base.ReadValueCore();
            ComponentModel.Value = (IChatHistoryFunctions)PropertyValue;
        }

        protected override object GetControlValueCore() => ComponentModel.Value;

        protected override void ApplyReadOnly()
        {
            base.ApplyReadOnly();
            ComponentModel?.SetAttribute("readonly", !AllowEdit);
        }
    }
}
