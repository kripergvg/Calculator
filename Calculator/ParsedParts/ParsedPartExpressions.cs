using System.Collections.Generic;

namespace Calculator.ParsedParts
{
    public class ParsedPartExpressions
    {
        public ParsedPartExpressions(double firstNumber, IReadOnlyList<PartExpression> partExpressions)
        {
            FirstNumber = firstNumber;
            PartExpressions = partExpressions;
        }

        public double FirstNumber { get; set; }

        public IReadOnlyList<PartExpression> PartExpressions { get; set; }
    }
}
