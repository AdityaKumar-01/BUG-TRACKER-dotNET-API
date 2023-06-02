namespace BugTracker.Services.Project;
using BugTracker.Models.Project;
public class ProjectService : IProjectService
{

    private static Dictionary<string, Project> _projects = new Dictionary<string, Project>();

    public List<string> AddUserToProject(string UserId, string ProjectId)
    {
        _projects[ProjectId].Contributors.Add(UserId);
        return _projects[ProjectId].Contributors;
    }

    public List<string> RemoveUserFromProject(string UserId, string ProjectId)
    {
        var users = _projects[ProjectId].Contributors;
        users.Remove(UserId);
        _projects[ProjectId].Contributors = users;
        return _projects[ProjectId].Contributors;
    }
    public string CreateProject(Project project)
    {
        _projects[project.ProjectId] = project;
        return _projects[project.ProjectId].ProjectId;
    }

    public string DeleteProject(string ProjectId)
    {
        _projects.Remove(ProjectId);
        return ProjectId;
    }

    public List<Project> GetAllProject()
    {
        var response = new List<Project>(_projects.Values);
        return response;
    }

    public Project GetByProjectId(string ProjectId)
    {
        return _projects[ProjectId];
    }

    public Project UpdateProjectDetails(Project project, string ProjectId)
    {
        _projects[ProjectId].Description = project.Description;
        _projects[ProjectId].ProjectName = project.ProjectName;
        _projects[ProjectId].Tags = project.Tags;
        _projects[ProjectId].Version = project.Version;
        _projects[ProjectId].UpdatedAt = project.UpdatedAt;
        return _projects[ProjectId];
    }
}

