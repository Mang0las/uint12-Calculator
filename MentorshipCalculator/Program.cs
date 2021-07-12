using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCalculator
{
    class Program
    {
        /// <summary>
        /// Console input and validation logic
        /// </summary>
        static void Main(string[] args)
        {
            var calc = new Calculator();

            string input;

            while (true)
            {
                Console.Write(">> ");
                input = Console.ReadLine();

                string[] inputValues = input.Split(' ');

                int inputInt = 0;

                if (inputValues.Length == 2)
                {
                    if (!int.TryParse(inputValues[1], out inputInt) || inputValues[0] != "Put")
                    {
                        Console.WriteLine("Invalid input. Try again.");
                        continue;
                    }

                }
                try
                {
                    Calculate(inputValues[0], inputInt, calc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        /// <summary>
        /// Processes input commands: Put <value>, Remove, Add, Sub
        /// </summary>
        /// <param name="op">command name</param>
        /// <param name="intToEnqueue"><value> to Put, if applicable</param>
        /// <param name="calc">Calculator object, needs to be a parameter for queue persistence</param>
        public static void Calculate(string op, int intToEnqueue, Calculator calc)
        {
            switch (op)
            {
                case "Put":
                    calc.Put(intToEnqueue);
                    break;
                case "Remove":
                    calc.Remove();
                    break;
                case "Add":
                    Console.WriteLine($">> {calc.Add()}");
                    break;
                case "Sub":
                    Console.WriteLine($">> {calc.Sub()}");
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            }
        }
    }
}

