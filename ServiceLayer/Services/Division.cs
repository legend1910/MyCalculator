using Serilog;
using System.Text;

namespace MyCalculator.Services
{
    public class Division<T> : IOperation<T>
    {
        private T _result;
        private StringBuilder _sentence;
        private StringBuilder _description;
        private object[] _operands;
        private bool _isError;
        private double _positiveInfinity = double.PositiveInfinity;
        public string Description
        {
            get => $"({_description.ToString()})";
        }
        public string Sentence
        {
            get => $"Division of {_sentence.ToString()}";
        }
        public bool IsError { get => _isError; }
        public Division(params object[] operands)
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
        ///Can Accept any number of parameters and retuns the division
        ///if the operation ends with infinity or exception the method returns 0 and Error
        /// </summary>
        /// <returns></returns>
        public T ToResult()
        {
            var logger = Log.ForContext<Division<T>>();
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
                        _description.Append($" / ");
                        _sentence.Append($",");
                    }
                    _result = EqualityComparer<T>.Default.Equals(_result, default(T)) ? operandResult : _result / operandResult;
                    if (_result == (dynamic)_positiveInfinity)
                        throw new DivideByZeroException();
                }

                return _result;
            }
            catch (DivideByZeroException ex)
            {
                _isError = true;
                _result = default(T);
                logger.Error($"Division - ToResult: DivideByZeroException Error While Division  - {ex.Message}");
            }
            catch (Exception ex)
            {
                _isError = true;
                _result = default(T);
                logger.Error($"Division - ToResult: Error While Division  - {ex.Message}");
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
