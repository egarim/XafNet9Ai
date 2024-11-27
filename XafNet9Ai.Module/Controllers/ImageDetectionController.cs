using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using XafNet9Ai.Module.BusinessObjects;
using XafSmartEditors.Razor;

namespace XafNet9Ai.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
    public partial class ImageDetectionController : ViewController
    {
        SimpleAction CleanResults;
        SimpleAction DescribeImage;
        SimpleAction Analyze;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public ImageDetectionController()
        {
            InitializeComponent();
            Analyze = new SimpleAction(this, "Analyze", "View");
            Analyze.Execute += Analyze_Execute;
            Analyze.TargetObjectType = typeof(XafNet9Ai.Module.BusinessObjects.CatCollection);
            Analyze.TargetViewType = ViewType.DetailView;

            DescribeImage = new SimpleAction(this, "Describe Image", "View");
            DescribeImage.Execute += DescribeImage_Execute;
            DescribeImage.TargetObjectType = typeof(XafNet9Ai.Module.BusinessObjects.CatCollectionDetail);
            DescribeImage.TargetViewType = ViewType.DetailView;

            CleanResults = new SimpleAction(this, "Clean Results", "View");
            CleanResults.Execute += CleanResults_Execute;
            CleanResults.TargetObjectType = typeof(XafNet9Ai.Module.BusinessObjects.CatCollection);
            CleanResults.TargetViewType = ViewType.DetailView;

            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void CleanResults_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var CurrentCatCollection = this.View.CurrentObject as XafNet9Ai.Module.BusinessObjects.CatCollection;
            CurrentCatCollection.NumberOfBlackCats = 0;
            CurrentCatCollection.NumberOfWhiteCats = 0;
            CurrentCatCollection.NumberOfOtherAnimals = 0;
            CurrentCatCollection.NumberOfNotAnimals = 0;

            foreach (CatCollectionDetail catCollectionDetail in CurrentCatCollection.CatCollectionDetails)
            {
                catCollectionDetail.Description = "";
            }
            if (this.View.ObjectSpace.IsModified)
            {
                this.View.ObjectSpace.CommitChanges();
            }

        }
        private async void DescribeImage_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var CurrentDetail = this.View.CurrentObject as XafNet9Ai.Module.BusinessObjects.CatCollectionDetail;
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
            ChatMessage Message = new ChatMessage(ChatRole.User, "Describe what is in the picture in 500 or less characters");

            Message.Contents.Add(new ImageContent(CurrentDetail.Image.MediaData, "image/jpg"));
            var Client = ChatClientHelper.GetChatClient();
            var Result = await Client.CompleteAsync(new List<ChatMessage>() { Message });
            CurrentDetail.Description= Result.Message.Text;
            if(this.View.ObjectSpace.IsModified)
            {
                this.View.ObjectSpace.CommitChanges();
            }
        }
   

        private async void Analyze_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            var CurrentCatCollection = this.View.CurrentObject as XafNet9Ai.Module.BusinessObjects.CatCollection;

            CurrentCatCollection.Processing = true;
            var Message = new ChatMessage(ChatRole.User, "Analyze this images to count the number of black cats, white cats, other animals and objects that are NOT animals");

            foreach (BusinessObjects.CatCollectionDetail catCollectionDetail in CurrentCatCollection.CatCollectionDetails)
            {
                Message.Contents.Add(new ImageContent(catCollectionDetail.Image.MediaData, "image/jpg"));
            }
            var Client = ChatClientHelper.GetChatClient();
           



            List<Func<Task<ChatCompletion>>> funcs = new List<Func<Task<ChatCompletion>>>();
            funcs.Add(async () => { return await Client.CompleteAsync<CatCollectionDescription>(new List<ChatMessage>() { Message }); });
           
            
            //Action trigger each time a task is done and progress is notified
            Action<int, string, Object> onProgressChanged = OnReportProgress;

            var worker = new AsyncBackgroundWorker<ChatCompletion>(
                  funcs,//List of task to run
                  onProgressChanged,//Progress report action
                  result => ProcessingDone(result) //Action to run when all tasks are done
              );

            worker.Start();

        }
        protected virtual void ProcessingDone(Dictionary<int, object> results)
        {
            object rawRepresentation = (results[0] as ChatCompletion).RawRepresentation;
            var OaiChatCompletion = rawRepresentation as OpenAI.Chat.ChatCompletion;
            var Json = OaiChatCompletion.Content[0].Text;
            CatCollectionDescription CatCollectionDescription=JsonSerializer.Deserialize<CatCollectionDescription>(Json); 
            var CurrentCatCollection= this.View.CurrentObject as XafNet9Ai.Module.BusinessObjects.CatCollection;
            CurrentCatCollection.NumberOfBlackCats = CatCollectionDescription.NumberOfBlackCats;
            CurrentCatCollection.NumberOfWhiteCats = CatCollectionDescription.NumberOfWhiteCats;
            CurrentCatCollection.NumberOfOtherAnimals = CatCollectionDescription.NumberOfOtherAnimals;
            CurrentCatCollection.NumberOfNotAnimals = CatCollectionDescription.NumberOfNotAnimals;

            CurrentCatCollection.Processing = false;
            if (this.View.ObjectSpace.IsModified)
            {
                this.View.ObjectSpace.CommitChanges();
            }
            //HACK this is in the U.I thread, so we can interact with the U.I and the view and object space of this controller
        }
        protected virtual void OnReportProgress(int progress, string status, object result)
        {
            //HACK this is in the U.I thread, so we can interact with the U.I and the view and object space of this controller
            MessageOptions options = new MessageOptions();
            options.Duration = 2000;
            options.Message = status;
            options.Type = InformationType.Success;
            options.Web.Position = InformationPosition.Right;
            options.Win.Caption = "Success";
            options.Win.Type = WinMessageType.Toast;
            Application.ShowViewStrategy.ShowMessage(options);

            Debug.WriteLine($"{status} - Result: {result}");
        }
        public class CatCollectionDescription
        {
            [JsonPropertyName("numberOfBlackCats")]
            [Description("Number of black cats")]
            public int NumberOfBlackCats { get; set; }
            [JsonPropertyName("numberOfWhiteCats")]
            [Description("Number of white cats")]
            public int NumberOfWhiteCats { get; set; }
            [JsonPropertyName("numberOfOtherAnimals")]
            [Description("Number of other animals that are NOT cats")]
            public int NumberOfOtherAnimals { get; set; }
            [JsonPropertyName("numberOfNotAnimals")]
            [Description("Number of other objects that are not animals")]
            public int NumberOfNotAnimals { get; set; }
            public CatCollectionDescription()
            {
                
            }
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
