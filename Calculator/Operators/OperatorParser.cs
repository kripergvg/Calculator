using System.Collections.Generic;

namespace Calculator.Operators
{
    public class OperatorParser : IOperatorParser
    {
        private readonly IEnumerable<IOperator> _operators;

        public OperatorParser(IEnumerable<IOperator> operators)
        {
            _operators = operators;
        }

        public IOperator ParseOperator(string rawOperator)
        {
            foreach (var @operator in _operators)
            {
                if (@operator.Validate(rawOperator))
                {
                    return @operator;
                }
            }

            return null;
        }
    }
}
