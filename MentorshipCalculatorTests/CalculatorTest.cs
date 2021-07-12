using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MentorshipCalculator;

namespace MentorshipCalculator.Tests
{
    public class CalculatorTest : IDisposable
    {
        Calculator calc;

        public CalculatorTest()
        {
            calc = new Calculator();
        }

        public void Dispose()
        {
            calc.Clear();
        }


        [Theory]
        [InlineData(-5)]
        [InlineData(1000)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Put_InputIsIntegerAndQueueHasSpace_AddsToQueue(int value)
        {
            calc.Put(value);
            int result = calc.Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public void Put_InputIsIntegerAndQueueHasNoSpace_ThrowsException()
        {
            //filling up queue
            for (int i = 0; i < 5; i++)
                calc.Put(i);

            int value = 420;
            Assert.Throws<InvalidOperationException>(() => calc.Put(value));

        }

        [Fact]
        public void Add_QueueHasOnevalue_ThrowsException()
        {
            calc.Put(1);
            Assert.Throws<InvalidOperationException>(() => calc.Add());
        }

        [Fact]
        public void Sub_QueueHasOnevalue_ThrowsException()
        {
            calc.Put(1);
            Assert.Throws<InvalidOperationException>(() => calc.Sub());
        }

        [Fact]
        public void Remove_RemovesValueIfOneExists()
        {
            calc.Put(1);
            calc.Remove();

            int result = calc.Count();
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(10, 5)]
        [InlineData(1000, 4000)]
        [InlineData(-10, -30)]
        [InlineData(10, -90)]
        [InlineData(int.MinValue, 1000)]
        [InlineData(5000, 10000)]
        [InlineData(-25, 17)]

        public void Add_IsValidResult(int value1, int value2)
        {
            calc.Put(value1);
            calc.Put(value2);
            calc.Add();

            int additionValue = calc.GetQueueValues()[0]; //first in queue, result

            Assert.True(0 <= additionValue && additionValue <= 4095);
        }

        [Theory]
        [InlineData(10, 5)]
        [InlineData(1000, 4000)]
        [InlineData(-10, -30)]
        [InlineData(10, -90)]
        [InlineData(int.MaxValue, 1000)]
        [InlineData(5000, 10000)]
        [InlineData(-25, 17)]

        public void Sub_IsValidResult(int value1, int value2)
        {
            calc.Put(value1);
            calc.Put(value2);
            calc.Sub();

            int subValue = calc.GetQueueValues()[0]; //first in queue, result

            Assert.True(0 <= subValue && subValue <= 4095);
        }








    }
}
