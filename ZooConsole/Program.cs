using System;
using System.Collections.Generic;
using System.Linq;
using Animals;
using People;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// The class which is used to represent a como zoo console.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The console's zoo.
        /// </summary>
        private static Zoo zoo;

        /// <summary>
        /// Initializes a new instance of the Main class.
        /// </summary>
        /// <param name="args">Arguments to be passed to the application.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Como Zoo!");
            Console.Title = "Object-Oriented Programming 2: Zoo";
            bool exit = false;
            zoo = Zoo.NewZoo();
            ConsoleHelper.AttachDelegates(zoo);
            while (!exit)
            {
                Console.Write("] ");
                string command = Console.ReadLine();
                string[] commandWords = command.Split();

                switch (commandWords[0].ToLower().Trim())
                {
                    case "exit":
                        exit = true;
                        break;

                    case "restart":
                        zoo = Zoo.NewZoo();
                        ConsoleHelper.AttachDelegates(zoo);
                        zoo.BirthingRoomTemperature = 77;
                        Console.WriteLine("A new Como Zoo has been created.");
                        break;

                    case "help":
                        if (commandWords.Length == 2)
                        {
                            ConsoleHelper.ShowHelpDetail(commandWords[1]);
                        }
                        else if (commandWords.Length == 1)
                        {
                            ConsoleHelper.ShowHelp();
                        }
                        else
                        {
                            Console.WriteLine("Too many parameters entered.");
                        }

                        break;

                    case "temp":
                        try
                        {
                            ConsoleHelper.SetTemperature(zoo, commandWords[1]);
                        }
                        catch
                        {
                            Console.WriteLine("You have to set a temperature between 35 and 95");
                        }

                        break;

                    case "show":
                        try
                        {
                            ConsoleHelper.ProcessShowCommand(zoo, commandWords[1], commandWords[2]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("You need to enter a parameter in order for the show command to work.");
                        }

                        break;

                    case "add":
                        try
                        {
                            ConsoleHelper.ProcessAddCommand(zoo, commandWords[1]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("You need to enter a parameter in order for the add command to work.");
                        }

                        break;

                    case "remove":
                        try
                        {
                            ConsoleHelper.ProcessRemoveCommand(zoo, commandWords[1], commandWords[2]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("You need to enter a parameter in order for the remove command to work.");
                        }

                        break;

                    case "sort":
                        try
                        {
                            SortResult result = new SortResult();

                            if (commandWords[1] == "animals")
                            {
                                result = zoo.SortAnimals(commandWords[2], commandWords[3]);

                                foreach (Animal a in result.Objects)
                                {
                                    Console.WriteLine(a.ToString());
                                }
                            }
                            else if (commandWords[1] == "guests")
                            {
                                result = zoo.SortGuests(commandWords[2], commandWords[3]);

                                foreach (Guest g in result.Objects)
                                {
                                    Console.WriteLine(g.ToString());
                                }
                            }

                            Console.WriteLine("SORT TYPE: " + commandWords[2].ToUpper());
                            Console.WriteLine("SORT BY: " + commandWords[1].ToUpper());
                            Console.WriteLine("SORT VALUE: " + commandWords[3].ToUpper());
                            Console.WriteLine("SWAP COUNT: " + result.SwapCount);
                            Console.WriteLine("COMPARE COUNT: " + result.CompareCount);
                            Console.WriteLine("TIME: " + result.ElapsedMilliseconds);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"Sort command must be entered as: sort[sort type][sort value-- weight, name or age for animals][name or moneybalance for guests].");
                        }

                        break;

                    case "search":
                        if (commandWords[1] == "binary")
                        {
                            int loopCounter = 0;
                            string name = ConsoleUtil.InitialUpper(commandWords[2]);

                            // SortResult animals = zoo.SortAnimals("bubble", "name");
                            List<Animal> animals = zoo.Animals.ToList();
                            int minPostion = 0;
                            int maxPostion = animals.Count - 1;
                            while (minPostion <= maxPostion)
                            {
                                int middlePosition = (minPostion + maxPostion) / 2;
                                loopCounter++;
                                int compare = string.Compare(name, animals[middlePosition].Name);
                                if (compare > 0)
                                {
                                    minPostion = middlePosition + 1;
                                }
                                else if (compare < 0)
                                {
                                    maxPostion = middlePosition - 1;
                                }
                                else
                                {
                                    Console.WriteLine($"{name} found. {loopCounter} loops complete.");
                                    break;
                                }
                            }
                        }

                        if (commandWords[1] == "linear")
                        {
                            int loopCounter = 0;
                            string name = ConsoleUtil.InitialUpper(commandWords[2]);
                            foreach (Animal a in zoo.Animals)
                            {
                                loopCounter++;
                                if (a.Name == name)
                                {
                                    Console.WriteLine($"{name} found. {loopCounter} loops complete.");
                                    break;
                                }
                            }
                        }
                        else if (commandWords[1] == "guests")
                        {
                        }

                        break;

                    case "save":
                        try
                        {
                            ConsoleHelper.SaveFile(zoo, commandWords[1]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("You need to enter the file name in order for the save command to work.");
                        }

                        break;

                    case "load":
                        try
                        {
                            zoo = ConsoleHelper.LoadFile(commandWords[1]);
                            ConsoleHelper.AttachDelegates(zoo);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("You need to enter the file name in order for the load command to work.");
                        }

                        break;
                    case "query":
                            string query = ConsoleHelper.QueryHelper(zoo, commandWords[1]);
                       
                        break;

                    default:
                        Console.WriteLine("Invalid command entered");
                        break;
                }
            }
        }
    }
}
