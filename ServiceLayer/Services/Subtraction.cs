using Serilog;
using System.Text;

namespace MyCalculator.Services
{
    public class Subtraction<T> : IOperation<T>
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
            get => $"Subtraction of {_sentence.ToString()}";
        }
        public bool IsError { get => _isError; }
        public Subtraction(params object[] operands)
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
        ///  Does the Subtraction operation for the parameters passed
        ///  if it's numeric type does the normal Subtraction Operation
        ///  if it's Another operation gets the resul of the operation 
        ///  if the operation throws excetion enitre Subtraction will result in Error
        /// </summary>
        /// <returns></returns>
        public T ToResult()
        {
            var logger = Log.ForContext<Subtraction<T>>();
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
                        _description.Append($" - ");
                        _sentence.Append($",");
                    }
                    result = EqualityComparer<T>.Default.Equals(result, default(T)) ? operandResult : result - operandResult;

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