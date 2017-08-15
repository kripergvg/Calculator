namespace Calculator.Operators
{
    public class AdditionOperator : IOperator
    {
        public double Execute(double firstValue, double secondValue)
        {
            return firstValue + secondValue;
        }

        public bool Validate(string operatorValue)
        {
            return operatorValue == "+";
        }

        public int Priority { get; } = 1;
    }
}
