using System;
using System.Collections.Generic;

namespace Lab3_2
{
    class ShoppingList
    {
        private static Dictionary<string, double> Menu = new Dictionary<string, double>() 
        {
            {"Honeydew Melons", 2.76 }, {"Chef's Surprise", 4.51},
            {"Pizza", 99.11}, {"Ice Cream", 42.44},
            {"Clams", 92.91}, {"Fresh Yogurt", 3.21},
            {"Whole Milk", 20.32}, {"Lettuce", 10.30}
        };

        private static List<double> prices = new List<double>();
        private static List<string> items = new List<string>();
        private static List<int> quantities = new List<int>();
        private static double sum = 0;

        private static void viewMenu()
        {
            //Printing the column headers
            Console.WriteLine($"{"Item",-25} {"Price", -15}");
            Console.WriteLine($"{"======", -25} {"======", -15}");

            foreach (KeyValuePair<string, double> item in Menu)
            {
                Console.WriteLine($"{item.Key,-25} {$"${item.Value:0.00}", -15}"); //Printing each menu item and it's value
            }
        }

        private static void addItem(string item, double value, int quantity)
        {
            if (!items.Contains(item))
            {
                //Adding the item's quantity, price, and name to the proper lists
                prices.Add(value);
                items.Add(item); 
                quantities.Add(quantity); 
            }
            else
            {
                int index = items.IndexOf(item);
                quantities[index] += quantity; //Increasing the item's quantity
                prices[index] += prices[index] * quantity; //Keeping track of the item's individual total
            }
            sum += value * quantity; //Keeping track of the user's total price
            Console.WriteLine($"\nAdded '{item}' priced at ${value:0.00} to your cart!\n");
        }

        private static void viewReceipt()
        {
            double average = sum / items.Count; //Average price of the order

            Console.WriteLine($"\nYour total is ${sum:0.00}. \nHere's your receipt: ");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{items[i],-18} {$"${prices[i]:0.00}",-15}");
            }

            Console.WriteLine($"\nThe average price for this order was ${average:0.00}");
        }
        
        static void Main(string[] args)
        {
            double value;
            string input;
            bool shopping = true;
            int quantity = 1;
            Console.WriteLine("Welcome to The Market!");

            while (shopping)
            {
                viewMenu();
                Console.Write("\nWhat would you like to order? ");
                input = Console.ReadLine();

                if (Menu.TryGetValue(input, out value)) //Checking to see if the user's input is a valid key
                {
                    Console.Write($"How many {input}(s) would you like to order? ");
                    quantity = Int32.Parse(Console.ReadLine());
                    addItem(input, value, quantity);
                }
                else //User's input is not a valid key
                {
                    Console.WriteLine("Sorry, we don't have that item. Please try again.\n");
                    continue;
                }

                do
                {
                    Console.Write("Would you like to order something else? (enter 'y' or 'n'): ");
                    input = Console.ReadLine();
                } while (input != "y" && input != "n");

                if (input == "n") shopping = false;
            }

            viewReceipt();

            Console.WriteLine("\nYour items will be delivered tomorrow morning. \nThank you for using this software!\n");
        }
    }
}
