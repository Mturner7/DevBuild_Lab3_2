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

        private static void addItem(string item, double price)
        {
            sum += price; //Keeping track of the total price
            prices.Add(price); //Adding an item's price to the "price" arraylist
            items.Add(item); //Adding each item to the user's item list

            Console.WriteLine($"\nAdded '{item}' priced at {price} to your cart!\n");
        }

        private static void viewReceipt()
        {
            int length = items.Count; //Length of items in the list
            double average = sum / items.Count; //Average price of the order

            Console.WriteLine($"\nYour total is ${sum:0.00}. Here's your receipt: ");

            //Printing out each item's name, price, and quantity
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"{items[i],-18} {$"${prices[i]:0.00}", -15}");
            }
            Console.WriteLine($"\nThe average price for this order was ${average:0.00}");
        }
        
        static void Main(string[] args)
        {
            double value;
            string input;
            bool shopping = true;

            Console.WriteLine("Welcome to The Market!");

            while (shopping)
            {
                viewMenu();
                Console.Write("\nWhat would you like to order? ");
                input = Console.ReadLine();

                if (Menu.TryGetValue(input, out value)) //Checking to see if the user's input is a valid key
                {
                    addItem(input, value);
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
