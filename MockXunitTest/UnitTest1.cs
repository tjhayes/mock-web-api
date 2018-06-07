using System;
using Xunit;
using MockWebApi.Models;
using Moq;

namespace MockXunitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange
            var test = new Mock<MockModel>();

            //Act
            test.SetupProperty(m => m.data, 3);

            Assert.Equal(3, test.Object.data);
        }
    }
}
