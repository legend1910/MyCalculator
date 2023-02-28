using Serilog;
using System.Text;

namespace MyCalculator.Services
{
    public class Addition<T> : IOperation<T>
    {
        private T _result;
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
            get => $"Sum of {_sentence.ToString()}";
        }
        public bool IsError { get => _isError; }
        public Addition(params object[] operands)
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
        ///  Does the Addition operation for the parameters passed
        ///  if it's numeric type does the normal Sum Operation
        ///  if it's Another operation gets the resul of the operation 
        ///  if the operation throws excetion enitre SUM is results in Error
        ///  Suppressing the exception to return the error in print and it can be 
        ///  logged through logger
        /// </summary>
        /// <returns></returns>
        public T ToResult()
        {
            var logger = Log.ForContext<Addition<T>>();
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
                        _description.Append($" + ");
                        _sentence.Append($",");
                    }
                    _result += operandResult;
                }

                return _result;
            }
            catch (OverflowException ex)
            {
                _isError = true;
                _result = default(T);
                logger.Error($"Addition - ToResult: Overflow Error While Adding  - {ex.Message}");
            }
            catch (Exception ex)
            {
                _isError = true;
                _result = default(T);
                logger.Error($"Addition - ToResult: Error While Adding  - {ex.Message}");
            }
            return _result;
        }
        public void Print()
        {
            string message = _isError ? "Resulted in Error" : $"{_result}";
            Console.WriteLine($"{Description} ={message}");
        }
        public void PrintSentence()
        {
            string message = _isError ? "Resulted in Error" : $"{_result}";
            Console.WriteLine($"{Sentence} is {_result}");
        }
    }
}
