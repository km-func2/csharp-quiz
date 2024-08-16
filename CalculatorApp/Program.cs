using System;
using static CalculatorApp.Calculator;
using Serilog;

namespace CalculatorApp
{
    class Program
    {
       
        static void Main(string[] args)
        {
            // V2 TO DO: Put this in its own object/file.
            using var logger = new LoggerConfiguration()
               // add console as logging target
               .WriteTo.Console()
               // add debug output as logging target
               .WriteTo.Debug()
               // set minimum level to log
               .MinimumLevel.Debug()
               .CreateLogger();


            try
            {

                

                var _inputProvider = new InputProvider();

                double num1 = _inputProvider.GetNumber("Enter the first number:");
                double num2 = _inputProvider.GetNumber("Enter the second number:");

                Console.WriteLine("Enter the operation (add, subtract, multiply, divide):");
                string operation = Console.ReadLine()?.ToLower() ?? string.Empty;

                double result = PerformOperation(num1, num2, operation);

                Console.WriteLine($"The result is: {result}");
            }
            // as long as the error is thrown, there can just be only one exception basta descriptive yung throw error message.
            catch(InvalidOperationException ex)
            {
                logger.Fatal($"Fatal: {ex.Message}");
            }
            catch (InvalidInputException ex)
            {
                logger.Fatal("Fatal error: ", ex.Message);
            }

            catch (DivideByZeroException ex)
            {
                logger.Warning($"Warning: {ex.Message}");

            }
            catch (DividendIsZeroException ex)
            {
                logger.Warning($"Warning: {ex.Message}");
            }
            catch (Exception ex)
            {
                logger.Debug($"An unexpected error has occured: {ex.Message}");
                
            }
            finally
            {
                Console.WriteLine("Calculation attempt finished.");
            }
        }


        public static double PerformOperation(double num1, double num2, string operation)
        {
            var calculator = new Calculator();
            return calculator.PerformOperation(num1, num2, operation);
        }
    }

    public class InputProvider
    {
        public virtual double GetNumber(string prompt)
        {
            double number;
            while (true)
            {
                Console.WriteLine(prompt);
                if (double.TryParse(Console.ReadLine(), out number))
                {
                    return number;
                }
                else
                {
                    throw new InvalidInputException("Invalid input. Please enter a numeric value.");
                }
            }
        }
    }

    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        {
        }
    }

}