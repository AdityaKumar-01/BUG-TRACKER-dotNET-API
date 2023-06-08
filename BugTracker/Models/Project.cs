namespace BugTracker.Models.Project;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Project
{
    [BsonId]
    public string ProjectId { get; set;}
    public string Name { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public string OwnerId { get; set; }
    public string OwnerName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Dictionary<string, Dictionary<string, string>> Contributors { get; set; }
    public List<string> HasIssue { get; set; }
    public List<string> Tags { get; set; }

    public Project(
        string projectId,
        string name,
        string description,
        string version,
        string ownerId,
        string ownerName,
        DateTime createdAt,
        DateTime updatedAt,
        Dictionary<string, Dictionary<string, string>> contributors,
        List<string> hasIssue,
        List<string> tags)
    {
        ProjectId = projectId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Version = version ?? throw new ArgumentNullException(nameof(version));
        OwnerId = ownerId ?? throw new ArgumentNullException(nameof(ownerId));
        OwnerName = ownerName ?? throw new ArgumentNullException(nameof(ownerName));
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Contributors = contributors ?? throw new ArgumentNullException(nameof(contributors));
        HasIssue = hasIssue;
        Tags = tags ?? throw new ArgumentNullException(nameof(tags));
    }
    public Project(string projectName, string description, string version, DateTime updatedAt, List<string> tags)
    {
        Name = projectName;
        Description = description;
        Version = version;
        UpdatedAt = updatedAt;
        Tags = tags;
    }

}

