using MongoDA;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MockWebApi.UnitTests
{
    public class UserModelTests
    {
        public User US_User()
        {
            User validAmericanUser = new User();
            validAmericanUser.Address = new Address();
            validAmericanUser.Address.AddressId = new Guid("11111111-1111-1111-1111-111111111111");
            validAmericanUser.Address.Address1 = "Apt 500, 100 Long Street";
            validAmericanUser.Address.Address2 = null;
            validAmericanUser.Address.City = "Tampa";
            validAmericanUser.Address.PostalCode = "12345";
            validAmericanUser.Address.State = "FL";
            validAmericanUser.Address.Country = "us";
            validAmericanUser.Email = "john@smith.com";
            validAmericanUser.Gender = "M";
            validAmericanUser.Location = "Tampa";
            validAmericanUser.Name = new Name();
            validAmericanUser.Name.First = "John";
            validAmericanUser.Name.Middle = null;
            validAmericanUser.Name.Last = "Smith";
            validAmericanUser.Name.NameId = new Guid("22222222-2222-2222-2222-222222222222");
            validAmericanUser.Type = "Associate";
            validAmericanUser.UserId = new Guid("33333333-3333-3333-3333-333333333333");
            return validAmericanUser;
        }

        public User Non_US_User()
        {
            User validNonAmericanUser = new User();
            validNonAmericanUser.Address = new Address();
            validNonAmericanUser.Address.AddressId = 
                new Guid("44444444-4444-4444-4444-444444444444");
            validNonAmericanUser.Address.Address1 = "132 Old Road";
            validNonAmericanUser.Address.Address2 = "Apt 100";
            validNonAmericanUser.Address.City = "Maastricht";
            validNonAmericanUser.Address.PostalCode = "3581 CD";
            validNonAmericanUser.Address.State = "Limburg";
            validNonAmericanUser.Address.Country = "NL";
            validNonAmericanUser.Email = "sophie@jansen.com";
            validNonAmericanUser.Gender = "Female";
            validNonAmericanUser.Location = "Tampa";
            validNonAmericanUser.Name = new Name();
            validNonAmericanUser.Name.First = "Sophie";
            validNonAmericanUser.Name.Middle = "Emma";
            validNonAmericanUser.Name.Last = "Jansen";
            validNonAmericanUser.Name.NameId = new Guid("55555555-5555-5555-5555-555555555555");
            validNonAmericanUser.Type = "Associate";
            validNonAmericanUser.UserId = new Guid("66666666-6666-6666-6666-666666666666");
            return validNonAmericanUser;
        }

        [Fact]
        [Trait("Type", "ControlGroup")]
        public void DefaultUserShouldBeInvalid()
        {
            User defaultUser = new User();
            Assert.False(defaultUser.Validate());
        }

        [Fact]
        [Trait("Type", "ControlGroup")]
        public void SampleAmericanUserShouldBeValid()
        {
            Assert.True(US_User().Validate());
        }

        [Fact]
        [Trait("Type", "ControlGroup")]
        public void SampleNonAmericanUserShouldBeValid()
        {
            Assert.True(Non_US_User().Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void UserIdRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.UserId = Guid.Empty;
            non_us.UserId = Guid.Empty;

            // Assert that empty Guids fail validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void UserLocationRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Location = null;
            non_us.Location = null;

            // Assert that null Location fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void UserLocationNotEmptyString()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Location = "";
            non_us.Location = "";

            // Assert that null Location fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "NotRequiredField")]
        public void UserAddressNotRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Address = null;
            non_us.Address = null;

            // Assert that null Address passes
            Assert.True(us.Validate());
            Assert.True(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void AddressIDRequired()
        {
            //Arrange
            User us = US_User();
            User non_us = Non_US_User();

            //Act
            us.Address.AddressId = Guid.Empty;
            non_us.Address.AddressId = Guid.Empty;

            //Assert that fail validation
            Assert.False(us.Address.Validate());
            Assert.False(non_us.Address.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void Address1Required()
        {
            //Arrange
            User us = US_User();
            User non_us = Non_US_User();

            //Act
            us.Address.Address1 = null;
            non_us.Address.Address1 = null;

            //Assert fail validation
            Assert.False(us.Address.Validate());
            Assert.False(non_us.Address.Validate());
        }

        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void Address1IsNotEmptyString()
        {
            //Arrange
            User us = US_User();
            User non_us = Non_US_User();

            //Act
            us.Address.Address1 = "";
            non_us.Address.Address1 = "";

            //Assert fail validation
            Assert.False(us.Address.Validate());
            Assert.False(non_us.Address.Validate());
        }

        [Fact]
        [Trait(Type)]
    }
}
