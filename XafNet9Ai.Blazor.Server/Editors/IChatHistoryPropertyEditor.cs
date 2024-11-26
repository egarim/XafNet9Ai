using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using Microsoft.AspNetCore.Components;
using XafSmartEditors.Razor.RagChat;
using XafSmartEditors.Razor.AiExtChatClient;

namespace XafNet9Ai.Blazor.Server.Editors
{
    [PropertyEditor(typeof(IChatHistory), nameof(IChatHistory), true)]
    public class IChatHistoryPropertyEditor : BlazorPropertyEditorBase, IComplexViewItem
    {
        public IChatHistoryPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
        }

        IObjectSpace _objectSpace;
        XafApplication _application;

        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {
            _objectSpace = objectSpace;
            _application = application;
        }


        public override ChatHistoryComponentModel ComponentModel => (ChatHistoryComponentModel)base.ComponentModel;

        protected override IComponentModel CreateComponentModel()
        {
            var model = new ChatHistoryComponentModel();

            model.ValueChanged = EventCallback.Factory
                .Create<IChatHistory>(
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
            ComponentModel.Value = (IChatHistory)PropertyValue;
        }

        protected override object GetControlValueCore() => ComponentModel.Value;

        protected override void ApplyReadOnly()
        {
            base.ApplyReadOnly();
            ComponentModel?.SetAttribute("readonly", !AllowEdit);
        }
    }
}
