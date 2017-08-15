using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Calculator.Operators;
using CSharpFunctionalExtensions;

namespace Calculator.ParsedParts
{
    public class PartExpressionsParser : IPartExpressionsParser
    {
        private readonly IOperatorParser _operatorParser;
        private static readonly Regex _expressionPartRegex = new Regex(@"(?<operator>[^\d]+)(?<number>-?\d+\.?\d*)", RegexOptions.Compiled);

        public PartExpressionsParser(
            IOperatorParser operatorParser)
        {
            _operatorParser = operatorParser;
        }

        public Result<ParsedPartExpressions> Parse(string fullExpression)
        {
            Result<double> firstNumber = ParseFirstNumber(fullExpression);
            Result<ReadOnlyCollection<PartExpression>> partExpressions = ParsePartExpressions(fullExpression);

            if (firstNumber.IsSuccess && partExpressions.IsSuccess)
            {
                return Result.Ok(new ParsedPartExpressions(firstNumber.Value, partExpressions.Value));
            }

            return Result.Fail<ParsedPartExpressions>(Result.Combine(firstNumber, partExpressions).Error);
        }

        private Result<ReadOnlyCollection<PartExpression>> ParsePartExpressions(string fullExpression)
        {
            var partExpressionMatches = _expressionPartRegex.Matches(fullExpression);

            var partExpressions = new List<PartExpression>();
            foreach (Match partExpressionMatch in partExpressionMatches)
            {
                var rawOperator = partExpressionMatch.Groups["operator"].ToString();
                var rawNumber = partExpressionMatch.Groups["number"].ToString();

                if (!string.IsNullOrEmpty(rawOperator) && double.TryParse(rawNumber, out double parsedNumber))
                {
                    var parsedOperator = _operatorParser.ParseOperator(rawOperator);
                    partExpressions.Add(new PartExpression(parsedOperator, parsedNumber));
                }
                else
                {
                    Result.Fail<ReadOnlyCollection<PartExpression>>("Expression is not correct");
                }

                // TODO обработка ошибок
            }

            return Result.Ok(partExpressions.AsReadOnly());
        }

        private Result<double> ParseFirstNumber(string fullExpression)
        {
            var rawFirstNumber = new String(fullExpression.TakeWhile(s => Char.IsDigit(s) || s == '.').ToArray());
            if (double.TryParse(rawFirstNumber, out double parsedFirstNumber))
            {
                return Result.Ok(parsedFirstNumber);
            }

            return Result.Fail<double>("Expression is not correct");
        }
    }
}
