using BugTracker.Services.Issue;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Models.Issue;
using BugTracker.Contracts.IssueContracts;
using BugTracker.Models.ServiceResponseType;
using System.Net.Http;
using System.Text;

namespace BugTracker.Controllers
{
    //[Route("api/v1/[controller]")]
    [ApiController]
    public class IssueController : HelperAndBaseController
    {
        private readonly IIssueService _issueService;

        public IssueController(IIssueService IssueService)
        {
            _issueService = IssueService;
        }

        // GET: api/v1/<IssueController>
        [HttpGet("api/v2/issue")]
        public async Task<IActionResult> GetAllIssue()
        {
            ServiceResponseType<List<Issue>> response = await _issueService.GetAllIssue();
            return ControllerResponse(response.StatusCode, response.Payload);
        }

        // GET api/<IssueController>/poj1123
        [HttpGet("api/v2/issue/{IssueId}")]
        public async Task<IActionResult> GetByPorjectId(string IssueId)
        {
            ServiceResponseType<Issue> response = await _issueService.GetByIssueId(IssueId);
            return ControllerResponse(response.StatusCode, response.Payload);
        }

        // POST api/<IssueController>
        [HttpPost("api/v2/issue")]
        public async Task<IActionResult> CreateIssue(CreateIssueRequest request)
        {
            var IssueId = RandomString();
            var Issue = new Issue(
                IssueId,
                request.Name,
                request.Description,
                request.Type,
                request.AssignedTo,
                request.CreatedBy,
                request.CurrentStatus,
                request.BelongsToProject,
                request.CreatedAt,
                request.UpdatedAt);
            ServiceResponseType<Issue> response = await _issueService.CreateIssue(Issue);

            return ControllerResponse(response.StatusCode,response.Payload,nameof(CreateIssue),IssueId);
        }

        // PUT api/<IssueController>/5
        [HttpPatch("api/v2/issue/{IssueId}")]
        public async Task<IActionResult> UpdateIssueDetails(UpdateIssueDetailsRequest request, string IssueId)
        {
            var Issue = new Issue(
                request.IssueName,
                request.IssueDescription,
                request.IssueType,
                request.UpdatedAt);
            ServiceResponseType<Issue> response = await _issueService.UpdateIssueDetails(Issue, IssueId);

            return ControllerResponse(response.StatusCode, response.Payload);
        }

        

        //PUT api/v1/<IssueController>/addcontributor
        [HttpPatch("api/v2/issue/add-contributor")]
        public async Task<IActionResult> AddContributor(UpdateAssignessRequest request)
        {
            ServiceResponseType<List<string>> response = await _issueService.AddUserToIssue(request.UserId, request.IssueId);
            return ControllerResponse(response.StatusCode, response.Payload);
        }

        //PUT api/v1/<IssueController>/removecontributor
        [HttpPatch("api/v2/issue/remove-contributor")]
        public async Task<IActionResult> RemoveContributor(UpdateAssignessRequest request)
        {
            ServiceResponseType<List<string>> response = await _issueService.RemoveUserFromIssue(request.UserId, request.IssueId);
            return ControllerResponse(response.StatusCode, response.Payload);
        }

        // PATCH api 
        [HttpPatch("api/v2/issue/transfer-issue")]
        public async Task<IActionResult> TransferIssue(TransferIssueRequest request)
        {
            ServiceResponseType<string> response = await _issueService.TransferIssue(request.IssueId, request.ToProjectId);
            if(response.StatusCode == 200)
            {
                var httpClient = new HttpClient();
                var payload = new { IssueId = request.IssueId, ProjectId= request.ToProjectId};
                var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var AddResult = await httpClient.PatchAsync("https://localhost:7146/api/v2/project/add-issue", httpContent);

                payload = new { IssueId = request.IssueId, ProjectId = request.FromProjectId };
                jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var RemoveResult = await httpClient.PatchAsync("https://localhost:7146/api/v2/project/remove-issue", httpContent);
                if (AddResult.IsSuccessStatusCode && RemoveResult.IsSuccessStatusCode)
                {
                    return ControllerResponse(200, default(int));
                }
                else
                {
                    response = await _issueService.TransferIssue(request.IssueId, request.FromProjectId);
                    return ControllerResponse(502, default(int));
                }
            }
            else
            {
                return ControllerResponse(response.StatusCode, response.Payload);
            }
        }
        // DELETE api/<IssueController>/5
        [HttpDelete("api/v2/issue/{IssueId}")]
        public async Task<IActionResult> DeleteIssue(string IssueId)
        {
            ServiceResponseType<string> response = await _issueService.DeleteIssue(IssueId);

            return ControllerResponse(response.StatusCode, response.Payload);
        }

    }
}
