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
            var users = await userDbContext.Users.FromSqlRaw("Select * from users").ToListAsync();

            if (users == null) {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet]
        [Route("{username}")]
        [ActionName("GetUsersByUsername")]
        public async Task<IActionResult> GetUsersByUsername([FromRoute] string Username)
        {
            var user = await userDbContext.Users.FromSqlRaw(
                "Select * FROM users WHERE username = {0}", Username).ToListAsync();

            if (user == null) {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user) {

            await userDbContext.Users.AddAsync(user);
            await userDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsersByUsername), new { Username = user.Username }, user);
        }

        [HttpPut]
        [Route("{username}")]
        public async Task<IActionResult> UpdateUserInfo([FromRoute] string Username, [FromBody] User updatedInfo) {

            var currentUserInfo = await userDbContext.Users.FromSqlRaw(
                "Select * FROM users WHERE username = {0}", Username).ToListAsync();

            if (currentUserInfo == null) {
                return NotFound();
            }

            for (int i = 0; i < currentUserInfo.Count; i++) {
                currentUserInfo[i].Username = updatedInfo.Username;
                currentUserInfo[i].About = updatedInfo.About;
            }

            await userDbContext.SaveChangesAsync();

            return Ok(currentUserInfo);

        }
    }
}
