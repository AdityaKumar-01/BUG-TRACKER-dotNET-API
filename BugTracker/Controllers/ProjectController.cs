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
            var digits = 24;
            var bytes = new Byte[24];
            random.NextBytes(bytes);

            var hexArray = Array.ConvertAll(bytes, x => x.ToString("X2"));
            var hexStr = String.Concat(hexArray);
            return hexStr;
        }
            // GET: api/v1/<ProjectController>
            [HttpGet("api/v1/project")]
        public async Task<List<Project>> GetAllProject()
        {
            return await _projectService.GetAllProject();
        }

        // GET api/<ProjectController>/poj1123
        [HttpGet("api/v1/project/{ProjectId}")]
        public async Task<IActionResult> GetByPorjectId(string ProjectId)
        {
            return Ok( await _projectService.GetByProjectId(ProjectId));
        }

        // POST api/<ProjectController>
        [HttpPost("api/v1/project")]
        public async Task<IActionResult> CreateProject(CreateProjectRequest request)
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
            var response = await _projectService.CreateProject(project);

            return CreatedAtAction(
                actionName: nameof(CreateProject),
                routeValues: ProjectId,
                value: response);
        }

        // PUT api/<ProjectController>/5
        [HttpPatch("api/v1/project/{ProjectId}")]
        public async Task<IActionResult> UpdateProjectDetails(UpdateProjectDetailsRequest request, string ProjectId)
        {
            var project = new Project(
                request.ProjectName,
                request.Description,
                request.Version,
                request.UpdatedAt,
                request.Tags);
            var response = await _projectService.UpdateProjectDetails(project, ProjectId);

            return Ok(response);
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("api/v1/project/{ProjectId}")]
        public async Task<IActionResult> DeleteProject(string ProjectId)
        {
            var response = await _projectService.DeleteProject(ProjectId);

            return Ok(response);
        }

        //PUT api/v1/<ProjectController>/addcontributor
        [HttpPatch("api/v1/project/addcontributor")]
        public async Task<IActionResult> AddContributor(UpdateContributorsRequest request)
        {
            var response =  await _projectService.AddUserToProject(request.UserId, request.ProjectId);
            return Ok(response);
        }

        //PUT api/v1/<ProjectController>/removecontributor
        [HttpPatch("api/v1/project/removecontributor")]
        public async Task<IActionResult> RemoveContributor(UpdateContributorsRequest request)
        {
            var response = await _projectService.RemoveUserFromProject(request.UserId, request.ProjectId);
            return Ok(response);
        }
    }
}
