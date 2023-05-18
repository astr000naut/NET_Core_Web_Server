using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase(1, 2, 3)]
        [TestCase(2, 3, 5)]
        [TestCase(2, 2, 4)]
        [TestCase(int.MaxValue, 3, (long)int.MaxValue + 3)]
        public void Add_ValidInput_ReturnSuccess(int a, int b, long expectedResult)
        {
            // Arrange
            
            // Act 

            var calculator = new Calculator();
            var actualResult = calculator.Add(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(1, 2, -1)]
        [TestCase(5, 4, 1)]
        [TestCase(int.MaxValue, 3, (long)int.MaxValue - 3)]
        [TestCase(int.MaxValue, int.MaxValue, 0)]
        public void Sub_ValidInput_ReturnSuccess(int a, int b, long expectedResult)
        {
            // Arrange

            // Act 

            var calculator = new Calculator();
            var actualResult = calculator.Sub(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(1, 2, 2)]
        [TestCase(5, 4, 20)]
        [TestCase(int.MaxValue, 3, (long)int.MaxValue * 3)]
        [TestCase(int.MaxValue, int.MaxValue, (long) int.MaxValue * int.MaxValue)]
        public void Mul_ValidInput_ReturnSuccess(int a, int b, long expectedResult)
        {
            // Arrange

            // Act 

            var calculator = new Calculator();
            var actualResult = calculator.Mul(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(1, 2, (double)1 / 2)]
        [TestCase(int.MaxValue, int.MaxValue, 1)]
        public void Div_ValidInput_ReturnSuccess(int a, int b, double expectedResult)
        {
            // Arrange

            // Act 

            var calculator = new Calculator();
            var actualResult = calculator.Div(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(1, 0, "Không chia được cho 0")]
        public void Div_ZeroDivide_ReturnException(int a, int b, string expectedResult)
        {
            // Arrange

            // Act 

            var calculator = new Calculator();
            var ex = Assert.Throws<Exception>(() => calculator.Div(a, b));

            // Assert
            StringAssert.Contains(expectedResult, ex.Message.ToString());
        }
    }
}
