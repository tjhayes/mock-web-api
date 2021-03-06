﻿using MongoDA;
using System;
using Xunit;

namespace MockWebApi.UnitTests
{
    public class UserModelTests
    {
        /// <summary>
        /// Generates a sample valid American user
        /// </summary>
        /// <returns>A valid user object with an American address</returns>
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

        /// <summary>
        /// Generates a sample valid non-American user
        /// </summary>
        /// <returns>A valid user object with a non-American address</returns>
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

        /// <summary>
        /// Test a default user
        /// </summary>
        [Fact]
        [Trait("Type", "ControlGroup")]
        public void DefaultUserShouldBeInvalid()
        {
            User defaultUser = new User();
            Assert.False(defaultUser.Validate());
        }

        /// <summary>
        /// Test the sample American user
        /// </summary>
        [Fact]
        [Trait("Type", "ControlGroup")]
        public void SampleAmericanUserShouldBeValid()
        {
            Assert.True(US_User().Validate());
        }

        /// <summary>
        /// Test the sample Non-American user
        /// </summary>
        [Fact]
        [Trait("Type", "ControlGroup")]
        public void SampleNonAmericanUserShouldBeValid()
        {
            Assert.True(Non_US_User().Validate());
        }

        /// <summary>
        /// Test that UserId is required
        /// </summary>
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

        /// <summary>
        /// Test that user Location is required
        /// </summary>
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

        /// <summary>
        /// Test that user Location isn't an empty string
        /// </summary>
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

        /// <summary>
        /// Test that user Address is not required
        /// </summary>
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

        /// <summary>
        /// Test that user Email is required
        /// </summary>
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

        /// <summary>
        /// Test that user Email can't be an empty string
        /// </summary>
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

        /// <summary>
        /// Test that valid email addresses pass the validator
        /// </summary>
        /// <param name="email">the email address to test</param>
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

        /// <summary>
        /// Test that invalid email addresses fail
        /// </summary>
        /// <param name="email">the email to test</param>
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

        /// <summary>
        /// Test that user's Name is required
        /// </summary>
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

        /// <summary>
        /// Test that user's NameId is required
        /// </summary>
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

        /// <summary>
        /// Test that user's First Name is required
        /// </summary>
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

        /// <summary>
        /// Test that user's First Name isn't an empty string
        /// </summary>
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

        /// <summary>
        /// Test that user's Middle Name isn't required
        /// </summary>
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

        /// <summary>
        /// Test that user's Middle Name isn't an empty string
        /// </summary>
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

        /// <summary>
        /// Test that user's Last Name is required
        /// </summary>
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

        /// <summary>
        /// Test that user's Last Name isn't empty string
        /// </summary>
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

        /// <summary>
        /// Test that user's Gender is required
        /// </summary>
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

        /// <summary>
        /// Test that valid Genders pass
        /// </summary>
        /// <param name="gender">The Gender string to test</param>
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

        /// <summary>
        /// Test that invalid Genders fail
        /// </summary>
        /// <param name="gender">The Gender string to test</param>
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

        /// <summary>
        /// Test that user Type is required
        /// </summary>
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

        /// <summary>
        /// Test that user Type isn't an empty string
        /// </summary>
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

        /// <summary>
        /// Test that user Postal Code is required
        /// </summary>
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

        /// <summary>
        /// Test that user Postal Code isn't an empty string
        /// </summary>
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

        /// <summary>
        /// Test that user Country is required
        /// </summary>
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

        /// <summary>
        /// Test that valid non-US country codes pass
        /// </summary>
        /// <param name="countryCode">The country code to test</param>
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

        /// <summary>
        /// Test that valid US country codes pass
        /// </summary>
        /// <param name="countryCode">The country code to test</param>
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

        /// <summary>
        /// Test that invalid US country codes fail
        /// </summary>
        /// <param name="countryCode">The country code to test</param>
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

        /// <summary>
        /// Test that invalid non-US country codes fail
        /// </summary>
        /// <param name="countryCode">The country code to test</param>
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

        /// <summary>
        /// Test that Address must have an Id.
        /// </summary>
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

        /// <summary>
        /// Test to ensure that at least one address field is filled in.
        /// </summary>
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

        /// <summary>
        /// Test that the required address is not an empty string.
        /// </summary>
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

        /// <summary>
        /// Test to ensure that a secondary address is not required.
        /// </summary>
        [Fact]
        [Trait("Type", "NotRequired")]
        public void Address2NotRequired()
        {
            //Arrange
            User us = US_User();
            User non_us = Non_US_User();

            //Act
            us.Address.Address2 = null;
            non_us.Address.Address2 = null;

            //Assert pass validation
            Assert.True(us.Address.Validate());
            Assert.True(non_us.Address.Validate());
        }

        /// <summary>
        /// Ensure that the secondary address is not an empty string
        /// </summary>
        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void Address2NotEmptyString()
        {
            //Arrange
            User us = US_User();
            User non_us = Non_US_User();

            //Act
            us.Address.Address2 = "";
            non_us.Address.Address2 = "";

            //Assert fail validation
            Assert.False(us.Address.Validate());
            Assert.False(non_us.Address.Validate());
        }

        /// <summary>
        /// Ensure that the city property is required.
        /// </summary>
        [Fact]
        [Trait("Type", "RequiredField")]
        public void CityRequired()
        {
            //Arrange
            User us = US_User();
            User non_us = Non_US_User();

            //Act
            us.Address.City = null;
            non_us.Address.City = null;

            //Assert fail validation
            Assert.False(us.Address.Validate());
            Assert.False(non_us.Address.Validate());
        }

        /// <summary>
        /// Ensure that the city is not an empty string.
        /// </summary>
        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void CityNotEmptyString()
        {
            //Arrange
            User us = US_User();
            User non_us = Non_US_User();

            //Act
            us.Address.City = "";
            non_us.Address.City = "";

            //Assert fail validation
            Assert.False(us.Address.Validate());
            Assert.False(non_us.Address.Validate());
        }

        /// <summary>
        /// Test that state is required.
        /// </summary>
        [Fact]
        [Trait("Type", "RequiredField")]
        public void StateRequired()
        {
            //Arrange
            User us = US_User();
            User non_us = Non_US_User();

            //Act
            us.Address.State = null;
            non_us.Address.State = null;

            //Assert fail validation
            Assert.False(us.Address.Validate());
            Assert.False(non_us.Address.Validate());
        }

        /// <summary>
        /// Test that state is not an empty string.
        /// </summary>
        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void StateNotEmptyString()
        {
            //Arrange
            User us = US_User();
            User non_us = Non_US_User();

            //Act
            us.Address.State = "";
            non_us.Address.State = "";

            //Assert fail validation
            Assert.False(us.Address.Validate());
            Assert.False(non_us.Address.Validate());
        }

        /// <summary>
        /// Test that valid state abbreviations pass.
        /// </summary>
        /// <param name="state"></param>
        [Theory]
        [Trait("Type", "TruePositive")]
        [InlineData("KS")]
        [InlineData("WV")]
        [InlineData("OK")]
        [InlineData("TX")]
        [InlineData("CA")]
        public void ValidAmericanStatePasses(string state)
        {
            //Arrange
            User us = US_User();
            User non_us = Non_US_User();

            //Act
            us.Address.State = state;
            non_us.Address.State = state;

            //Assert pass validation
            Assert.True(us.Address.Validate());
            Assert.True(non_us.Address.Validate());
        }

        /// <summary>
        /// Test that invalid state expressions fail.
        /// </summary>
        /// <param name="state"></param>
        [Theory]
        [Trait("Type", "TruePositive")]
        [InlineData("Kansas")]
        [InlineData("Wv")]
        [InlineData("Canada")]
        [InlineData("Europe")]
        [InlineData("I like pie")]
        public void InValidAmericanStateFails(string state)
        {
            //Arrange
            User us = US_User();

            //Act
            us.Address.State = state;

            //Assert pass validation
            Assert.False(us.Address.Validate());
        }

        /// <summary>
        /// Test that only proper postal code formats pass.
        /// </summary>
        /// <param name="zip"></param>
        [Theory]
        [Trait("Type", "TruePositive")]
        [InlineData("55555")]
        [InlineData("66043")]
        [InlineData("66048-1234")]
        [InlineData("66002")]
        [InlineData("12345-6789")]
        public void ValidAmericanPostalCodePasses(string zip)
        {
            //Arrange
            User us = US_User();
            User non_us = Non_US_User();

            //Act
            us.Address.PostalCode = zip;
            non_us.Address.PostalCode = zip;

            //Assert pass validation
            Assert.True(us.Address.Validate());
            Assert.True(non_us.Address.Validate());
        }

        /// <summary>
        /// Test that invalid postal code formats fail.
        /// </summary>
        /// <param name="zip"></param>
        [Theory]
        [Trait("Type", "TruePositive")]
        [InlineData("5555")]
        [InlineData("66043-")]
        [InlineData("66048-12")]
        [InlineData("66002-12345")]
        [InlineData("123456")]
        public void InValidAmericanPostalCodeFails(string zip)
        {
            //Arrange
            User us = US_User();

            //Act
            us.Address.PostalCode = zip;

            //Assert pass validation
            Assert.False(us.Address.Validate());
        }
    }
}
