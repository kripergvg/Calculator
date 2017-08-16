using CSharpFunctionalExtensions;

namespace Calculator.Operators
{
    public class SubtractionOperator : IOperator
    {
        public Result<double> Execute(double firstValue, double secondValue)
        {
            return Result.Ok(firstValue - secondValue);
        }

        public bool Validate(string operatorValue)
        {
            return operatorValue == "-";
        }

        public int Priority { get; } = 1;
    }
}
