# Xaf Net 9 A.I Extension

This XAF solution is compiled in Dot Net 9 and uses the new Microsoft AI Extensions library.

[https://learn.microsoft.com/en-us/dotnet/ai/ai-extensions](https://learn.microsoft.com/en-us/dotnet/ai/ai-extensions)

## Use Cases

### Embbeding generation


[XpoEmbedding.cs](https://github.com/egarim/XafNet9Ai/blob/master/XafNet9Ai.Module/BusinessObjects/XpoEmbedding.cs)

[embeddingGeneratorController.cs](https://github.com/egarim/XafNet9Ai/blob/master/XafNet9Ai.Module/Controllers/embeddingGeneratorController.cs)
   
### Image Analysis

[embeddingGeneratorController.cs](https://github.com/egarim/XafNet9Ai/blob/master/XafNet9Ai.Module/Controllers/ImageDetectionController.cs)

### Structured output

Analyze_Execute Method

[embeddingGeneratorController.cs](https://github.com/egarim/XafNet9Ai/blob/master/XafNet9Ai.Module/Controllers/ImageDetectionController.cs)


### Fuctions

[ChatClientHelper.cs](https://github.com/egarim/XafNet9Ai/blob/master/XafSmartEditors.Razor/ChatClientHelper.cs)
[ShoppingCart.cs](https://github.com/egarim/XafNet9Ai/blob/master/XafSmartEditors.Razor/ShoppingCart.cs)
[AiExChatComponentFunctions.razor.cs](https://github.com/egarim/XafNet9Ai/blob/master/XafSmartEditors.Razor/AiExtChatClientFunctions/AiExChatComponentFunctions.razor.cs)

### Middleware

ChatClientHelper.cs](https://github.com/egarim/XafNet9Ai/blob/master/XafSmartEditors.Razor/ChatClientHelper.cs)
[Language Middleware](https://github.com/egarim/XafNet9Ai/blob/master/XafSmartEditors.Razor/Middleware/UseLanguageStep.cs)    
[Rate Limit](https://github.com/egarim/XafNet9Ai/blob/master/XafSmartEditors.Razor/Middleware/UseRateLimitMiddleware.cs)    