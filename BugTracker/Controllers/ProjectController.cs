using BugTracker.Services.Project;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Models.Project;
using BugTracker.Contracts.ProjectContracts;
namespace BugTracker.Controllers
{
    //[Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        private static Random random = new Random();
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // GET: api/v1/<ProjectController>
        [HttpGet("api/v1/project")]
        public List<Project> GetAllProject()
        {
            return _projectService.GetAllProject();
        }

        // GET api/<ProjectController>/poj1123
        [HttpGet("api/v1/project/{ProjectId}")]
        public IActionResult GetByPorjectId(string ProjectId)
        {

            return Ok(_projectService.GetByProjectId(ProjectId));
        }

        // POST api/<ProjectController>
        [HttpPost("api/v1/project")]
        public IActionResult CreateProject(CreateProjectRequest request)
        {
            var ProjectId = RandomString();
            var project = new Project(
                ProjectId,
                request.ProjectName,
                request.Description,
                request.Version,
                request.OwnerId,
                request.OwnerName,
                request.CreatedAt,
                request.UpdatedAt,
                request.Contributors,
                request.Tags);
            var response = _projectService.CreateProject(project);

            return CreatedAtAction(
                actionName: nameof(CreateProject),
                routeValues: ProjectId,
                value: response);
        }

        // PUT api/<ProjectController>/5
        [HttpPatch("api/v1/project/{ProjectId}")]
        public IActionResult UpdateProjectDetails(UpdateProjectDetailsRequest request, string ProjectId)
        {
            var project = new Project(
                request.ProjectName,
                request.Description,
                request.Version,
                request.UpdatedAt,
                request.Tags);
            var response = _projectService.UpdateProjectDetails(project,ProjectId);

            return Ok(response);
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("api/v1/project/{ProjectId}")]
        public IActionResult DeleteProject(string ProjectId)
        {
            var response = _projectService.DeleteProject(ProjectId);

            return Ok(response);
        }

        //PUT api/v1/<ProjectController>/addcontributor
        [HttpPatch("api/v1/project/addcontributor")]
        public IActionResult AddContributor(UpdateContributorsRequest request)
        {
            var response = _projectService.AddUserToProject(request.UserId, request.ProjectId);
            return Ok(response);
        }

        //PUT api/v1/<ProjectController>/removecontributor
        [HttpPatch("api/v1/project/removecontributor")]
        public IActionResult RemoveContributor(UpdateContributorsRequest request)
        {
            var response = _projectService.RemoveUserFromProject(request.UserId, request.ProjectId);
            return Ok(response);
        }



    }
}
