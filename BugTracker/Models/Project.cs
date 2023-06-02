namespace BugTracker.Models.Project;
public class Project
{
    public string ProjectId { get; }
    public string ProjectName { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public string OwnerId { get; }
    public string OwnerName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<string> Contributors { get; set; }
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
        List<string> contributors,
        List<string> tags)
    {
        ProjectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
        ProjectName = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Version = version ?? throw new ArgumentNullException(nameof(version));
        OwnerId = ownerId ?? throw new ArgumentNullException(nameof(ownerId));
        OwnerName = ownerName ?? throw new ArgumentNullException(nameof(ownerName));
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Contributors = contributors ?? throw new ArgumentNullException(nameof(contributors));
        Tags = tags ?? throw new ArgumentNullException(nameof(tags));
    }

    public Project(string projectName, string description, string version, DateTime updatedAt, List<string> tags)
    {
        ProjectName = projectName;
        Description = description;
        Version = version;
        UpdatedAt = updatedAt;
        Tags = tags;
    }
}

