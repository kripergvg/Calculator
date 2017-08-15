using Calculator.Operators;
using Calculator.ParsedParts;
using SimpleInjector;

namespace Calculator
{
    public static class RegistrationCalculator
    {
        public static Container RegisterCalculator(this Container container)
        {
            container.RegisterSingleton<ICalculator, Calculator>();
            container.RegisterSingleton<IPartExpressionsParser, PartExpressionsParser>();
            container.RegisterSingleton<IOperatorParser, OperatorParser>();
            container.RegisterSingleton<IExpressionFormatter, ExpressionFormatter>();

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
