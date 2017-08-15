namespace Calculator.Operators
{
    public interface IOperator
    {
        double Execute(double firstValue, double secondValue);

        bool Validate(string operatorValue);

        int Priority { get; }
    }
}
