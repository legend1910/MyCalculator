using MyCalculator.Services;
using Serilog;

namespace SumCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .WriteTo.Console()
                            .CreateLogger();

            Addition<double> Addition1 = new Addition<double>(5.2, new Division<double>(1, 0));
            double result1 = Addition1.ToResult(); // result1 should be 6.7
            Console.WriteLine(result1);
            Addition1.Print();
            Addition1.PrintSentence();
            //Addition<int> Addition2 = new Addition<int>(5, 1);
            //int result2 = Addition2.ToResult(); 
            //Console.WriteLine(result2);
            //Addition2.Print();
            //Addition2.PrintSentence();
            //Addition<int> Addition3 = new Addition<int>(6, new Addition<int>(4, 2));
            //int result3 = Addition3.ToResult(); // result3 should be 12
            //Console.WriteLine(result3);
            //Addition3.Print();
            //Addition3.PrintSentence();
            //Addition<double> Addition4 = new Addition<double>(6, new Addition<double>(4.1, 2.2));
            //double result4 = Addition4.ToResult(); // result4 should be 12.3
            //Console.WriteLine(result4);
            //Addition4.Print();
            //Addition4.PrintSentence();
            //Addition<int> Addition5 = new Addition<int>(1, 2, 3, 4, 5);
            //int result5 = Addition5.ToResult(); // result5 should be 15
            //Console.WriteLine(result5);
            //Addition5.Print();
            //Addition5.PrintSentence();
            //Addition<int> Addition6 = new Addition<int>(new Addition<int>(5, 1), new Addition<int>(4, 2), new Addition<int>(6, 1), new Addition<int>(4, 1));
            //int result6 = Addition6.ToResult(); // result6 should be 24
            //Console.WriteLine(result6);
            //Addition6.Print();
            //Addition6.PrintSentence();
            //Addition<int> Addition7 = new Addition<int>(new Addition<int>(5, 1), new Addition<int>(4, 2));
            //int result7 = Addition7.ToResult();
            //Addition7.Print();
            //Addition7.PrintSentence();
            //Console.WriteLine(result7);
        }
    }
}