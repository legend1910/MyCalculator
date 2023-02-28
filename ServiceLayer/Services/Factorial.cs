using Serilog;
using System.Text;

namespace MyCalculator.Services
{
    public class Factorial<T> : IOperation<T> where T : IComparable<T>
    {
        private T result;
        private StringBuilder _sentence;
        private StringBuilder _description;
        private object[] _operands;
        private bool _isError;
        public string Description
        {
            get => $"({_description.ToString()}!)";
        }

        public string Sentence
        {
            get => $"Faculty of {_sentence.ToString()}";
        }
        public bool IsError { get => _isError; }
        public Factorial(params object[] operands)
        {
            if (typeof(T) == typeof(char))
            {
                throw new InvalidOperationException("Invalid type argument: char");
            }
            if (operands.Length > 1)
            {
                throw new InvalidOperationException("Factorial Accepts only one parameter");
            }
            _operands = operands;
            _description = new StringBuilder();
            _sentence = new StringBuilder();
        }
        /// <summary>
        ///  Does the Factorial operation for the parameters passed
        /// Accepts only one parameter any numerical value or any operation
        ///  Suppressing the exception to return the error in print and it can be 
        /// </summary>
        /// <returns></returns>
        public T ToResult()
        {
            var logger = Log.ForContext<Factorial<T>>();
            try
            {
                dynamic operand = _operands.First();
                if (operand?.GetType()?.BaseType == typeof(object))
                {

                    var value = operand.ToResult();
                    _description.Append($"{operand.Description}");
                    _sentence.Append($"{operand.Sentence}");
                    if (operand.IsError || value == null)
                        throw new Exception("Error");
                    operand = value;
                    result = FactorialOperation(operand);
                }
                else
                {
                    result = FactorialOperation(operand);
                    _description.Append(operand);
                    _sentence.Append(operand);
                }

                return result;
            }
            catch (Exception ex)
            {
                _isError = true;
                logger.Error($"Subtraction - ToResult: Error While Subtraction  - {ex.Message}");
            }
            return default;
        }
        private T FactorialOperation(T n)
        {

            dynamic operandResult = n;
            dynamic result = 1;
            for (dynamic i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
        public void Print()
        {
            string message = _isError ? "Resulted in Error" : $"{result}";
            Console.WriteLine($"{Description} ={message}");
        }

        public void PrintSentence()
        {
            string message = _isError ? "Resulted in Error" : $"{result}";
            Console.WriteLine($"{Sentence} is {result}");
        }


    }
}