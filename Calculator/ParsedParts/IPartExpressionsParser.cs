using CSharpFunctionalExtensions;

namespace Calculator.ParsedParts
{
    public interface IPartExpressionsParser
    {
        Result<ParsedPartExpressions> Parse(string fullExpression);
    }
}
