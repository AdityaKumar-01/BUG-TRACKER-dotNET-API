namespace BugTracker.Services.Issue;
using BugTracker.Models.Issue;

public interface IIssueService
{
    Task<List<string>> AddUserToIssue(string UserId, string IssueId);
    Task<List<string>> RemoveUserFromIssue(string UserId, string IssueId);
    Task<string> CreateIssue(Issue issue);
    Task<string> DeleteIssue(string IssueId);
    Task<List<Issue>> GetAllIssue();
    Task<Issue> GetByIssueId(string IssueId);
    Task<Issue> UpdateIssueDetails(Issue issue, string IssueId);

}
