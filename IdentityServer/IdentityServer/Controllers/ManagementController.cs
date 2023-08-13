using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly ConfigurationDbContext _configuration;
        private readonly TestUserStore _users;
        public ManagementController(ConfigurationDbContext configurationDb,  TestUserStore testUserStore)
        {
            _users= testUserStore;
            _configuration = configurationDb;
        }
        [HttpPost]
        public IActionResult Register(string username,string password)
        {
            _users.CreateUser(username, password);
            return Ok(_users.FindByUsername(username));
        }
        [HttpGet]
        public IActionResult test()
        {
            return Ok();    
        }
    }
}
