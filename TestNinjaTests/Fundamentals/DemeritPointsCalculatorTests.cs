using NUnit.Framework;
using TestNinja.Fundamentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Fundamentals.Tests
{
    [TestFixture()]
    public class DemeritPointsCalculatorTests
    {
        /*
                < 0
                > 300

                    speed <= speedlimit

                    speed >= speedlimit
        */

        [Test()]
        public void CalculateDemeritPoints_SpeedNegative_ReturnArgumentOutOfRangeException()
        {
            var demeritPointsCalculator = new DemeritPointsCalculator();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                demeritPointsCalculator.CalculateDemeritPoints(-1);
            });
        }

        [Test()]
        public void CalculateDemeritPoints_SpeedBiggerThenMaxSpeed_ReturnArgumentOutOfRangeException()
        {
            var demeritPointsCalculator = new DemeritPointsCalculator();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                demeritPointsCalculator.CalculateDemeritPoints(301);
            });
        }

        [Test()]
        public void CalculateDemeritPoints_SpeedIsLessThanSpeedLimit_ReturnZero()
        {
            var demeritPointsCalculator = new DemeritPointsCalculator();

            var result = demeritPointsCalculator.CalculateDemeritPoints(64);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test()]
        public void CalculateDemeritPoints_SpeedIsEqualThanSpeedLimit_ReturnZero()
        {
            var demeritPointsCalculator = new DemeritPointsCalculator();

            var result = demeritPointsCalculator.CalculateDemeritPoints(65);

            Assert.That(result, Is.EqualTo(0));
        }


        [Test()]
        public void CalculateDemeritPoints_SpeedIsBiggerThanSpeedLimitBy1Km_ReturnZero()
        {
            var demeritPointsCalculator = new DemeritPointsCalculator();

            var result = demeritPointsCalculator.CalculateDemeritPoints(66);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test()]
        public void CalculateDemeritPoints_SpeedIsBiggerThanSpeedLimit_ReturnOne()
        {
            var demeritPointsCalculator = new DemeritPointsCalculator();

            var result = demeritPointsCalculator.CalculateDemeritPoints(70);

            Assert.That(result, Is.EqualTo(1));
        }


        [Test]
        [TestCase(0, 0)]
        [TestCase(64, 0)]
        [TestCase(65, 0)]
        [TestCase(66, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnPoints(int speed, int expectedResult)
        {
            var demeritPointsCalculator = new DemeritPointsCalculator();

            var result = demeritPointsCalculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_SpeedIsOutOfRange_ReturnReturnArgumentOutOfRangeException(int speed)
        {
            var calculator = new DemeritPointsCalculator();

            Assert.That(() => calculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}