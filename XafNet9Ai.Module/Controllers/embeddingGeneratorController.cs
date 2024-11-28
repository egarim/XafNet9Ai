using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics.Tensors;
using System.Text;
using System.Threading.Tasks;
using XafNet9Ai.Module.BusinessObjects;

namespace XafNet9Ai.Module.Controllers
{
    public class EmbeddingGeneratorController : ViewController
    {
        SimpleAction GenerateZipEmbeddings;
        ParametrizedAction SemanticSearch;
        SimpleAction GeneratedEmbeddings;
        public EmbeddingGeneratorController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            GeneratedEmbeddings = new SimpleAction(this, "Generate Embeddings", "View");
            GeneratedEmbeddings.Execute += GeneratedEmbeddings_Execute;
            GeneratedEmbeddings.TargetViewType = ViewType.DetailView;
            this.TargetObjectType = typeof(XpoEmbedding);

            GenerateZipEmbeddings = new SimpleAction(this, "Generate Zip Embeddings", "View");
            GenerateZipEmbeddings.Execute += GenerateZipEmbeddings_Execute;
            

            SemanticSearch = new ParametrizedAction(this, "Semantic Search", "View", typeof(string));
            SemanticSearch.Execute += SemanticSearch_Execute;
            SemanticSearch.TargetViewType = ViewType.ListView;

        }
        private async void GenerateZipEmbeddings_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator = new OllamaEmbeddingGenerator(new Uri("http://127.0.0.1:11434"), modelId: "all-minilm:latest");

            var strings= this.View.ObjectSpace.GetObjectsQuery<XpoEmbedding>().Select(x => x.Text).ToList();

            (string Value, Embedding<float> Embedding)[] ZipEmbeddings = await embeddingGenerator.GenerateAndZipAsync(strings);

          

        }
        private async void SemanticSearch_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            var parameterValue = (string)e.ParameterCurrentValue;
            IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator = new OllamaEmbeddingGenerator(new Uri("http://127.0.0.1:11434"), modelId: "all-minilm:latest");
            Embedding<float> InputEmbedding = await embeddingGenerator.GenerateEmbeddingAsync(parameterValue);
            
            
            Debug.WriteLine($"vector lenght:{InputEmbedding.Vector.Length}");


            var Embeddings= this.View.ObjectSpace.GetObjectsQuery<XpoEmbedding>().ToList();

            var Closest = from candidate in Embeddings
                          let similarity = TensorPrimitives.CosineSimilarity(candidate.GetEmbedding().Vector.Span, InputEmbedding.Vector.Span)
                          orderby similarity descending
                          select new { Text = candidate.Text, Similarity = similarity, Code=candidate.Code };


            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in Closest)
            {
                stringBuilder.AppendLine($"Code:{item.Code} Text:{item.Text} Similarity:{item.Similarity}");
            }

            MessageOptions options = new MessageOptions();
            options.Duration = 20000;
            options.Message = stringBuilder.ToString();
            options.Type = InformationType.Success;
            options.Web.Position = InformationPosition.Right;
            options.Win.Caption = "Success";
            options.Win.Type = WinMessageType.Toast;
            Application.ShowViewStrategy.ShowMessage(options);
        }
        private async void GeneratedEmbeddings_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var CurrentXpoEmbedding = this.View.CurrentObject as XpoEmbedding;
            IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator = new OllamaEmbeddingGenerator(new Uri("http://127.0.0.1:11434"), modelId: "all-minilm:latest");
            
            
            Embedding<float> result = await embeddingGenerator.GenerateEmbeddingAsync(CurrentXpoEmbedding.Text);
            Debug.WriteLine($"vector lenght:{result.Vector.Length}");

            CurrentXpoEmbedding.Data = result.Vector.ToArray();

            if(this.ObjectSpace.IsModified)
            {
                this.ObjectSpace.CommitChanges();
            }   
            //List<string> strings = new List<string>() { CurrentXpoEmbedding.Text };

            //(string Value, Embedding<float> Embedding)[] ZipEmbeddings = await embeddingGenerator.GenerateAndZipAsync(strings);








    


           
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
