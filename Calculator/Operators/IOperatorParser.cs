namespace Calculator.Operators
{
    public interface IOperatorParser
    {
        IOperator ParseOperator(string rawOperator);
    }
}
