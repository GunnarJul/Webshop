using System;

namespace Webshop
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database();
            var basket = new ShoppingBasket(db);
            
            basket.AddToBasket("mig", 1, 1);
            Console.WriteLine ($"Samlet pris :{basket.GetTotalPrice("mig")}");
            
            
            Console.WriteLine("Hey there - press any key");

            Console.ReadKey(); 
        }
    }
}
