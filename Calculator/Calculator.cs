using System.Collections.Generic;
using System.Linq;
using Calculator.Operators;
using Calculator.ParsedParts;
using CSharpFunctionalExtensions;

namespace Calculator
{
    public class Calculator : ICalculator
    {
        private readonly IPartExpressionsParser _expressionsParser;
        private readonly IExpressionFormatter _expressionFormatter;

        public Calculator(
            IPartExpressionsParser expressionsParser,
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

            var number = Calculate(parsedExpression);
            return Result.Ok(number);
        }

        private double Calculate(ParsedPartExpressions parsedExpression)
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
                var lastOperator = operators.Peek();
                if (partExpression.Operator.Priority <= lastOperator.Priority)
                {
                    var lastNumber = numbers.Pop();
                    var prevLastNumber = numbers.Pop();

                    // удалим оператор, который взяли через Peek()
                    operators.Pop();

                    var value = lastOperator.Execute(prevLastNumber, lastNumber);
                    value = partExpression.Operator.Execute(value, partExpression.Number);

                    numbers.Push(value);
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

                var value = @operator.Execute(prevLastNumber, lastNumber);
                numbers.Push(value);
            }

            return numbers.Pop();
        }
    }
}
