using Calculator.Operators;

namespace Calculator.ExpressionsParser
{
    public class ParsedExpression
    {
        public ParsedExpression(IOperator @operator, double number)
        {
            Operator = @operator;
            Number = number;
        }

        public IOperator Operator { get; set; }

        public double Number { get; set; }
    }
}
