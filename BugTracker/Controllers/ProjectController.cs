using BugTracker.Services.Project;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Models.Project;
using BugTracker.Contracts.ProjectContracts;
using BugTracker.Models.ServiceResponseType;
using Amazon.Runtime.Internal;

namespace BugTracker.Controllers
{
    //[Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : HelperAndBaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        // GET: api/v1/<ProjectController>
        [HttpGet("api/v2/project")]
        public async Task<IActionResult> GetAllProject()
        {
            ServiceResponseType<List<Project>> response = await _projectService.GetAllProject();
            return ControllerResponse(response.StatusCode, response.Payload);
        }

        // GET api/<ProjectController>/poj1123
        [HttpGet("api/v2/project/{ProjectId}")]
        public async Task<IActionResult> GetByPorjectId(string ProjectId)
        {
            ServiceResponseType<Project> response = await _projectService.GetByProjectId(ProjectId);
            return ControllerResponse(response.StatusCode, response.Payload);
        }

        // POST api/<ProjectController>
        [HttpPost("api/v2/project")]
        public async Task<IActionResult> CreateProject(CreateProjectRequest request)
        {
            var ProjectId = RandomString();
            var project = new Project(
                ProjectId,
                request.Name,
                request.Description,
                request.Version,
                request.OwnerId,
                request.OwnerName,
                request.CreatedAt,
                request.UpdatedAt,
                request.Contributors,
                request.HasIssue,
                request.Tags);
            ServiceResponseType<Project> response = await _projectService.CreateProject(project);

            return ControllerResponse(response.StatusCode, response.Payload, nameof(CreateProject), ProjectId);
        }

        // PUT api/<ProjectController>/5
        [HttpPatch("api/v2/project/{ProjectId}")]
        public async Task<IActionResult> UpdateProjectDetails(UpdateProjectDetailsRequest request, string ProjectId)
        {
            var project = new Project(
                request.Name,
                request.Description,
                request.Version,
                request.UpdatedAt,
                request.Tags);
            ServiceResponseType<Project> response = await _projectService.UpdateProjectDetails(project, ProjectId);

            return ControllerResponse(response.StatusCode, response.Payload);
        }

        //PUT api/v1/<ProjectController>/add-contributor
        [HttpPatch("api/v2/project/add-contributor")]
        public async Task<IActionResult> AddContributor(AddContributorRequest request)
        {
            Dictionary<string, string> updatePayload = new Dictionary<string, string>();
            updatePayload["Name"] = request.UserName;
            updatePayload["Role"] = request.Role;
            ServiceResponseType<Dictionary<string, Dictionary<string, string>>> response = await _projectService.AddUserToProject(request.UserId, request.ProjectId, updatePayload);
            return ControllerResponse(response.StatusCode, response.Payload);
        }

        //PUT api/v1/<ProjectController>/remove-contributor
        [HttpPatch("api/v2/project/remove-contributor")]
        public async Task<IActionResult> RemoveContributor(RemoveContributorFromProjectRequest request)
        {
            ServiceResponseType<Dictionary<string, Dictionary<string, string>>> response = await _projectService.RemoveUserFromProject(request.UserId, request.ProjectId);
            return ControllerResponse(response.StatusCode, response.Payload);
        }

        //PUT api/v1/<ProjectController>/add-issue
        [HttpPatch("api/v2/project/add-issue")]
        public async Task<IActionResult> AddIssue(UpdateIssueInProjectRequest request)

        {
            ServiceResponseType<List<string>> response = await _projectService.AddIssueToProject(request.IssueId, request.ProjectId);
            return ControllerResponse(response.StatusCode, response.Payload);
        }

        //PUT api/v1/<ProjectController>/remove-issue
        [HttpPatch("api/v2/project/remove-issue")]
        public async Task<IActionResult> RemoveIssue(UpdateIssueInProjectRequest request)
        {
            ServiceResponseType<List<string>> response = await _projectService.RemoveIssueFromProject(request.IssueId, request.ProjectId);
            return ControllerResponse(response.StatusCode, response.Payload);
        }
        // DELETE api/<ProjectController>/5
        
        [HttpDelete("api/v2/project/{ProjectId}")]
        public async Task<IActionResult> DeleteProject(string ProjectId)
        {
            ServiceResponseType<string> response = await _projectService.DeleteProject(ProjectId);

            return ControllerResponse(response.StatusCode, response.Payload);
        }
    }
}
