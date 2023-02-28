namespace MyCalculatore.Tests
{
    public class SubtractionTests
    {
        [Fact]
        public void SubtractionWithIntegerReturnsResult()
        {
            // Arrange
            IOperation<int> SubtractionObj = new Subtraction<int>(5, 1, 3);
            int excptedResult = (5 - 1 - 3);
            // Act
            int actualResult = SubtractionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void SubtractionWithDoubleReturnsResult()
        {
            // Arrange
            IOperation<double> SubtractionObj = new Subtraction<double>(5.1, 1.3, 3.2);
            double excptedResult = (5.1 - 1.3 - 3.2);
            // Act
            double actualResult = SubtractionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void SubtractionWithMultiplicationOperationReturnsResult()
        {
            // Arrange
            IOperation<int> SubtractionObj = new Subtraction<int>(new Multiplication<int>(2, 3), new Multiplication<int>(3, 3));
            int excptedResult = ((2 * 3) - (3 * 3));
            // Act
            int actualResult = SubtractionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void SubtractionWithDivisionOperationReturnsResult()
        {
            // Arrange
            IOperation<int> SubtractionObj = new Subtraction<int>(new Division<int>(6, 3), new Division<int>(15, 3));
            int excptedResult = ((6 / 3) - (15 / 3));
            // Act
            int actualResult = SubtractionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void SubtractionWithFactorialOperationReturnsResult()
        {
            // Arrange
            IOperation<int> SubtractionObj = new Subtraction<int>(new Factorial<int>(4), new Factorial<int>(4));
            int excptedResult = (24 - 24);
            // Act
            int actualResult = SubtractionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void SubtractionWithMultipleOperationReturnsResult()
        {
            // Arrange
            IOperation<int> SubtractionObj = new Subtraction<int>(new Subtraction<int>(4, 6),
                                            new Subtraction<int>(4, 2), new Multiplication<int>(4, 4));
            int excptedResult = ((4 - 6) - (4 - 2) - (4 * 4));
            // Act
            int actualResult = SubtractionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void CheckPrintSentence()
        {
            // Arrange
            double operandLeft = 5.2;
            double operandRight = 1.5;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOperation<double> SubtractionObj = new Subtraction<double>(5.2, 1.5);
            var excptedResult = $"Subtraction of {operandLeft},{operandRight} is {operandLeft - operandRight}";
            // Act
            var output = SubtractionObj.ToResult();
            SubtractionObj.PrintSentence();
            // Assert
            Assert.Equal(excptedResult, stringWriter.ToString().Trim());
        }
        [Fact]
        public void CheckPrint()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOperation<double> SubtractionObj = new Subtraction<double>(5.2, 1.5);
            // Act
            var output = SubtractionObj.ToResult();
            SubtractionObj.Print();
            // Assert
            Assert.Equal("(5.2 - 1.5) =3.7", stringWriter.ToString().Trim());
        }
        [Fact]
        public void CheckSubtractionPrintWithInvalidData()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOperation<double> SubtractionObj = new Subtraction<double>(5.2, new Division<double>(1, 0));
            // Act
            var output = SubtractionObj.ToResult();
            SubtractionObj.Print();
            // Assert
            Assert.Equal("(5.2 - (1 / 0)) =Resulted in Error", stringWriter.ToString().Trim());
        }
    }
}
