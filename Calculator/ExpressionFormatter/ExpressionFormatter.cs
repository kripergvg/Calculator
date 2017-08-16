namespace Calculator.ExpressionFormatter
{
    public class ExpressionFormatter : IExpressionFormatter
    {
        public string Format(string rawExpression)
        {
            return rawExpression.Replace(" ", string.Empty);
        }
    }
}
