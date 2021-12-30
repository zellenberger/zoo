using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Animals;
using People;
using Reproducers;
using Utilities;

namespace ZooConsole
{
    /// <summary>
    /// The class which is used to represent a como zoo utility console.
    /// </summary>
    internal static class ConsoleUtil
    {
        /// <summary>
        /// The initial upper method.
        /// </summary>
        /// <param name="value">The name of the value.</param>
        /// <returns>Returns a name of the selected value.</returns>
        public static string InitialUpper(string value)
        {
            string name = null;
            if (value != null && value.Length > 0)
            {
                name = char.ToUpper(value[0]) + value.Substring(1);
            }

            return name;
        }

        /// <summary>
        /// The read type method.
        /// </summary>
        /// <returns>Returns a result of the selected value.</returns>
        public static AnimalType ReadAnimalType()
        {
            AnimalType result = AnimalType.Dingo;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Type");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found...
                if (Enum.TryParse<AnimalType>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid animal type. Possible animal types: " + GetTypes(typeof(AnimalType)));
                }
            }

            return result;
        }

        /// <summary>
        /// The get types method.
        /// </summary>
        /// <param name="type">The name of the value.</param>
        /// <returns>Returns a type of the selected value.</returns>
        public static string GetTypes(Type type)
        {
            StringBuilder typeList = new StringBuilder();

            foreach (string at in Enum.GetNames(type))
            {
                typeList.Append(at + "|");
            }

            return "|" + typeList.ToString();
        }

        /// <summary>
        /// The read alphabetical method.
        /// </summary>
        /// <param name="prompt">The name of the value.</param>
        /// <returns>Returns a result of the selected value.</returns>
        public static string ReadAlphabeticValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                result = ConsoleUtil.ReadStringValue(prompt);

                if (Regex.IsMatch(result, @"^[a-zA-Z ]+$"))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must contain only letters or spaces.");
                }
            }

            return result;
        }

        /// <summary>
        /// The read double method.
        /// </summary>
        /// <param name="prompt">The name of the value.</param>
        /// <returns>Returns a result of the selected value.</returns>
        public static double ReadDoubleValue(string prompt)
        {
            double result = 0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (double.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be either a whole number or a decimal number.");
                }
            }

            return result;
        }

        /// <summary>
        /// The read gender method.
        /// </summary>
        /// <returns>Returns a result of the selected value.</returns>
        public static Gender ReadGender()
        {
            Gender result = Gender.Female;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Gender");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found...
                if (Enum.TryParse<Gender>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid gender. Possible genders: " + GetTypes(typeof(Gender)));
                }
            }

            return result;
        }

        /// <summary>
        /// The read color method.
        /// </summary>
        /// <returns>Returns a result of the selected value.</returns>
        public static WalletColor ReadColor()
        {
            WalletColor result = WalletColor.Black;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Color");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found..
                if (Enum.TryParse<WalletColor>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid wallet color. Possible wallet colors: " + GetTypes(typeof(WalletColor)));
                }
            }

            return result;
        }

        /// <summary>
        /// The read int method.
        /// </summary>
        /// <param name="prompt">The name of the value.</param>
        /// <returns>Returns a result of the selected value.</returns>
        public static int ReadIntValue(string prompt)
        {
            int result = 0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (int.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be a whole number.");
                }
            }

            return result;
        }

        /// <summary>
        /// The read string method.
        /// </summary>
        /// <param name="prompt">The name of the value.</param>
        /// <returns>Returns a result of the selected value.</returns>
        public static string ReadStringValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                Console.Write(prompt + "] ");

                string stringValue = Console.ReadLine().ToLower().Trim();

                Console.WriteLine();

                if (stringValue != string.Empty)
                {
                    result = stringValue;
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must have a value.");
                }
            }

            return result;
        }

        /// <summary>
        /// Writes the help detail.
        /// </summary>
        /// <param name="command">The command for writing help.</param>
        /// <param name="overview">The overview of the command.</param>
        /// <param name="arguments">The arguments used for the command.</param>
        public static void WriteHelpDetail(string command, string overview, Dictionary<string, string> arguments)
        {
            string upperCommand = command.ToUpper();
            Console.WriteLine($"Command name: {upperCommand}");
            Console.WriteLine($"Overview: {overview}");
            if (arguments != null)
             {
                Console.WriteLine($"Usage: {upperCommand} {arguments.Keys.Flatten(" ")}");
                Console.WriteLine($"Parameters:");
                arguments.ToList().ForEach(kvp => Console.WriteLine($"{kvp.Key}: {kvp.Value}"));
                ////foreach (KeyValuePair<string, string> kvp in arguments)
                ////{
                ////    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                ////}
            }
        }

        /// <summary>
        /// Writes the help detail for console.
        /// </summary>
        /// <param name="command">The command used.</param>
        /// <param name="overview">The overview of the command.</param>
        /// <param name="argument">The arguments used for the command.</param>
        /// <param name="argumentUsage">The arguments usage.</param>
        public static void WriteHelpDetail(string command, string overview, string argument, string argumentUsage)
        {
            Dictionary<string, string> help = new Dictionary<string, string>();
            help.Add(argument, argumentUsage);
            WriteHelpDetail(command, overview, help);
        }

        /// <summary>
        /// Writes the help detail for console.
        /// </summary>
        /// <param name="command">The command used.</param>
        /// <param name="overview">The overview of the command.</param>
        public static void WriteHelpDetail(string command, string overview)
        {
            WriteHelpDetail(command, overview, null);
        }
    }
}
