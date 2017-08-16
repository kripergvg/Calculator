using Calculator.Calculator;
using Calculator.ExpressionFormatter;
using Calculator.ExpressionsParser;
using Calculator.Operators;
using SimpleInjector;

namespace Calculator
{
    public static class RegistrationCalculator
    {
        public static Container RegisterCalculator(this Container container)
        {
            container.RegisterSingleton<ICalculator, Calculator.Calculator>();
            container.RegisterSingleton<IExpressionsParser, ExpressionsParser.ExpressionsParser>();
            container.RegisterSingleton<IOperatorParser, OperatorParser>();
            container.RegisterSingleton<IExpressionFormatter, ExpressionFormatter.ExpressionFormatter>();

            container.RegisterCollection<IOperator>(new[]
            {
                typeof(AdditionOperator),
                typeof(DivisionOperator),
                typeof(MultiplicationOperator),
                typeof(SubtractionOperator)
            });

            return container;
        }
    }
}
