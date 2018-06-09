using MongoDA;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MockWebApi.UnitTests
{
    public class UserModelTests
    {
        private readonly User _user;

        public UserModelTests()
        {
            _user = new User();
        }

        [Fact]
        public void DefaultUserShouldBeInvalid()
        {
            Assert.False(_user.Validate());
        }
    }
}
