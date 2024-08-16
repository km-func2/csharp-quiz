using System;

namespace CalculatorApp
{
    public class Calculator
    {
        // You gotta THROW errors in classes. and CATCH in Program.
        public double PerformOperation(double num1, double num2, string operation)
        {
            switch (operation.ToLower())
            {
                case "add":
                    return num1 + num2;
                case "subtract":
                    return num1 - num2;
                case "multiply":
                    return num1 * num2;
                case "divide":
                    if (num2 == 0)
                    {
                        throw new DivideByZeroException("Cannot divide by zero.");
                    }
                    if (num1 == 0)
                    {
                        throw new DividendIsZeroException("Result is undefined.");
                    }
                    return num1 / num2;
                default:
                    throw new InvalidOperationException("An error occurred: The specified operation is not supported.");
            }
        }

        public class DividendIsZeroException : Exception
        {
            public DividendIsZeroException(string message) : base(message)
            {
            }
        }
    }
}