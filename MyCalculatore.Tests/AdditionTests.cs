namespace MyCalculatore.Tests
{
    public class AdditionTests
    {
        [Fact]
        public void AdditionWithIntegerReturnsResult()
        {
            // Arrange
            IOperation<int> additionObj = new Addition<int>(5, 1, 3);
            int excptedResult = (5 + 1 + 3);
            // Act
            int actualResult = additionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void AdditionWithDoubleReturnsResult()
        {
            // Arrange
            IOperation<double> additionObj = new Addition<double>(5.1, 1.3, 3.2);
            double excptedResult = (5.1 + 1.3 + 3.2);
            // Act
            double actualResult = additionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void AdditionWithMultiplicationOperationReturnsResult()
        {
            // Arrange
            IOperation<int> additionObj = new Addition<int>(new Multiplication<int>(2, 3), new Multiplication<int>(3, 3));
            int excptedResult = ((2 * 3) + (3 * 3));
            // Act
            int actualResult = additionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void AdditionWithDivisionOperationReturnsResult()
        {
            // Arrange
            IOperation<int> additionObj = new Addition<int>(new Division<int>(6, 3), new Division<int>(15, 3));
            int excptedResult = ((6 / 3) + (15 / 3));
            // Act
            int actualResult = additionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void AdditionWithFactorialOperationReturnsResult()
        {
            // Arrange
            IOperation<int> additionObj = new Addition<int>(new Factorial<int>(4), new Factorial<int>(4));
            int excptedResult = (24 + 24);
            // Act
            int actualResult = additionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void AdditionWithMultipleOperationReturnsResult()
        {
            // Arrange
            IOperation<int> additionObj = new Addition<int>(new Addition<int>(4, 6),
                                            new Subtraction<int>(4, 2), new Multiplication<int>(4, 4));
            int excptedResult = ((4 + 6) + (4 - 2) + (4 * 4));
            // Act
            int actualResult = additionObj.ToResult();
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
            IOperation<double> additionObj = new Addition<double>(5.2, 1.5);
            var excptedResult = $"Sum of {operandLeft},{operandRight} is {operandLeft+operandRight}";
            // Act
            var output = additionObj.ToResult();
            additionObj.PrintSentence();
            // Assert
            Assert.Equal(excptedResult, stringWriter.ToString().Trim());
        }
        [Fact]
        public void CheckPrint()
        {
            // Arrange
            double operandLeft = 5.2;
            double operandRight = 1.5;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOperation<double> additionObj = new Addition<double>(5.2, 1.5);
            // Act
            var output = additionObj.ToResult();
            additionObj.Print();
            // Assert
            Assert.Equal($"({operandLeft} + {operandRight}) ={operandLeft+operandRight}", stringWriter.ToString().Trim());
        }
        [Fact]
        public void CheckAdditionPrintWithInvalidData()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOperation<double> additionObj = new Addition<double>(5.2, new Division<double>(1, 0));
            // Act
            var output = additionObj.ToResult();
            additionObj.Print();
            // Assert
            Assert.Equal("(5.2 + (1 / 0)) =Resulted in Error", stringWriter.ToString().Trim());
        }
    }
}