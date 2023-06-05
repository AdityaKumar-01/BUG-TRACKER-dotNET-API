namespace BugTracker.Services.Issue;
using BugTracker.Models.Issue;
using BugTracker.Models.ServiceResponseType;

public interface IIssueService
{
    Task<ServiceResponseType<List<string>>> AddUserToIssue(string UserId, string IssueId);
    Task<ServiceResponseType<List<string>>> RemoveUserFromIssue(string UserId, string IssueId);
    Task<ServiceResponseType<Issue>> CreateIssue(Issue issue);
    Task<ServiceResponseType<string>> DeleteIssue(string IssueId);
    Task<ServiceResponseType<List<Issue>>> GetAllIssue();
    Task<ServiceResponseType<Issue>> GetByIssueId(string IssueId);
    Task<ServiceResponseType<Issue>> UpdateIssueDetails(Issue issue, string IssueId);

}
