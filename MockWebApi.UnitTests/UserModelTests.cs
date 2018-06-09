using MongoDA;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MockWebApi.UnitTests
{
    public class UserModelTests
    {
        private readonly User _validAmericanUser;
        private readonly User _validNonAmericanUser;

        public UserModelTests()
        {
            _validAmericanUser = new User();
            _validAmericanUser.Address = new Address();
            _validAmericanUser.Address.AddressId = new Guid("11111111-1111-1111-1111-111111111111");
            _validAmericanUser.Address.Address1 = "Apt 500, 100 Long Street";
            _validAmericanUser.Address.Address2 = null;
            _validAmericanUser.Address.City = "Tampa";
            _validAmericanUser.Address.PostalCode = "12345";
            _validAmericanUser.Address.State = "FL";
            _validAmericanUser.Address.Country = "us";
            _validAmericanUser.Email = "john@smith.com";
            _validAmericanUser.Gender = "M";
            _validAmericanUser.Location = "Tampa";
            _validAmericanUser.Name = new Name();
            _validAmericanUser.Name.First = "John";
            _validAmericanUser.Name.Middle = null;
            _validAmericanUser.Name.Last = "Smith";
            _validAmericanUser.Name.NameId = new Guid("22222222-2222-2222-2222-222222222222");
            _validAmericanUser.Type = "Associate";
            _validAmericanUser.UserId = new Guid("33333333-3333-3333-3333-333333333333");

            _validNonAmericanUser = new User();
            _validNonAmericanUser.Address = new Address();
            _validNonAmericanUser.Address.AddressId = 
                new Guid("44444444-4444-4444-4444-444444444444");
            _validNonAmericanUser.Address.Address1 = "132 Old Road";
            _validNonAmericanUser.Address.Address2 = "Apt 100";
            _validNonAmericanUser.Address.City = "Maastricht";
            _validNonAmericanUser.Address.PostalCode = "3581 CD";
            _validNonAmericanUser.Address.State = "Limburg";
            _validNonAmericanUser.Address.Country = "NL";
            _validNonAmericanUser.Email = "sophie@jansen.com";
            _validNonAmericanUser.Gender = "Female";
            _validNonAmericanUser.Location = "Tampa";
            _validNonAmericanUser.Name = new Name();
            _validNonAmericanUser.Name.First = "Sophie";
            _validNonAmericanUser.Name.Middle = "Emma";
            _validNonAmericanUser.Name.Last = "Jansen";
            _validNonAmericanUser.Name.NameId = new Guid("55555555-5555-5555-5555-555555555555");
            _validNonAmericanUser.Type = "Associate";
            _validNonAmericanUser.UserId = new Guid("66666666-6666-6666-6666-666666666666");
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
            Assert.True(_validAmericanUser.Validate());
        }

        [Fact]
        [Trait("Type", "ControlGroup")]
        public void SampleNonAmericanUserShouldBeValid()
        {
            Assert.True(_validNonAmericanUser.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void UserIdRequired()
        {
            Guid oldId1 = _validAmericanUser.UserId;
            Guid oldId2 = _validNonAmericanUser.UserId;

            _validAmericanUser.UserId = Guid.Empty;
            _validNonAmericanUser.UserId = Guid.Empty;

            // Assert that empty Guids fail validation
            Assert.False(_validAmericanUser.Validate());
            Assert.False(_validNonAmericanUser.Validate());

            _validAmericanUser.UserId = oldId1;
            _validNonAmericanUser.UserId = oldId2;

            // Assert that non-empty Guids pass validation
            Assert.True(_validAmericanUser.Validate());
            Assert.True(_validNonAmericanUser.Validate());
        }

        [Fact]
        [Trait("Type", "RequiredField")]
        public void UserLocationRequired()
        {
            string oldLoc1 = _validAmericanUser.Location;
            string oldLoc2 = _validNonAmericanUser.Location;

            _validAmericanUser.Location = null;
            _validNonAmericanUser.Location = null;

            // Assert that null Location fails validation
            Assert.False(_validAmericanUser.Validate());
            Assert.False(_validNonAmericanUser.Validate());

            _validAmericanUser.Location = oldLoc1;
            _validNonAmericanUser.Location = oldLoc2;
        }

        [Fact]
        [Trait("Type", "NotEmptyString")]
        public void UserLocationNotEmptyString()
        {
            string oldLoc1 = _validAmericanUser.Location;
            string oldLoc2 = _validNonAmericanUser.Location;

            _validAmericanUser.Location = "";
            _validNonAmericanUser.Location = "";

            // Assert that empty string Location fails validation
            Assert.False(_validAmericanUser.Validate());
            Assert.False(_validNonAmericanUser.Validate());

            _validAmericanUser.Location = oldLoc1;
            _validNonAmericanUser.Location = oldLoc2;
        }

        [Fact]
        [Trait("Type", "NotRequiredField")]
        public void UserAddressNotRequired()
        {
            Address oldAddress1 = _validAmericanUser.Address;
            Address oldAddress2 = _validNonAmericanUser.Address;

            _validAmericanUser.Address = null;
            _validNonAmericanUser.Address = null;

            // Assert that null Address passes validation
            Assert.True(_validAmericanUser.Validate());
            Assert.True(_validNonAmericanUser.Validate());

            _validAmericanUser.Address = oldAddress1;
            _validNonAmericanUser.Address = oldAddress2;
        }

    }
}
