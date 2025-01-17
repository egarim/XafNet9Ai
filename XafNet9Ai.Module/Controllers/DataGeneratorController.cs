using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XafNet9Ai.Module.BusinessObjects;

namespace XafNet9Ai.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.WindowController.
    public partial class DataGeneratorController : WindowController
    {
        SimpleAction GenerateSalesInformation;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public DataGeneratorController()
        {
            InitializeComponent();
            GenerateSalesInformation = new SimpleAction(this, "GenerateSalesInformation", "View");
            GenerateSalesInformation.Execute += GenerateSalesInformation_Execute;
            
            // Target required Windows (via the TargetXXX properties) and create their Actions.
        }
        private void GenerateSalesInformation_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var Os = Application.CreateObjectSpace<Invoice>() as XPObjectSpace;
            SampleDataGenerator.CreateSampleData(Os.Session);
            Os.CommitChanges();

            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target Window.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
