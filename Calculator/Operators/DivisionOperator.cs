using CSharpFunctionalExtensions;

namespace Calculator.Operators
{
    public class DivisionOperator : IOperator
    {
        public Result<double> Execute(double firstValue, double secondValue)
        {
            if (secondValue == 0)
            {
                return Result.Fail<double>("You cant divide by zero");
            }

            return Result.Ok(firstValue / secondValue);
        }

        public bool Validate(string operatorValue)
        {
            return operatorValue == "/";
        }

        public int Priority { get; } = 2;
    }
}
