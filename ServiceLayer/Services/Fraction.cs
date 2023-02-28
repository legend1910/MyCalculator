using Serilog;
using System.Text;

namespace MyCalculator.Services
{
    public class Fraction<T> : IOperation<T> where T : IComparable<T>
    {
        private Double result;
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
            get => $"Fraction of {_sentence.ToString()}";
        }
        public bool IsError { get => _isError; }
        public Fraction(params object[] operands)
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
        /// Returns the fraction for the paramters passed
        /// Note: Only Double should be passed as T
        /// </summary>
        /// <returns></returns>
        public T ToResult()
        {
            var logger = Log.ForContext<Fraction<T>>();
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
                    result = (result == default(double)) ? operandResult : result / operandResult;
                }

                return (dynamic)result;
            }
            catch (Exception ex)
            {
                _isError = true;
                logger.Error($"Fractions - ToResult: Error While calculating Fractions  - {ex.Message}");
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