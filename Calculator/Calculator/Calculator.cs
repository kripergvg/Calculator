using System.Collections.Generic;
using System.Linq;
using Calculator.ExpressionFormatter;
using Calculator.ExpressionsParser;
using Calculator.Operators;
using CSharpFunctionalExtensions;

namespace Calculator.Calculator
{
    public class Calculator : ICalculator
    {
        private readonly IExpressionsParser _expressionsParser;
        private readonly IExpressionFormatter _expressionFormatter;

        public Calculator(
            IExpressionsParser expressionsParser,
            IExpressionFormatter expressionFormatter)
        {
            _expressionsParser = expressionsParser;
            _expressionFormatter = expressionFormatter;
        }

        public Result<double> Calculate(string expression)
        {
            var formattedExpression = _expressionFormatter.Format(expression);
            var parsedExpressionResult = _expressionsParser.Parse(formattedExpression);
            if (parsedExpressionResult.IsFailure)
            {
                return Result.Fail<double>(parsedExpressionResult.Error);
            }

            var parsedExpression = parsedExpressionResult.Value;
            if (!parsedExpression.PartExpressions.Any())
            {
                return Result.Fail<double>("There are should be at least one expression");
            }

            return Calculate(parsedExpression);
        }

        private Result<double> Calculate(ParsedExpressions parsedExpression)
        {
            var operators = new Stack<IOperator>();
            var numbers = new Stack<double>();

            numbers.Push(parsedExpression.FirstNumber);

            var firstPartExpression = parsedExpression.PartExpressions.First();
            numbers.Push(firstPartExpression.Number);
            operators.Push(firstPartExpression.Operator);

            for (var i = 1; i < parsedExpression.PartExpressions.Count; i++)
            {
                var partExpression = parsedExpression.PartExpressions[i];
                if (operators.Any() && partExpression.Operator.Priority <= operators.Peek().Priority)
                {
                    var lastNumber = numbers.Pop();
                    var prevLastNumber = numbers.Pop();

                    // удалим оператор, который взяли через Peek()
                    var lastOperator = operators.Pop();

                    var valueResult = lastOperator.Execute(prevLastNumber, lastNumber)
                        .OnSuccess(r => partExpression.Operator.Execute(r, partExpression.Number));

                    if (valueResult.IsSuccess)
                    {
                        numbers.Push(valueResult.Value);
                    }
                    else
                    {
                        return valueResult;
                    }
                }
                else
                {
                    operators.Push(partExpression.Operator);
                    numbers.Push(partExpression.Number);
                }
            }

            // если остались числа, то досчитаем их
            while (numbers.Count > 1)
            {
                var lastNumber = numbers.Pop();
                var prevLastNumber = numbers.Pop();

                var @operator = operators.Pop();

                var result = @operator.Execute(prevLastNumber, lastNumber)
                      .OnSuccess(r => numbers.Push(r));

                if (result.IsFailure)
                {
                    return result;
                }
            }

            return Result.Ok(numbers.Pop());
        }
    }
}
