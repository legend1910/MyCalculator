namespace MyCalculatore.Tests
{
    public class FactorialTests
    {
        [Fact]
        public void FactorialWithIntegerReturnsResult()
        {
            // Arrange
            IOperation<int> FactorialObj = new Factorial<int>(5);
            int excptedResult = 120;
            // Act
            int actualResult = FactorialObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void FactorialWithDoubleReturnsResult()
        {
            // Arrange
            IOperation<double> FactorialObj = new Factorial<double>(5.1);
            double excptedResult = 120;
            // Act
            double actualResult = FactorialObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void FactorialWithMultiplicationOperationReturnsResult()
        {
            // Arrange
            IOperation<int> FactorialObj = new Factorial<int>(new Multiplication<int>(5, 1));
            int excptedResult = 120;
            // Act
            int actualResult = FactorialObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void FactorialWithDivisionOperationReturnsResult()
        {
            // Arrange
            IOperation<int> FactorialObj = new Factorial<int>(new Division<int>(5, 1));
            int excptedResult = 120;
            // Act
            int actualResult = FactorialObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void FactorialWithFactorialOperationReturnsResult()
        {
            // Arrange
            IOperation<int> FactorialObj = new Factorial<int>(new Factorial<int>(3));
            int excptedResult = 720;
            // Act
            int actualResult = FactorialObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }

        [Fact]
        public void CheckPrintSentence()
        {
            // Arrange
            double operand = 5.2;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOperation<double> FactorialObj = new Factorial<double>(5.2);
            var excptedResult = $"Faculty of {operand} is {120}";
            // Act
            var output = FactorialObj.ToResult();
            FactorialObj.PrintSentence();
            // Assert
            Assert.Equal(excptedResult, stringWriter.ToString().Trim());
        }
        [Fact]
        public void CheckPrint()
        {
            // Arrange
            double operand = 5.2;

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOperation<double> FactorialObj = new Factorial<double>(5.2);
            // Act
            var output = FactorialObj.ToResult();
            FactorialObj.Print();
            // Assert
            Assert.Equal($"({operand}!) ={120}", stringWriter.ToString().Trim());
        }
        [Fact]
        public void CheckFactorialWithInvalidData()
        {
            // Arrange
            IOperation<double> FactorialObj;
            // Act
            var output = Record.Exception(() => FactorialObj = new Factorial<double>(5.2, new Division<double>(1, 0)));
            // Assert
            Assert.NotNull(output);
        }
    }
}