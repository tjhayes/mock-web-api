using MongoDA;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MockWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserContext _context;

        public UsersController()
        {
            _context = new UserContext(new UserRepository());
        }

        // GET api/users
        [HttpGet]
        public JsonResult Get()
        {
            return Json(_context.Get());
        }

        // GET api/users/12345678-1234-1234-1234-1234567890AB
        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            return Json(_context.GetById(id));
        }
    }
}