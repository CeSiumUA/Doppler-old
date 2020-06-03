using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DopplerREST.Database;
using DopplerREST.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DopplerREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private APIdataBaseContext dbContext;
        public AuthenticationController(APIdataBaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost("/api/[controller]/register")]
        public async Task<ActionResult<Contact>> RegisterUser(string username, string password, string name, string Description)
        {
            
            try
            {
                User user = new User()
                {
                    UserName = username,
                    Password = password,
                    Contact = new Contact
                    {
                        UserName = username,
                        Description = Description,
                        Name = name
                    }
                };

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                return Ok(user.Contact);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Contact>> LoginUser(string username, string password)
        {
            User usr = await dbContext.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefaultAsync();
            if(usr != null)
            {
                return usr.Contact;
            }
            return NotFound();
        }
    }
}
