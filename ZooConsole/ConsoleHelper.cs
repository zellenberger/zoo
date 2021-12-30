using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts;
using Animals;
using People;
using Reproducers;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// The class which is used to represent a como zoo helper console.
    /// </summary>
    internal static class ConsoleHelper
    {
        /// <summary>
        /// Processes add command in console.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="type">The type of the animal.</param>
        public static void ProcessAddCommand(Zoo zoo, string type)
        {
            switch (type)
            {
                case "animal":
                    try
                    {
                       ConsoleHelper.AddAnimal(zoo);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("You need to enter an animal in order for the add command to work.");
                    }

                    break;

                case "guest":
                    try
                    {
                        ConsoleHelper.AddGuest(zoo);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("You need to enter a guest in order for the add command to work.");
                    }

                    break;

                default:
                    Console.WriteLine("Only adding animals or guests is supported by the add command .");
                    break;
            }
        }

        /// <summary>
        /// Processes remove command in console.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="type">The type of the animal.</param>
        /// <param name="name">The name of the animal.</param>
        public static void ProcessRemoveCommand(Zoo zoo, string type, string name)
        {
            ConsoleUtil.InitialUpper(name);

            switch (type)
            {
                case "animal":
                    ConsoleHelper.RemoveAnimal(zoo, name);
                    break;

                case "guest":
                    ConsoleHelper.RemoveGuest(zoo, name);
                    break;

                default:
                    Console.WriteLine("Only removing animals or guests is supported by the remove command .");
                    break;
            }
        }

        /// <summary>
        /// Show commands in console.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="type">The type of the animal.</param>
        /// <param name="name">The name of the animal.</param>
        public static void ProcessShowCommand(Zoo zoo, string type, string name)
        {
            name = ConsoleUtil.InitialUpper(name);

            switch (type)
            {
                case "animal":
                    try
                    {
                        ShowAnimal(zoo, name);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("You need to enter a name in order for the animal command to work.");
                    }

                    break;

                case "guest":
                    try
                    {
                        ShowGuest(zoo, name);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("You need to enter a name in order for the guest command to work.");
                    }

                    break;

                case "cage":
                    try
                    {
                        ShowCage(zoo, name);
                    }
                    catch
                    {
                        Console.WriteLine("You need to enter a name in order for the cage command to work.");
                    }

                    break;

                case "children":
                    try
                    {
                        ShowChildren(zoo, name);
                    }
                    catch
                    {
                        Console.WriteLine("You need to enter a name in order for the children command to work.");
                    }

                    break;

                default:
                    Console.WriteLine("Only animals and guests can be shown in the console.");
                    break;
            }
        }

        /// <summary>
        /// Shows the help in detail.
        /// </summary>
        /// <param name="command">The command for help.</param>
        public static void ShowHelpDetail(string command)
        {
            Dictionary<string, string> arguments;
            switch (command)
            {
                case "show":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("objectType", "The type of object to show (ANIMAL, GUEST, or CAGE).");
                    arguments.Add("objectName", "The name of the object to show (use an animal name for CAGE).");
                    ConsoleUtil.WriteHelpDetail(command, "Show details of an object.", arguments);
                    break;

                case "remove":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("objectType", "The type of object to remove (ANIMAL, GUEST, or CAGE).");
                    arguments.Add("objectName", "The name of the object to remove (use an animal name for CAGE).");
                    ConsoleUtil.WriteHelpDetail(command, "Removes an object from the zoo.", arguments);
                    break;

                case "temp":
                    arguments = new Dictionary<string, string>();
                    ConsoleUtil.WriteHelpDetail(command, "Sets the temperature in the birthing room.", "temperature", "Temperature to set the temperature to (in farenheight)");
                    break;

                case "add":
                    arguments = new Dictionary<string, string>();
                    ConsoleUtil.WriteHelpDetail(command, "Adds an object to the zoo.", "objectType", "The type of object to add (ANIMAL or GUEST)");
                    break;

                case "restart":
                    ConsoleUtil.WriteHelpDetail(command, "Creates a new zoo and corresponding objects.");
                    break;

                case "exit":
                    ConsoleUtil.WriteHelpDetail(command, "Exits the program.");
                    break;

                case "save":
                    ConsoleUtil.WriteHelpDetail(command, "Saves the zoo file.");
                    break;

                case "load":
                    ConsoleUtil.WriteHelpDetail(command, "Loads the zoo file.");
                    break;
            }
        }

        /// <summary>
        /// Shows the help command.
        /// </summary>
        public static void ShowHelp()
        {
            Console.WriteLine("OOP 2 Zoo Help Index");
            ConsoleUtil.WriteHelpDetail("help", "Show help detail", "[command]", "The command for which to show help details.");
            Console.WriteLine("Known commands:");
            Console.WriteLine("EXIT: Exits the application.");
            Console.WriteLine("RESTART: Creates a new zoo.");
            Console.WriteLine("TEMP: Sets the birthing room temperature.");
            Console.WriteLine("SHOW ANIMAL [animal name]: Displays information for the specified animal.");
            Console.WriteLine("SHOW GUEST [guest name]: Displays information for specified guest.");
            Console.WriteLine("ADD: [animal] or [guest] with values corresponding to create them.");
            Console.WriteLine("REMOVE: Removes a [animal] or [guest]");
            Console.WriteLine("SAVE: Saves the zoo file.");
            Console.WriteLine("LOAD: loads the zoo file.");
        }

        /// <summary>
        /// Sets birthing room temperature.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="temperature">The temperature of the birthing room.</param>
        public static void SetTemperature(Zoo zoo, string temperature)
        {
            try
            {
                zoo.BirthingRoomTemperature = double.Parse(temperature);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("You need to state a number parameter in order to change the temperature.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("You need to enter a parameter in order for the temperaure command to work.");
            }
        }

        /// <summary>
        /// The save file method.
        /// </summary>
        /// <param name="zoo">The zoo parameter.</param>
        /// <param name="fileName">The file name.</param>
        public static void SaveFile(Zoo zoo, string fileName)
        {
            try
            {
                if (fileName != null)
                {
                    zoo.SaveToFile(fileName);
                    Console.WriteLine("Your zoo has been successfully saved.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Your zoo was not saved successfully.");
            }
        }

        /// <summary>
        /// The load file method.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>Returns zoo.</returns>
        public static Zoo LoadFile(string fileName)
        {
            Zoo zoo = null;

            try
            {
                zoo = Zoo.LoadFromFile(fileName);
                Console.WriteLine("Your zoo has been successfully loaded.");
            }
            catch (Exception)
            {
                Console.WriteLine("Your zoo was not loaded successfully.");
            }

            return zoo;
        }

        /// <summary>
        /// The Attach Delegates method.
        /// </summary>
        /// <param name="zoo">The zoo parameter.</param>
        public static void AttachDelegates(Zoo zoo)
        {
            zoo.OnBirthingRoomTemperatureChange = (double previousTemp, double currentTemp) =>
            {
                Console.WriteLine($"Previous temperature: {previousTemp:0.0}.");
                Console.WriteLine($"New temperature: {currentTemp:0.0}.");
            };
        }

        /// <summary>
        /// The query helper method.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="query">The query parameter.</param>
        /// <returns>Returns the result.</returns>
        public static string QueryHelper(Zoo zoo, string query)
        {
            string result = "Something went wrong.";

            switch (query)
            {
                case "totalanimalweight":
                    result = zoo.Animals.ToList().Sum(a => a.Weight).ToString();

                    break;

                case "averageanimalweight":
                    result = zoo.Animals.ToList().Average(a => a.Weight).ToString();

                    break;

                case "animalcount":
                    result = zoo.Animals.ToList().Count().ToString();

                    break;

                case "getheavyanimals":
                    result = "Heaviest Animals:\n";
                    zoo.GetHeavyAnimals().ToList().ForEach(a => result += a.ToString() + "\n");

                    break;

                case "getyoungguests":
                    result = "Youngest Guests:\n";
                    zoo.GetYoungGuests().ToList().ForEach(a => result += a.ToString() + "\n");

                    break;

                case "getfemaledingoes":
                    result = "Female Dingoes:\n";
                    zoo.GetFemaleDingoes().ToList().ForEach(a => result += a.ToString() + "\n");

                    break;

                case "getguestsbyage":
                    result = "Guests By Age:\n";
                    zoo.GetGuestsByAge().ToList().ForEach(a => result += a.ToString() + "\n");

                    break;

                case "getflyinganimals":
                    result = "Flying Animals:\n";
                    zoo.GetFlyingAnimals().ToList().ForEach(a => result += a.ToString() + "\n");

                    break;

                case "getadoptedanimals":
                    result = "Guests and their Adopted Animals:\n";
                    zoo.GetAdoptedAnimals().ToList().ForEach(a => result += a.ToString() + "\n");

                    break;

                case "totalbalancebycolor":
                    result = "Total Balance of Wallets by Color:\n";
                    zoo.GetTotalBalanceByWalletColor().ToList().ForEach(a => result += a.ToString() + "\n");

                    break;

                case "averageweightbyanimaltype":
                    result = "Average Weight of Animals by Type:\n";
                    zoo.GetAverageAnimalWeightByAnimalType().ToList().ForEach(a => result += a.ToString() + "\n");

                    break;
            }

            Console.WriteLine(result);
            return result;
        }

        /// <summary>
        /// Adds the animal to the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        private static void AddAnimal(Zoo zoo)
        {
            bool success = false;
            AnimalType type = AnimalType.Chimpanzee;
            Animal animal = AnimalFactory.CreateAnimal(type, "Ringo", 5, 12.00, Gender.Male);
            while (!success)
            {
                try
                {
                    type = ConsoleUtil.ReadAnimalType();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    animal.Name = ConsoleUtil.InitialUpper(animal.Name = ConsoleUtil.ReadAlphabeticValue("Name"));
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    success = false;
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    animal.Gender = ConsoleUtil.ReadGender();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    success = false;
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    animal.Age = ConsoleUtil.ReadIntValue("Age");
                    success = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("The animal's age must be between 0 and 100");
                    success = false;
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    animal.Weight = ConsoleUtil.ReadDoubleValue("Weight");
                    success = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("The animal's weight must be between 0 and 1000");
                    success = false;
                }
            }

            success = false;

            zoo.AddAnimal(animal);
            Console.WriteLine($"{animal.Name} has been added to the zoo");
            ShowAnimal(zoo, animal.Name);
        }

        /// <summary>
        /// Adds the guest to the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        private static void AddGuest(Zoo zoo)
        {
            Account account = new Account();
            Guest guest = new Guest("Joe", 25, 0.00m, WalletColor.Indigo, Gender.Male, account);
            bool success = false;
            while (!success)
            {
                try
                {
                    guest.Name = ConsoleUtil.InitialUpper(guest.Name = ConsoleUtil.ReadAlphabeticValue("Name"));
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    success = false;
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Gender = ConsoleUtil.ReadGender();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    success = false;
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Age = ConsoleUtil.ReadIntValue("Age");
                    success = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("The guest's age must be between 0 and 100");
                    success = false;
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Wallet.AddMoney((decimal)ConsoleUtil.ReadDoubleValue("Wallet money balance"));
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    success = false;
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Wallet.Color = ConsoleUtil.ReadColor();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    success = false;
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.CheckingAccount.AddMoney(decimal.Parse(ConsoleUtil.ReadStringValue("Checking account balance")));
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    success = false;
                }
            }

            zoo.AddGuest(guest, zoo.SellTicket(guest));
            ShowGuest(zoo, guest.Name);
        }

        /// <summary>
        /// Shows the selected animal.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="name">The name of the animal.</param>
        private static void ShowAnimal(Zoo zoo, string name)
        {
            string animalName = ConsoleUtil.InitialUpper(name);
            Animal animal = zoo.FindAnimal(a => a.Name == name);
            if (animal != null)
            {
                Console.WriteLine($"The following animal was found: { animal}");
            }
            else
            {
                Console.WriteLine($"The animal could not be found. { animal}");
            }
        }

        /// <summary>
        /// Shows the selected guest.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="name">The name of the guest.</param>
        private static void ShowGuest(Zoo zoo, string name)
        {
            string guestName = ConsoleUtil.InitialUpper(name);
            Guest guest = zoo.FindGuest(g => g.Name == name);
            if (guest != null)
            {
                Console.WriteLine($"The following guest was found: {guest}");
            }
            else
            {
                Console.WriteLine($"The guest could not be found. {guest}");
            }
        }

        /// <summary>
        /// Removes the animal to the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="name">The name of the animal.</param>
        private static void RemoveAnimal(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == name);
            if (animal != null)
            {
                zoo.RemoveAnimal(animal);
                Console.WriteLine("The animal was removed.");
            }
            else if (animal == null)
            {
                Console.WriteLine("The animal could not be found.");
            }
        }

        /// <summary>
        /// Removes the animal to the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="name">The name of the animal.</param>
        private static void RemoveGuest(Zoo zoo, string name)
        {
            Guest guest = zoo.FindGuest(g => g.Name == name);
            if (guest != null)
            {
                zoo.RemoveGuest(guest);
                Console.WriteLine("The guest was removed.");
            }
            else if (guest == null)
            {
                Console.WriteLine("The guest could not be found.");
            }
        }

        /// <summary>
        /// The show cage method.
        /// </summary>
        /// <param name="zoo">The zoo parameter.</param>
        /// <param name="animalName">The animal name parameter.</param>
        private static void ShowCage(Zoo zoo, string animalName)
        {
            string name = ConsoleUtil.InitialUpper(animalName);
            Animal animal = zoo.FindAnimal(a => a.Name == name);
            Cage cage = null;
            if (animal != null)
            {
                cage = zoo.FindCage(animal.GetType());
                Console.WriteLine(cage.ToString());
            }
            else
            {
                Console.WriteLine("Yo dawg that animal don't exist, fool!");
            }
        }

        /// <summary>
        /// The show children method.
        /// </summary>
        /// <param name="zoo">The zoo parameter.</param>
        /// <param name="name">The name parameter.</param>
        private static void ShowChildren(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == name);
            WalkTree(animal, string.Empty);
        }

        /// <summary>
        /// The wal tree method.
        /// </summary>
        /// <param name="animal">The animal.</param>
        /// <param name="prefix">The prefix.</param>
        private static void WalkTree(Animal animal, string prefix)
        {
            Console.WriteLine($"{prefix}{animal.Name}");

            foreach (Animal a in animal.Children)
            {
                WalkTree(a, prefix + "  ");
            }
        }     
    }
}
