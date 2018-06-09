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

            // Assert that empty string Location fails validation
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
        public void UserEmailRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Email = null;
            non_us.Email = null;

            // Assert that null Email fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void UserEmailNotEmptyString()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Email = "";
            non_us.Email = "";

            // Assert that empty string Email fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Theory]
        [Trait("Type", "TruePositive")]
        [InlineData("1_2-a.b+c@a.b-c")]
        [InlineData("a@[123.123.123.123]")]
        [InlineData("a@123.123.123.123")]
        public void ValidUserEmailPasses(string email)
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Email = email;
            non_us.Email = email; 

            // Assert that valid emails pass
            Assert.True(us.Validate());
            Assert.True(non_us.Validate());
        }

        [Theory]
        [Trait("Type", "TrueNegative")]
        [InlineData("abc")]
        [InlineData("@domain.com")]
        [InlineData("my<p>hello</p>my@mail.com")]
        [InlineData("a@b@c.com")]
        [InlineData("abc.com")]
        [InlineData("a@b.com word")]
        public void InvalidUserEmailFails(string email)
        {
            // Arrange
            User us = US_User();
            User non_us = US_User();

            // Act
            us.Email = email;
            non_us.Email = email;

            // Assert that invalid emails fails
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void UserNameRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Name = null;
            non_us.Name = null;

            // Assert that null Name fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void UserNameIdRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Name.NameId = Guid.Empty;
            non_us.Name.NameId = Guid.Empty;

            // Assert that empty NameId fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void UserFirstNameRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Name.First = null;
            non_us.Name.First = null;

            // Assert that null First Name fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void UserFirstNameNotEmptyString()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Name.First = "";
            non_us.Name.First = "";

            // Assert that empty string First Name fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "NotRequiredField")]
        public void UserMiddleNameNotRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Name.Middle = null;
            non_us.Name.Middle = null;

            // Assert that null Middle Name passes validation
            Assert.True(us.Validate());
            Assert.True(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void UserMiddleNameNotEmptyString()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Name.Middle = "";
            non_us.Name.Middle = "";

            // Assert that empty string Middle Name fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void UserLastNameRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Name.Last = null;
            non_us.Name.Last = null;

            // Assert that null Last Name fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void UserLastNameNotEmptyString()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Name.Last = "";
            non_us.Name.Last = "";

            // Assert that empty string Last Name fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void UserGenderRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Gender = null;
            non_us.Gender = null;

            // Assert that null Gender fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Theory]
        [Trait("Type", "TruePositive")]
        [InlineData("M")]
        [InlineData("m")]
        [InlineData("male")]
        [InlineData("MALE")]
        [InlineData("Male")]
        [InlineData("F")]
        [InlineData("f")]
        [InlineData("female")]
        [InlineData("FEMALE")]
        [InlineData("Female")]
        public void ValidGenderPasses(string gender)
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Gender = gender;
            non_us.Gender = gender;

            // Assert that valid Gender passes
            Assert.True(us.Validate());
            Assert.True(non_us.Validate());
        }

        [Theory]
        [Trait("Type", "TrueNegative")]
        [InlineData("X")]
        [InlineData("")]
        [InlineData("12345")]
        public void InvalidGenderFails(string gender)
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Gender = gender;
            non_us.Gender = gender;

            // Assert that invalid Gender fails
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void UserTypeRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Type = null;
            non_us.Type = null;

            // Assert that empty Type fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void UserTypeNotEmptyString()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Type = "";
            non_us.Type = "";

            // Assert that empty string Type fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void PostalCodeRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Address.PostalCode = null;
            non_us.Address.PostalCode = null;

            // Assert that null Postal Code fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void PostalCodeNotEmptyString()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Address.PostalCode = "";
            non_us.Address.PostalCode = "";

            // Assert that empty string Postal Code fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void CountryRequired()
        {
            // Arrange
            User us = US_User();
            User non_us = Non_US_User();

            // Act
            us.Address.Country = null;
            non_us.Address.Country = null;

            // Assert that null Country fails validation
            Assert.False(us.Validate());
            Assert.False(non_us.Validate());
        }

        [Theory]
        [Trait("Type", "TruePositive")]
        [InlineData("GB")]
        [InlineData("ZA")]
        [InlineData("NL")]
        [InlineData("BR")]
        [InlineData("CO")]
        [InlineData("FR")]
        public void ValidNonUSCountryCodePasses(string countryCode)
        {
            // Arrange
            User non_us = Non_US_User();

            // Act
            non_us.Address.Country = countryCode;

            // Assert that valid Country passes
            Assert.True(non_us.Validate());
        }

        [Theory]
        [Trait("Type", "TruePositive")]
        [InlineData("US")]
        [InlineData("us")]
        public void ValidUSCountryCodePasses(string countryCode)
        {
            // Arrange
            User us = US_User();

            // Act
            us.Address.Country = countryCode;

            // Assert that valid US Country code passes
            Assert.True(us.Validate());
        }

        [Theory]
        [Trait("Type", "TrueNegative")]
        [InlineData("")]
        [InlineData("United States")]
        [InlineData("USA")]
        public void InvalidUSCountryCodeFails(string countryCode)
        {
            // Arrange
            User us = US_User();

            // Act
            us.Address.Country = countryCode;

            // Assert that invalid US Country code fails
            Assert.False(us.Validate());
        }

        [Theory]
        [Trait("Type", "TrueNegative")]
        [InlineData("")]
        [InlineData("China")]
        [InlineData("ZZ")]
        [InlineData("YY")]
        [InlineData("XX")]
        [InlineData("A")]
        [InlineData("ABC")]
        [InlineData("US")]
        public void InvalidNonUSCountryCodeFails(string countryCode)
        {
            // Arrange
            User non_us = Non_US_User();

            // Act
            non_us.Address.Country = countryCode;

            // Assert that invalid Non-US Country code fails
            Assert.False(non_us.Validate());
        }
    }
}
