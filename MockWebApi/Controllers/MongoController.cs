using System;
using System.Collections.Generic;
using System.Linq;
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
            _repo = new UserRepository("mongodb://localhost", "usersdb", "users");
        }

        //public MongoController(string mongoDbConnectionString, string databaseName, string usersTableName)
        //{
        //    _repo = new UserRepository(mongoDbConnectionString, databaseName, usersTableName);
        //}

        [HttpGet]
        public JsonResult Get()
        {
            return Json(_repo.Get());
        }
    }
}