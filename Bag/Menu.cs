using System;
using System.Collections.Generic;

namespace BagAssignment
{
    public class Menu
    {
        private Bag bag = new Bag();

        public Menu() { }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                int option;
                PrintMenu();
                try
                {
                    if (!int.TryParse(Console.ReadLine(), out option))
                    {
                        Console.WriteLine("Invalid option. Press enter to continue.");
                        Console.ReadLine();
                        continue;
                    }
                }
                catch (System.FormatException) { option = -1; }

                switch (option)
                {
                    case 1:
                        AddElement();
                        break;

                    case 2:
                        RemoveElement();
                        break;

                    case 3:
                        GetFrequency();
                        break;

                    case 4:
                        GetSingleElementCount();
                        break;

                    case 5:
                        PrintBag();
                        break;

                    case 6:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Press enter to continue.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static private void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add an element");
            Console.WriteLine("2. Remove an element");
            Console.WriteLine("3. Get frequency of an element");
            Console.WriteLine("4. Get count of single elements");
            Console.WriteLine("5. Print the bag");
            Console.WriteLine("6. Exit");
        }

        private void AddElement()
        {
            Console.Write("Enter element to add: ");
            int element;
            if (!int.TryParse(Console.ReadLine(), out element))
            {
                Console.WriteLine("Invalid element. Press enter to continue.");
                Console.ReadLine();
                return;
            }

            bag.Insert(element);
            Console.WriteLine($"Element {element} added. Press enter to continue.");
            Console.ReadLine();
        }

        private void RemoveElement()
        {
            Console.Write("Enter element to remove: ");
            int element;
            if (!int.TryParse(Console.ReadLine(), out element))
            {
                Console.WriteLine("Invalid element. Press enter to continue.");
                Console.ReadLine();
                return;
            }

            bool removed = bag.Remove(element);
            if (removed)
            {
                Console.WriteLine($"Element {element} removed. Press enter to continue.");
            }
            else
            {
                Console.WriteLine($"Element {element} not found in bag. Press enter to continue.");
            }
            Console.ReadLine();
        }

        private void GetFrequency()
        {
            Console.Write("Enter element to get frequency: ");
            int element;
            if (!int.TryParse(Console.ReadLine(), out element))
            {
                Console.WriteLine("Invalid element. Press enter to continue.");
                Console.ReadLine();
                return;
            }

            int frequency = bag.Frequency(element);
            Console.WriteLine($"Frequency of element {element}: {frequency}. Press enter to continue.");
            Console.ReadLine();
        }

        private void GetSingleElementCount()
        {
            int count = bag.SingleElementCount();
            Console.WriteLine($"Count of single elements: {count}. Press enter to continue.");
            Console.ReadLine();
        }

        private void PrintBag()
        {
            Console.WriteLine("Bag contents:");
            foreach ((int element, int frequency) in bag)
            {
                Console.WriteLine($"{element}: {frequency}");
            }
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }
    }
}