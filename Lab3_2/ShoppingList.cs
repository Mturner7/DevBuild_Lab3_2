using System;
using System.Collections.Generic;

namespace Lab3_2
{
    class ShoppingList
    {
        private static Dictionary<string, decimal> Menu = new Dictionary<string, decimal>() 
        {
            {"Honeydew Melons", 2.76m }, {"Chef's Surprise", 4.51m},
            {"Pizza", 99.11m}, {"Ice Cream", 42.44m},
            {"Clams", 92.91m}, {"Fresh Yogurt", 3.21m},
            {"Whole Milk", 20.32m}, {"Lettuce", 10.30m}
        };

        private static List<decimal> prices = new List<decimal>();
        private static List<string> items = new List<string>();
        private static List<int> quantities = new List<int>();
        
        private static decimal sum = 0;
        private static int lowIndex = 0;
        private static int highIndex = 0;

        private static void viewMenu()
        {
            //Printing the column headers
            Console.WriteLine($"\n{"Item",-25} {"Price", -15}");
            Console.WriteLine($"{"======", -25} {"======", -15}");

            //Printing the Menu
            foreach (KeyValuePair<string, decimal> item in Menu)
            {
                Console.WriteLine($"{item.Key,-25} {$"${item.Value:0.00}", -15}");
            }
        }

        private static void addItem(string item, decimal value, int quantity)
        {
            int index;
            if (!items.Contains(item))
            {
                //Adding the item's quantity, price, and name to the proper lists
                prices.Add(value * quantity);
                items.Add(item); 
                quantities.Add(quantity);
                index = items.Count - 1;
            }
            else
            {
                index = items.IndexOf(item);
                quantities[index] += quantity; //Increasing the item's quantity
                prices[index] += (value * quantity); //Keeping track of the item's individual total
            }

            //Keeping track of the least and most exspensive items in the list 
            if (prices[index] < prices[lowIndex]) lowIndex = index;
            if (prices[index] > prices[highIndex]) highIndex = index;

            sum += value * quantity; //Keeping track of the user's total price
            Console.WriteLine($"\nAdded '{item}' x{quantity} priced at ${value:0.00} (each) to your cart!\n");
        }

        private static void viewReceipt()
        {
            decimal average = sum / items.Count; //Average price of the order

            //Printing the receipt
            Console.WriteLine($"\nYour total is ${sum:0.00}. \nYour receipt: ");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{items[i], -25} x{quantities[i], -10} {$"${prices[i]:0.00}",10}");
            }
            //Printing the average price
            Console.WriteLine($"\nThe average price for this order was ${average:0.00}");
            Console.WriteLine($"Most Expensive Item(s): {items[highIndex]}");
            Console.WriteLine($"Least Expensive Item(s): {items[lowIndex]}\n\n");
        }
        
        static void Main(string[] args)
        {
            decimal value;
            string input;
            bool shopping = true;
            int quantity = 1;
            Console.WriteLine("Welcome to The Market!");

            while (shopping)
            {
                //Printing the menu and prompting the user
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

                do //Continuously prompt the user to continue until the correct input (y/n) is received
                {
                    Console.Write("Would you like to order something else? (enter 'y' or 'n'): ");
                    input = Console.ReadLine().ToLower();
                } while (input != "y" && input != "n");

                if (input == "n") shopping = false;
            }
             
            viewReceipt(); //Printing the user's receipt

            Console.WriteLine("Thank you for using this software!\n");
        }
    }
}
