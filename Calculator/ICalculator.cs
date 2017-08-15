using CSharpFunctionalExtensions;

namespace Calculator
{
    public interface ICalculator
    {
        Result<double> Calculate(string expression);
    }
}
