using Calculator.Operators;

namespace Calculator.ParsedParts
{
    public class PartExpression
    {
        public PartExpression(IOperator @operator, double number)
        {
            Operator = @operator;
            Number = number;
        }

        public IOperator Operator { get; set; }

        public double Number { get; set; }
    }
}
