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
        public void DefaultUserShouldBeInvalid()
        {
            User defaultUser = new User();
            Assert.False(defaultUser.Validate());
        }

        [Fact]
        public void SampleAmericanUserShouldBeValid()
        {
            Assert.True(_validAmericanUser.Validate());
        }

        [Fact]
        public void SampleNonAmericanUserShouldBeValid()
        {
            Assert.True(_validNonAmericanUser.Validate());
        }
    }
}
