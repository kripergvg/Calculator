namespace Calculator.Operators
{
    public class MultiplicationOperator : IOperator
    {
        public double Execute(double firstValue, double secondValue)
        {
            return firstValue * secondValue;
        }

        public bool Validate(string operatorValue)
        {
            return operatorValue == "*";
        }

        public int Priority { get; } = 2;
    }
}
