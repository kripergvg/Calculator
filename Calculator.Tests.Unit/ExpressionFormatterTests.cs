using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Tests.Unit
{
    [TestFixture]
    public class ExpressionFormatterTests
    {
        [Test]
        public void Format_WhenExpressionCorrect_ShouldReturnSameResult()
        {
            // given
            var expression = "testString";

            // SUT
            var formatter = new ExpressionFormatter.ExpressionFormatter();

            // when
            var formattedExpression = formatter.Format(expression);

            // then
            formattedExpression.Should().Be(expression);
        }

        [Test]
        public void Format_WhenExpressionHasSpaces_ShouldReturResultWithouSpaces()
        {
            // given
            var expression = "  testSt   ring  ";

            // SUT
            var formatter = new ExpressionFormatter.ExpressionFormatter();

            // when
            var formattedExpression = formatter.Format(expression);

            // then
            formattedExpression.Should().Be("testString");
        }
    }
}
