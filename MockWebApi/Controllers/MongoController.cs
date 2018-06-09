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
        private readonly UserContext _context;

        public MongoController()
        {
            _context = new UserContext(new UserRepository());
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(_context.Get());
        }
    }
}