using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUnitTesting.Tests
{
    [TestFixture]
    public class CalculationsTest
    {
        private Calculations.MathsHelper expectedResult;
        int result;
        [SetUp]
        public void TestSetup()
        {
            expectedResult = new Calculations.MathsHelper();
        }

        [TestCase]
        public void AddTest()
        {
            result = expectedResult.Add(20, 10);
            Assert.AreEqual(30, result);
        }

        [TestCase]
        public void SubtractTest()
        {
            result = expectedResult.Subtract(20, 10);
            Assert.AreEqual(10, result);
        }
        [TestCase]
        public void MultiplyTest()
        {
            result = expectedResult.Multiply(20, 10);
            Assert.AreEqual(200, result);
        }
        [TestCase]
        public void DivideTest()
        {
            result = expectedResult.Divide(20, 10);
            Assert.That(result, Is.EqualTo(2));
        }
        [TestCase]
        public void DivideZeroTest()
        {
            result = expectedResult.Divide(20, 0);
            Assert.AreEqual(0, result);
        }


        [TearDown]
        public void TestTearDown()
        {
            expectedResult = null;
        }
    }
}
