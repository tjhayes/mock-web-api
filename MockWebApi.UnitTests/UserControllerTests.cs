﻿using Microsoft.AspNetCore.Mvc;
using MockWebApi.Controllers;
using MongoDA;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MockWebApi.UnitTests
{
    public class UserControllerTests
    {
        private List<User> users;

        public UserControllerTests()
        {
            users = new List<User>();

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

            User validAmericanUser2 = new User();
            validAmericanUser2.Address = new Address();
            validAmericanUser2.Address.AddressId = new Guid("44444444-4444-4444-4444-444444444444");
            validAmericanUser2.Address.Address1 = "100 Short Street";
            validAmericanUser2.Address.Address2 = "Apt 100";
            validAmericanUser2.Address.City = "Reston";
            validAmericanUser2.Address.PostalCode = "12321";
            validAmericanUser2.Address.State = "va";
            validAmericanUser2.Address.Country = "US";
            validAmericanUser2.Email = "Sophie@email.com";
            validAmericanUser2.Gender = "Female";
            validAmericanUser2.Location = "Reston";
            validAmericanUser2.Name = new Name();
            validAmericanUser2.Name.First = "Sophie";
            validAmericanUser2.Name.Middle = "Anna";
            validAmericanUser2.Name.Last = "Johnson";
            validAmericanUser2.Name.NameId = new Guid("55555555-5555-5555-5555-555555555555");
            validAmericanUser2.Type = "Associate";
            validAmericanUser2.UserId = new Guid("77777777-7777-7777-7777-777777777777");

            users.Add(validAmericanUser);
            users.Add(validAmericanUser2);
        }

        [Fact]
        public async void ValidGet_ShouldReturn200_AndList()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.Get()).Returns(users);

            UsersController c = new UsersController(mockRepo.Object);

            ObjectResult result = (ObjectResult) await c.Get();

            Assert.Equal(200, result.StatusCode);
            IEnumerable<User> usersResult = (IEnumerable<User>) result.Value;
            var enumerator = usersResult.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal("John", enumerator.Current.Name.First);
            enumerator.MoveNext();
            Assert.Equal("Sophie", enumerator.Current.Name.First);
            enumerator.Dispose();
        }

        [Fact]
        public async void InvalidGet_ShouldReturn500()
        {
            var mockRepo = new Mock<IUserRepository>();
            // Make user list invalid
            users[0].Name = null;
            mockRepo.Setup(x => x.Get()).Returns(users);

            UsersController c = new UsersController(mockRepo.Object);

            StatusCodeResult result = (StatusCodeResult) await c.Get();

            Assert.Equal(500, result.StatusCode);
        }
    }
}
