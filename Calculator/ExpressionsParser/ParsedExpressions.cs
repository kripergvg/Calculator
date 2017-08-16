using System.Collections.Generic;

namespace Calculator.ExpressionsParser
{
    public class ParsedExpressions
    {
        public ParsedExpressions(double firstNumber, IReadOnlyList<ParsedExpression> partExpressions)
        {
            FirstNumber = firstNumber;
            PartExpressions = partExpressions;
        }

        public double FirstNumber { get; set; }

        public IReadOnlyList<ParsedExpression> PartExpressions { get; set; }
    }
}
