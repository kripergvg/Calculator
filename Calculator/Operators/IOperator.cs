using CSharpFunctionalExtensions;

namespace Calculator.Operators
{
    public interface IOperator
    {
        Result<double> Execute(double firstValue, double secondValue);

        bool Validate(string operatorValue);

        int Priority { get; }
    }
}
