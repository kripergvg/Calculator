using CSharpFunctionalExtensions;

namespace Calculator.Calculator
{
    public interface ICalculator
    {
        Result<double> Calculate(string expression);
    }
}
