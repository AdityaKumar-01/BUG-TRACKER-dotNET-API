namespace BugTracker.Services.Project;
using BugTracker.Models.Project;

public interface IProjectService
{
    Task<List<Project>> GetAllProject();
    Task<Project> GetByProjectId(string ProjectId);
    Task<string> CreateProject(Project project);
    Task<Project> UpdateProjectDetails(Project project, string ProjectId);
    Task<string> DeleteProject(string ProjectId);
    Task<List<string>> AddUserToProject(string UserId, string ProjectId);
    Task<List<string>> RemoveUserFromProject(string UserId, string ProjectId);

}

