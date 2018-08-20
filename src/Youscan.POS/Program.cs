using System;
using System.Linq;
using Youscan.POS.Library;
using Youscan.POS.Library.Exceptions;

namespace Youscan.POS
{
    class Program
    {
        static void Main()
        {
            var terminal = new PointOfSaleTerminal();
            SetDefaultPrices(terminal);

            Console.WriteLine("Input product names to scan");
            Console.WriteLine("Example: ABCDABA");

            do
            {
                terminal.Reset();
                ScanProducts(terminal);

                var totalPrice = terminal.CalculateTotal();

                Console.WriteLine();
                Console.WriteLine($"Total price: {totalPrice}");
                Console.WriteLine("Press ESC to exit. Press any key to scan again");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }


        private static void SetDefaultPrices(IPointOfSaleTerminal terminal)
        {
            terminal.SetPricing("A", 1.25m);
            terminal.SetPricing("A", 3, 3m);
            terminal.SetPricing("B", 4.25m);
            terminal.SetPricing("C", 1m);
            terminal.SetPricing("C", 6, 5m);
            terminal.SetPricing("D", 0.75m);
        }

        private static void ScanProducts(IPointOfSaleTerminal terminal)
        {
            Console.Write("Products: ");
            var products = Console.ReadLine();
            var productNames = products.Select(n => n.ToString());

            foreach (var productName in productNames)
            {
                try
                {
                    Console.WriteLine($"Scanning product \"{productName}\"");
                    terminal.Scan(productName);
                }
                catch (ProductNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
