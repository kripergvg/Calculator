using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Calculator.Operators;
using CSharpFunctionalExtensions;

namespace Calculator.ExpressionsParser
{
    public class ExpressionsParser : IExpressionsParser
    {
        private readonly IOperatorParser _operatorParser;
        private static readonly Regex _expressionPartRegex = new Regex(@"(?<operator>[^\d]+)(?<number>-?\d+\.?\d*)", RegexOptions.Compiled);
        private static readonly Regex _numberRegex = new Regex(@"-?\d+\.?\d*", RegexOptions.Compiled);

        public ExpressionsParser(IOperatorParser operatorParser)
        {
            _operatorParser = operatorParser;
        }

        public Result<ParsedExpressions> Parse(string fullExpression)
        {
            var firstNumberResult = ParseFirstNumber(fullExpression);
            var partExpressions = ParseExpressions(fullExpression, firstNumberResult.Value.FirstNumberLength);

            if (firstNumberResult.IsSuccess && partExpressions.IsSuccess)
            {
                return Result.Ok(new ParsedExpressions(firstNumberResult.Value.FirstNumber, partExpressions.Value));
            }

            return Result.Fail<ParsedExpressions>(Result.Combine(firstNumberResult, partExpressions).Error);
        }

        private Result<ReadOnlyCollection<ParsedExpression>> ParseExpressions(string fullExpression, int startParseIndex)
        {
            var partExpressionMatches = _expressionPartRegex.Matches(fullExpression, startParseIndex);

            var partExpressions = new List<ParsedExpression>();
            foreach (Match partExpressionMatch in partExpressionMatches)
            {
                var rawOperator = partExpressionMatch.Groups["operator"].ToString();
                var rawNumber = partExpressionMatch.Groups["number"].ToString();

                if (!string.IsNullOrEmpty(rawOperator) && double.TryParse(rawNumber, out double parsedNumber))
                {
                    var parsedOperator = _operatorParser.ParseOperator(rawOperator);
                    if (parsedOperator == null)
                    {
                       return Result.Fail<ReadOnlyCollection<ParsedExpression>>("Operator is not valid");
                    }

                    partExpressions.Add(new ParsedExpression(parsedOperator, parsedNumber));
                }
                else
                {
                   return Result.Fail<ReadOnlyCollection<ParsedExpression>>("Expression is not correct");
                }
            }

            return Result.Ok(partExpressions.AsReadOnly());
        }

        private Result<(double FirstNumber, int FirstNumberLength)> ParseFirstNumber(string fullExpression)
        {
            var firstNumberMatch = _numberRegex.Match(fullExpression);
            if (firstNumberMatch.Success && double.TryParse(firstNumberMatch.Value, out double parsedFirstNumber))
            {
                return Result.Ok((parsedFirstNumber, firstNumberMatch.Length));
            }

            return Result.Fail<(double FirstNumber, int FirstNumberLength)>("Expression is not correct");
        }
    }
}