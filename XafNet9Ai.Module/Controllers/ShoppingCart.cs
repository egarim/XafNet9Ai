using System;
using System.ComponentModel;
using System.Linq;
namespace XafNet9Ai.Module.Controllers
{
    public class ShoppingCart
    {
        public object NumPairOfSocks { get; set; }

        public void AdSocksToCart(int NumOfPairs)
        {

        }
        [Description("Computes the price of socks, returning a value in dollars.")]
        public float GetPrice([Description("The number of pairs of socks to calculate the price for")] int Count)
        {
            return Count * 10f;
        }
        public ShoppingCart()
        {

        }
    }
}
