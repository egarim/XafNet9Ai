using System;
using System.ComponentModel;
using System.Linq;
using XafSmartEditors.Razor.AiExtChatClientFunctions;
namespace XafSmartEditors.Razor
{
    public class ShoppingCart
    {
        public int NumPairOfSocks { get; set; }
        public decimal Total { get; set; }

        public void AdSocksToCart(int NumOfPairs)
        {
            NumPairOfSocks += NumOfPairs;
            if(_aiExtChatViewFunctions!=null)
            {
                _aiExtChatViewFunctions.FunctionOutput =
                   
                    $"{_aiExtChatViewFunctions.FunctionOutput} {Environment.NewLine} Added {NumOfPairs} pairs of socks to the cart. Total: {NumPairOfSocks} pairs (${NumPairOfSocks*PricePerPair})";
            }
        }

        private const float PricePerPair = 10f;

        [Description("Computes the price of socks, returning a value in dollars.")]
        public float GetPrice([Description("The number of pairs of socks to calculate the price for")] int Count)
        {
            if (_aiExtChatViewFunctions != null)
            {
                _aiExtChatViewFunctions.FunctionOutput = $"{_aiExtChatViewFunctions.FunctionOutput} {Environment.NewLine} Calculating price for {Count} pairs of socks. Total:${Count * PricePerPair}";
            }
            return Count * PricePerPair;
        }
        AiExtChatViewFunctions _aiExtChatViewFunctions;
        public ShoppingCart(AiExtChatViewFunctions aiExtChatViewFunctions)
        {
            _aiExtChatViewFunctions = aiExtChatViewFunctions;
        }
    }
}
