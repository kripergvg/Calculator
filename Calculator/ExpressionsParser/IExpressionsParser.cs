using CSharpFunctionalExtensions;

namespace Calculator.ExpressionsParser
{
    public interface IExpressionsParser
    {
        Result<ParsedExpressions> Parse(string fullExpression);
    }
}
