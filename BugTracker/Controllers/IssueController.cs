using BugTracker.Services.Issue;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Models.Issue;
using BugTracker.Contracts.IssueContracts;

namespace BugTracker.Controllers
{
    //[Route("api/v1/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssueController(IIssueService IssueService)
        {
            _issueService = IssueService;
        }
        private static Random random = new Random();
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 24)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // GET: api/v1/<IssueController>
        [HttpGet("api/v1/Issue")]
        public async Task<List<Issue>> GetAllIssue()
        {
            return await _issueService.GetAllIssue();
        }

        // GET api/<IssueController>/poj1123
        [HttpGet("api/v1/Issue/{IssueId}")]
        public async Task<IActionResult> GetByPorjectId(string IssueId)
        {

            return Ok(await _issueService.GetByIssueId(IssueId));
        }

        // POST api/<IssueController>
        [HttpPost("api/v1/Issue")]
        public async Task<IActionResult> CreateIssue(CreateIssueRequest request)
        {
            var IssueId = RandomString();
            var Issue = new Issue(
                IssueId,
                request.IssueName,
                request.IssueDescription,
                request.IssueType,
                request.AssignedTo,
                request.CreatedBy,
                request.CurrentStatus,
                request.CreatedAt,
                request.UpdatedAt);
            var response = await _issueService.CreateIssue(Issue);

            return CreatedAtAction(
                actionName: nameof(CreateIssue),
                routeValues: IssueId,
                value: response);
        }

        // PUT api/<IssueController>/5
        [HttpPatch("api/v1/Issue/{IssueId}")]
        public async Task<IActionResult> UpdateIssueDetails(UpdateIssueDetailsRequest request, string IssueId)
        {
            var Issue = new Issue(
                request.IssueName,
                request.IssueDescription,
                request.IssueType,
                request.UpdatedAt);
            var response = await _issueService.UpdateIssueDetails(Issue, IssueId);

            return Ok(response);
        }

        // DELETE api/<IssueController>/5
        [HttpDelete("api/v1/Issue/{IssueId}")]
        public async Task<IActionResult> DeleteIssue(string IssueId)
        {
            var response = await _issueService.DeleteIssue(IssueId);

            return Ok(response);
        }

        //PUT api/v1/<IssueController>/addcontributor
        [HttpPatch("api/v1/Issue/addcontributor")]
        public async Task<IActionResult> AddContributor(UpdateAssignessRequest request)
        {
            var response = await _issueService.AddUserToIssue(request.UserId, request.IssueId);
            return Ok(response);
        }

        //PUT api/v1/<IssueController>/removecontributor
        [HttpPatch("api/v1/Issue/removecontributor")]
        public async Task<IActionResult> RemoveContributor(UpdateAssignessRequest request)
        {
            var response = await _issueService.RemoveUserFromIssue(request.UserId, request.IssueId);
            return Ok(response);
        }



    }
}
