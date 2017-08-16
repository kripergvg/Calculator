using System;
using Calculator.Calculator;
using FluentAssertions;
using NUnit.Framework;
using SimpleInjector;

namespace Calculator.Tests.Integration
{
    [TestFixture]
    public class CalculatorTests
    {
        public ICalculator Calculator { get; set; }

        [OneTimeSetUp]
        public void RegisterContainer()
        {
            var container = new Container();
            container.RegisterCalculator();

            Calculator = container.GetInstance<ICalculator>();
        }

        [TestCase(" 23 * 2 + 45 - 24 / 5", 86.2)]
        [TestCase("3+1", 4)]
        [TestCase("3+1   ", 4)]
        [TestCase("    3+1", 4)]
        [TestCase("3+    1", 4)]
        [TestCase("3    +1", 4)]
        [TestCase("3   +   1", 4)]
        [TestCase("4/2", 2)]
        [TestCase("3*2", 6)]
        [TestCase("3*1", 3)]
        [TestCase("3+1/2*3-10", -5.5)]
        [TestCase("-3-1", -4)]
        [TestCase("-3*2", -6)]
        [TestCase("-10/2", -5)]
        [TestCase("-3+2", -1)]
        public void Calculate_WhenExpressionValid_ShouldReturnCorrectResult(string exression, double expectedResult)
        {
            // when
            var result = Calculator.Calculate(exression);

            // then
            Math.Abs(result.Value - expectedResult).Should().BeLessOrEqualTo(0.0001);
        }

        [TestCase("3Operator1")]
        [TestCase("Operato3r1")]
        [TestCase("Operator1+3")]
        public void Calculate_WhenExpressionIsNotValid_ShouldReturnFailedResult(string exression)
        {
            // when
            var result = Calculator.Calculate(exression);

            // then
            result.IsFailure.Should().BeTrue();
        }

        [TestCase("3/0")]
        public void Calculate_WhenNumbersIsNotValid_ShouldReturnFailedResult(string exression)
        {
            // when
            var result = Calculator.Calculate(exression);

            // then
            result.IsFailure.Should().BeTrue();
        }
    }
}
