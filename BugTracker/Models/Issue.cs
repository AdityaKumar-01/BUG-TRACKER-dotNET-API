using MongoDB.Bson.Serialization.Attributes;

namespace BugTracker.Models.Issue;
public class Issue
{
    [BsonId]
    public string IssueId { get; set; }
    public string IssueName { get; set; }
    public string IssueDescription { get; set; }
    public string IssueType { get; set; }
    public List<string> AssignedTo { get; set; }
    public string CreatedBy { get; set; }
    public string CurrentStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Issue(string IssueId,
        string IssueName,
        string IssueDescription,
        string IssueType,
        List<string> assignedTo,
        string createdBy,
        string currentStatus,
        DateTime createdAt,
        DateTime updatedAt)
    {
        this.IssueId = IssueId ?? throw new ArgumentNullException(nameof(IssueId));
        this.IssueName = IssueName ?? throw new ArgumentNullException(nameof(IssueName));
        this.IssueDescription = IssueDescription ?? throw new ArgumentNullException(nameof(IssueDescription));
        this.IssueType = IssueType ?? throw new ArgumentNullException(nameof(IssueType));
        AssignedTo = assignedTo ?? throw new ArgumentNullException(nameof(assignedTo));
        CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        CurrentStatus = currentStatus ?? throw new ArgumentNullException(nameof(currentStatus));
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Issue(string issueName, string issueDescription, string issueType, DateTime updatedAt)
    {
        IssueName = issueName ?? throw new ArgumentNullException(nameof(issueName));
        IssueDescription = issueDescription ?? throw new ArgumentNullException(nameof(issueDescription));
        IssueType = issueType ?? throw new ArgumentNullException(nameof(issueType));
        UpdatedAt = updatedAt;
    }
}
