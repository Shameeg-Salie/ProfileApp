using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileApplication.Data;
using ProfileApplication.Models;

namespace ProfileApplication.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserDbContext userDbContext;
        public UserController(UserDbContext userDbContext) { 

            this.userDbContext = userDbContext;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers() {
            return Ok(await userDbContext.Users.ToListAsync());
        }

        [HttpGet]
        [Route("{username}")]
        [ActionName("GetUsersByUsername")]
        public async Task<IActionResult> GetUsersByUsername([FromRoute] string Username)
        {
            var user = await userDbContext.Users.FirstOrDefaultAsync(x => x.Username == Username);

            //var user = await userDbContext.Users.FindAsync(Username);

            if (user == null) {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user) {
            //user.Id = 0;
            await userDbContext.Users.AddAsync(user);
            await userDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsersByUsername), new { Username = user.Username }, user);
        }
    }
}
