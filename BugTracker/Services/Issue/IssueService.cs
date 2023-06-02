namespace BugTracker.Services.Issue;
using BugTracker.Models.Issue;

public class IssueService:IIssueService
{
    private static Dictionary<string, Issue> _issues = new Dictionary<string, Issue>();

    public List<string> AddUserToIssue(string UserId, string IssueId)
    {
        _issues[IssueId].AssignedTo.Add(UserId);
        return _issues[IssueId].AssignedTo;
    }

    public List<string> RemoveUserFromIssue(string UserId, string IssueId)
    {
        var users = _issues[IssueId].AssignedTo;
        users.Remove(UserId);
        _issues[IssueId].AssignedTo = users;
        return _issues[IssueId].AssignedTo;
    }
    public string CreateIssue(Issue Issue)
    {
        _issues[Issue.IssueId] = Issue;
        return _issues[Issue.IssueId].IssueId;
    }

    public string DeleteIssue(string IssueId)
    {
        _issues.Remove(IssueId);
        return IssueId;
    }

    public List<Issue> GetAllIssue()
    {
        var response = new List<Issue>(_issues.Values);
        return response;
    }

    public Issue GetByIssueId(string IssueId)
    {
        return _issues[IssueId];
    }

    public Issue UpdateIssueDetails(Issue Issue, string IssueId)
    {
        _issues[IssueId].IssueDescription = Issue.IssueDescription;
        _issues[IssueId].IssueName = Issue.IssueName;
        _issues[IssueId].IssueType = Issue.IssueType;
        _issues[IssueId].UpdatedAt = Issue.UpdatedAt;
        return _issues[IssueId];
    }
}


