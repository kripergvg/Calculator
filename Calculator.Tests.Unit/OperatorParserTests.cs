using System.Collections.Generic;
using Calculator.Operators;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Calculator.Tests.Unit
{
    [TestFixture]
    public class OperatorParserTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void ParseOperator_WhenHasValidOperator_ShouldReturnOperator()
        {
            // given
            var validOperator = Substitute.For<IOperator>();
            validOperator.Validate(Arg.Any<string>()).Returns(true);

            var notValidOperator = Substitute.For<IOperator>();
            notValidOperator.Validate(Arg.Any<string>()).Returns(false);

            var operators = new List<IOperator>
            {
                validOperator,
                notValidOperator
            };

            // SUT
            var parser = new OperatorParser(operators);

            // when
            var parsedOperator = parser.ParseOperator(_fixture.Create<string>());

            // then
            parsedOperator.Should().Be(validOperator);
        }

        [Test]
        public void ParseOperator_WhenHasTwoValidOperator_ShouldReturnFirstOperator()
        {
            // given
            var firstValidOperator = Substitute.For<IOperator>();
            firstValidOperator.Validate(Arg.Any<string>()).Returns(true);

            var secondValidOperator = Substitute.For<IOperator>();
            secondValidOperator.Validate(Arg.Any<string>()).Returns(true);

            var operators = new List<IOperator>
            {
                firstValidOperator,
                secondValidOperator
            };

            // SUT
            var parser = new OperatorParser(operators);

            // when
            var parsedOperator = parser.ParseOperator(_fixture.Create<string>());

            // then
            parsedOperator.Should().Be(firstValidOperator);
        }

        [Test]
        public void ParseOperator_WhenHasntValidOperator_ShouldReturnNull()
        {
            // given
            var notValidOperator = Substitute.For<IOperator>();
            notValidOperator.Validate(Arg.Any<string>()).Returns(false);

            var operators = new List<IOperator>
            {
                notValidOperator
            };

            // SUT
            var parser = new OperatorParser(operators);

            // when
            var parsedOperator = parser.ParseOperator(_fixture.Create<string>());

            // then
            parsedOperator.Should().BeNull();
        }
    }
}
