namespace MyCalculatore.Tests
{
    public class FractionTests
    {
        [Fact]
        public void FractionWithValidDataReturnsResult()
        {
            // Arrange
            IOperation<double> FractionObj = new Fraction<double>(9, 4);
            double excptedResult = 2.25;
            // Act
            double actualResult = FractionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
       
        [Fact]
        public void FractionWithMultiplicationOperationReturnsResult()
        {
            // Arrange
            IOperation<double> FractionObj = new Fraction<double>(new Multiplication<double>(3, 3), new Multiplication<double>(2,2));
            double excptedResult = 2.25;
            // Act
            double actualResult = FractionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void FractionWithDivisionOperationReturnsResult()
        {
            // Arrange
            IOperation<double> FractionObj = new Fraction<double>(new Division<double>(27,3), new Division<double>(12, 3));
            double excptedResult = 2.25;
            // Act
            double actualResult = FractionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void FractionWithFractionOperationReturnsResult()
        {
            // Arrange
            IOperation<double> FractionObj = new Fraction<double>(new Fraction<double>(9,4));
            double excptedResult = 2.25;
            // Act
            double actualResult = FractionObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }

        [Fact]
        public void FractionCheckPrintSentence()
        {
            // Arrange
            double operand = 5.2;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOperation<double> FractionObj = new Fraction<double>(5.2);
            var excptedResult = $"Fraction of {operand} is {5.2}";
            // Act
            var output = FractionObj.ToResult();
            FractionObj.PrintSentence();
            // Assert
            Assert.Equal(excptedResult, stringWriter.ToString().Trim());
        }
        [Fact]
        public void FractionCheckPrint()
        {
            // Arrange
            double operandLeft = 9;
            double operandRight = 4;

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOperation<double> FractionObj = new Fraction<double>(9,4);
            // Act
            var output = FractionObj.ToResult();
            FractionObj.Print();
            // Assert
            Assert.Equal($"({operandLeft} / {operandRight}) =2.25", stringWriter.ToString().Trim());
        }
        [Fact]
        public void CheckFractionWithInvalidData()
        {
            // Arrange
            IOperation<int> FractionObj = new Fraction<int>(9, 4);
            // Act
            var output = Record.Exception(() => FractionObj.ToResult());
            // Assert
            Assert.True(FractionObj.IsError);
        }
    }
}