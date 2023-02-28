using Serilog;
using System.Text;

namespace MyCalculator.Services
{
    public class Multiplication<T> : IOperation<T>
    {
        private T result;
        private StringBuilder _sentence;
        private StringBuilder _description;
        private object[] _operands;
        private bool _isError;
        public string Description
        {
            get => $"({_description.ToString()})";
        }

        public string Sentence
        {
            get => $"Multiplication of {_sentence.ToString()}";
        }
        public bool IsError { get => _isError; }
        public Multiplication(params object[] operands)
        {
            if (typeof(T) == typeof(char))
            {
                throw new InvalidOperationException("Invalid type argument: char");
            }
            _operands = operands;
            _description = new StringBuilder();
            _sentence = new StringBuilder();
        }
        /// <summary>
        ///  Does the Multiplication operation for the parameters passed
        ///  if it's numeric type does the normal Multiplication Operation
        ///  if it's Another operation gets the result of the operation 
        ///  if the operation throws excetion enitre Multiplication will result in Error
        ///  Suppressing the exception to return the error in print and it can be 
        ///  logged through Seri Log
        /// </summary>
        /// <returns></returns>
        public T ToResult()
        {
            var logger = Log.ForContext<Multiplication<T>>();
            try
            {
                foreach (var operand in _operands)
                {
                    dynamic operandResult = operand;
                    if (operand.GetType()?.BaseType == typeof(object))
                    {
                        var value = operandResult.ToResult();
                        _description.Append($"{operandResult.Description}");
                        _sentence.Append($"{operandResult.Sentence}");
                        if (operandResult.IsError || value == null)
                            throw new Exception("Error");
                        operandResult = value;
                    }
                    else
                    {
                        _description.Append(operand);
                        _sentence.Append(operand);
                    }
                    if (operand != _operands.Last())
                    {
                        _description.Append($" * ");
                        _sentence.Append($",");
                    }
                    result = EqualityComparer<T>.Default.Equals(result, default(T)) ? operandResult : result * operandResult;

                }

                return result;
            }
            catch (OverflowException ex)
            {
                _isError = true;
                logger.Error($"Multiplication - ToResult: Error While Multiplication  - {ex.Message}");
            }
            catch (Exception ex)
            {
                _isError = true;
                logger.Error($"Multiplication - ToResult: Error While Multiplication  - {ex.Message}");
            }
            return default;
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