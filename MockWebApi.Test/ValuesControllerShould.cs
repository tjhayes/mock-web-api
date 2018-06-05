using MockWebApi.Controllers;
using System;
using Xunit;

namespace MockWebApi.Test
{
    public class ValuesControllerShould
    {
        [Theory]
        [Trait("Category", "Controller")]
        [InlineData(1, "value #1" )]
        [InlineData(5, "value #5" )]
        [InlineData(100, "value #100" )]
        public void GetById(int num, string expectedResult)
        {
            // Arrange
            ValuesController valuesController = new ValuesController();
    
            // Act
            var actualResult = valuesController.Get(num);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Get()
        {
            // Arrange
            ValuesController valuesController = new ValuesController();
    
            // Act
            var actualResult = valuesController.Get().GetEnumerator();
            actualResult.MoveNext();

            // Assert
            Assert.Equal("value1", actualResult.Current);
            actualResult.MoveNext();
            Assert.Equal("value2", actualResult.Current);
        }
    }
}
