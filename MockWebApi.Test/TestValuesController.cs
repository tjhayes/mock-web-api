using MockWebApi.Controllers;
using System;
using Xunit;

namespace MockWebApi.Test
{
    public class TestValuesController
    {
        [Theory]
        [Trait("Category", "Controller")]
        [InlineData(1, "value #1" )]
        [InlineData(5, "value #5" )]
        [InlineData(100, "value #100" )]
        public void TestGetById(int num, string expectedResult)
        {
            // Arrange
            ValuesController vc = new ValuesController();
    
            // Act
            var actualResult = vc.Get(num);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
