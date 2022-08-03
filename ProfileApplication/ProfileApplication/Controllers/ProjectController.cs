using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileApplication.Data;
using ProfileApplication.Models;

namespace ProfileApplication.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly UserDbContext userDbContext;

        public ProjectController(UserDbContext userDbContext) {
            this.userDbContext = userDbContext;
        }

        [HttpGet]
        [Route("{projectId}")]
        [ActionName("GetAllUsersProjects")]
        public async Task<IActionResult> GetAllUsersProjects([FromRoute] string projectId){

            var projects = await userDbContext.Projects.FromSqlRaw("Select * from projects where projectId = {0}", projectId).ToListAsync();

            if (projects == null)
            {
                return NotFound();
            }
            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(Project project) {

            await userDbContext.Projects.AddAsync(project);
            await userDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllUsersProjects), new { ProjectId = project.ProjectId }, project);
        }
    }
}
