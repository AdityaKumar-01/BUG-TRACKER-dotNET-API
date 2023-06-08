using MongoDB.Bson.Serialization.Attributes;

namespace BugTracker.Models.Issue;
public class Issue
{
    [BsonId]
    public string IssueId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public List<string> AssignedTo { get; set; }
    public string CreatedBy { get; set; }
    public string CurrentStatus { get; set; }
    public string BelongsToProject { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Issue(string issueId,
        string name,
        string description,
        string type,
        List<string> assignedTo,
        string createdBy,
        string currentStatus,
        string belongsToProject,
        DateTime createdAt,
        DateTime updatedAt)
    {
        IssueId = issueId ?? throw new ArgumentNullException(nameof(issueId));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Type = type ?? throw new ArgumentNullException(nameof(type));
        AssignedTo = assignedTo ?? throw new ArgumentNullException(nameof(assignedTo));
        CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        CurrentStatus = currentStatus ?? throw new ArgumentNullException(nameof(currentStatus));
        BelongsToProject = belongsToProject;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Issue(string issueName, string issueDescription, string issueType, DateTime updatedAt)
    {
        Name = issueName ?? throw new ArgumentNullException(nameof(issueName));
        Description = issueDescription ?? throw new ArgumentNullException(nameof(issueDescription));
        Type = issueType ?? throw new ArgumentNullException(nameof(issueType));
        UpdatedAt = updatedAt;
    }
}
