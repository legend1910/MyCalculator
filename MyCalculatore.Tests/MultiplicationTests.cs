using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculatore.Tests
{
    public class MultiplicationTests
    {
        [Fact]
        public void MultiplicationWithIntegerReturnsResult()
        {
            // Arrange
            IOperation<int> MultiplicationObj = new Multiplication<int>(5, 1, 3);
            int excptedResult = (5 * 1 * 3);
            // Act
            int actualResult = MultiplicationObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void MultiplicationWithDoubleReturnsResult()
        {
            // Arrange
            IOperation<double> MultiplicationObj = new Multiplication<double>(5.1, 1.3, 3.2);
            double excptedResult = (5.1 * 1.3 * 3.2);
            // Act
            double actualResult = MultiplicationObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void MultiplicationWithMultiplicationOperationReturnsResult()
        {
            // Arrange
            IOperation<int> MultiplicationObj = new Multiplication<int>(new Multiplication<int>(2, 3), new Multiplication<int>(3, 3));
            int excptedResult = ((2 * 3) * (3 * 3));
            // Act
            int actualResult = MultiplicationObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void MultiplicationWithDivisionOperationReturnsResult()
        {
            // Arrange
            IOperation<int> MultiplicationObj = new Multiplication<int>(new Division<int>(6, 3), new Division<int>(15, 3));
            int excptedResult = ((6 / 3) * (15 / 3));
            // Act
            int actualResult = MultiplicationObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void MultiplicationWithFactorialOperationReturnsResult()
        {
            // Arrange
            IOperation<int> MultiplicationObj = new Multiplication<int>(new Factorial<int>(4), new Factorial<int>(4));
            int excptedResult = (24 * 24);
            // Act
            int actualResult = MultiplicationObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void MultiplicationWithMultipleOperationReturnsResult()
        {
            // Arrange
            IOperation<int> MultiplicationObj = new Multiplication<int>(new Multiplication<int>(4, 6),
                                            new Subtraction<int>(4, 2), new Multiplication<int>(4, 4));
            int excptedResult = ((4 * 6) * (4 - 2) * (4 * 4));
            // Act
            int actualResult = MultiplicationObj.ToResult();
            // Assert
            Assert.Equal(excptedResult, actualResult);
        }
        [Fact]
        public void CheckMultiplicationPrintSentence()
        {
            // Arrange
            double operandLeft = 5.2;
            double operandRight = 1.5;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOperation<double> MultiplicationObj = new Multiplication<double>(5.2, 1.5);
            var excptedResult = $"Multiplication of {operandLeft},{operandRight} is {operandLeft * operandRight}";
            // Act
            var output = MultiplicationObj.ToResult();
            MultiplicationObj.PrintSentence();
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
            IOperation<double> MultiplicationObj = new Multiplication<double>(5.2, 1.5);
            // Act
            var output = MultiplicationObj.ToResult();
            MultiplicationObj.Print();
            // Assert
            Assert.Equal($"({operandLeft} * {operandRight}) ={operandLeft * operandRight}", stringWriter.ToString().Trim());
        }
        [Fact]
        public void CheckMultiplicationPrintWithInvalidData()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOperation<double> MultiplicationObj = new Multiplication<double>(5.2, new Division<double>(1, 0));
            // Act
            var output = MultiplicationObj.ToResult();
            MultiplicationObj.Print();
            // Assert
            Assert.Equal("(5.2 * (1 / 0)) =Resulted in Error", stringWriter.ToString().Trim());
        }
    }
}
