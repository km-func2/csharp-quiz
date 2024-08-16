using NUnit.Framework;
using CalculatorApp;
using Moq;
using static CalculatorApp.Calculator;

namespace CalculatorApp.UnitTests
{
    [TestFixture]
    public class CalculatorTest
    {
        private Calculator _calculator;
        private Mock<InputProvider> _mockInputProvider;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
            _mockInputProvider = new Mock<InputProvider>();
        }

        [Test]
        [TestCase(5, 3, "add", 8)]
        [TestCase(5, 3, "subtract", 2)]
        [TestCase(5, 3, "multiply", 15)]
        [TestCase(6, 3, "divide", 2)]
        public void PerformOperation_ValidOperations_ReturnsExpectedResult(double num1, double num2, string operation, double expected)
        {
            // Act
            double result = _calculator.PerformOperation(num1, num2, operation);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(5, 0, "divide")]
        public void PerformOperation_DivideByZero_ThrowsDivideByZeroException(double num1, double num2, string operation)
        {
            // Act
            var ex = Assert.Throws< DivideByZeroException>(() => _calculator.PerformOperation(num1, num2, operation));
            // Assert
            Assert.AreEqual("Cannot divide by zero.", ex.Message);
        }

        [Test]
        [TestCase(0, 5, "divide")]
        public void PerformOperation_DivideByZero_ThrowsDividendIsZeroException(double num1, double num2, string operation)
        {
            // Act
            var ex = Assert.Throws<DividendIsZeroException>(() => _calculator.PerformOperation(num1, num2, operation));
            // Assert
            Assert.AreEqual("Result is undefined.", ex.Message);
        }


        // testing naming convention -- Perform (method name), Test case name (situation name), returns value (expected output or error)
        [Test]
        public void PerformOperation_InvalidOperation_ThrowsInvalidOperationException()
        {
            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => _calculator.PerformOperation(5, 3, "power"));

            // Assert
            Assert.AreEqual("An error occurred: The specified operation is not supported.", ex.Message);
        }

        [Test]
        public void GetNumber_InvalidInput_ThrowsInvalidInputException()
        {
            // Arrange
            _mockInputProvider.Setup(ip => ip.GetNumber(It.IsAny<string>())).Throws(new InvalidInputException("Invalid input. Please enter a numeric value."));

            // Act & Assert
            var ex = Assert.Throws<InvalidInputException>(() => _mockInputProvider.Object.GetNumber("Enter a number:"));
            Assert.AreEqual("Invalid input. Please enter a numeric value.", ex.Message);
        }


    }
}