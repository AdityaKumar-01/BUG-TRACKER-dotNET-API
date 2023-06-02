namespace BugTracker.Services.Issue;
using BugTracker.Models.Issue;

public interface IIssueService
{
    List<string> AddUserToIssue(string UserId, string IssueId);
    List<string> RemoveUserFromIssue(string UserId, string IssueId);
    string CreateIssue(Issue issue);
    string DeleteIssue(string IssueId);
    List<Issue> GetAllIssue();
    Issue GetByIssueId(string IssueId);
    Issue UpdateIssueDetails(Issue issue, string IssueId);

}
