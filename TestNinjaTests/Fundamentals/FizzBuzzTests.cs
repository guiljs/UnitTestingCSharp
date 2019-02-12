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
    public class FizzBuzzTests
    {
        [Test()]
        public void GetOutput_Number3_ReturnFizz()
        {
            var result = FizzBuzz.GetOutput(3);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_Number5_ReturnBuzz()
        {
            Assert.That(FizzBuzz.GetOutput(5), Is.EqualTo("Buzz"));
        }

        [Test]
        [TestCase(3, ExpectedResult = "Fizz")]
        [TestCase(5, ExpectedResult = "Buzz")]
        [TestCase(15, ExpectedResult = "FizzBuzz")]
        [TestCase(1, ExpectedResult = "1")]
        public string GetOutPut_WhenCalled_ReturnRight(int number)
        {
            return FizzBuzz.GetOutput(number);
        }
    }
}