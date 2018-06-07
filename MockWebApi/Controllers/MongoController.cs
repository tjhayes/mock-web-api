using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDA;
using MongoDB.Driver;

namespace MockWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class MongoController : Controller
    {
        private readonly UserRepository _repo;

        public MongoController()
        {
            string connectionString =
              @"mongodb://cameron-wags:rp7KMfeoIp0KgM7dMMpnZDF9Cmtde0PIlQAQ9pdrpZZaZdO9Pqt9mk8VXl3upDpp2pyrzajfNvOm2JZtqfOzkQ==@cameron-wags.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            IMongoClient mongoClient = new MongoClient(settings);
            _repo = new UserRepository(mongoClient, "userdb", "users");
        }

        //public MongoController(IMongoClient mongoClient, string databaseName, string usersTableName)
        //{
        //    _repo = new UserRepository(mongoClient, databaseName, usersTableName);
        //}

        [HttpGet]
        public JsonResult Get()
        {
            return Json(_repo.Get());
        }
    }
}