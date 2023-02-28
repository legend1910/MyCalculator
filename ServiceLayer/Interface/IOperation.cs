using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Services
{
    public interface IOperation<T>
    {
        string Description { get; }
        string Sentence { get; }
        bool IsError { get; }
        T ToResult();
        void Print();
        void PrintSentence();
    }
}
