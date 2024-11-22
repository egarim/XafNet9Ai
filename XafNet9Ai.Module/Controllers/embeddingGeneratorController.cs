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

namespace XafNet9Ai.Module.Controllers
{
    public class EmbeddingGeneratorController : ViewController
    {
        SimpleAction GeneratedEmbeddings;
        public EmbeddingGeneratorController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            GeneratedEmbeddings = new SimpleAction(this, "Generate Embeddings", "View");
            GeneratedEmbeddings.Execute += GeneratedEmbeddings_Execute;
            
        }
        private async void GeneratedEmbeddings_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator = new OllamaEmbeddingGenerator(new Uri("http://127.0.0.1:11434"), modelId: "all-minilm:latest");
            Embedding<float> result = await embeddingGenerator.GenerateEmbeddingAsync("XAF is the best application Framework");
            Debug.WriteLine($"vector lenght:{result.Vector.Length}");

            List<string> strings = new List<string>() { "hello", "bye" };

            (string Value, Embedding<float> Embedding)[] ZipEmbeddings = await embeddingGenerator.GenerateAndZipAsync(strings);


            var InputEmbedding = await embeddingGenerator.GenerateEmbeddingAsync("hello");


            var Closest = from candidate in ZipEmbeddings
                          let similarity = TensorPrimitives.CosineSimilarity(candidate.Embedding.Vector.Span, InputEmbedding.Vector.Span)
                          orderby similarity descending
                          select new { Text = candidate.Value, Similarity = similarity };
                         

            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
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
