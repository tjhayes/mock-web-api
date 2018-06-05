using FakeDb;
using MockWebApi.Controllers;
using System;
using Xunit;

namespace MockWebApi.Test
{
    public class ValuesControllerShould
    {
        [Theory]
        [Trait("Category", "Controller")]
        [ClassData(typeof(Db))]
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
