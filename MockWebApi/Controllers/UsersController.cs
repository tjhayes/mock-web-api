using MongoDA;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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

        public UsersController(IUserRepository repo)
        {
            _context = new UserContext(repo);
        }


        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>OkObjectResult with an IEnumerable of all users,
        /// or a 500 StatusCodeResult if an error occurs.</returns>
        [HttpGet]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = _context.Get();
                return await Task.Run(() => Ok(users));
            }
            catch 
            {
                return new StatusCodeResult(500);
            }
        }

        // GET api/users/12345678-1234-1234-1234-1234567890AB
        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            return Json(_context.GetById(id));
        }
    }
}