using BugTracker.Services.Issue;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Models.Issue;
using BugTracker.Contracts.IssueContracts;
using BugTracker.Models.Project;

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
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // GET: api/v1/<IssueController>
        [HttpGet("api/v1/Issue")]
        public List<Issue> GetAllIssue()
        {
            return _issueService.GetAllIssue();
        }

        // GET api/<IssueController>/poj1123
        [HttpGet("api/v1/Issue/{IssueId}")]
        public IActionResult GetByPorjectId(string IssueId)
        {

            return Ok(_issueService.GetByIssueId(IssueId));
        }

        // POST api/<IssueController>
        [HttpPost("api/v1/Issue")]
        public IActionResult CreateIssue(CreateIssueRequest request)
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
            var response = _issueService.CreateIssue(Issue);

            return CreatedAtAction(
                actionName: nameof(CreateIssue),
                routeValues: IssueId,
                value: response);
        }

        // PUT api/<IssueController>/5
        [HttpPatch("api/v1/Issue/{IssueId}")]
        public IActionResult UpdateIssueDetails(UpdateIssueDetailsRequest request, string IssueId)
        {
            var Issue = new Issue(
                request.IssueName,
                request.IssueDescription,
                request.IssueType,
                request.UpdatedAt);
            var response = _issueService.UpdateIssueDetails(Issue, IssueId);

            return Ok(response);
        }

        // DELETE api/<IssueController>/5
        [HttpDelete("api/v1/Issue/{IssueId}")]
        public IActionResult DeleteIssue(string IssueId)
        {
            var response = _issueService.DeleteIssue(IssueId);

            return Ok(response);
        }

        //PUT api/v1/<IssueController>/addcontributor
        [HttpPatch("api/v1/Issue/addcontributor")]
        public IActionResult AddContributor(UpdateAssignessRequest request)
        {
            var response = _issueService.AddUserToIssue(request.UserId, request.IssueId);
            return Ok(response);
        }

        //PUT api/v1/<IssueController>/removecontributor
        [HttpPatch("api/v1/Issue/removecontributor")]
        public IActionResult RemoveContributor(UpdateAssignessRequest request)
        {
            var response = _issueService.RemoveUserFromIssue(request.UserId, request.IssueId);
            return Ok(response);
        }



    }
}
