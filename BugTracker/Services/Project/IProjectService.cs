namespace BugTracker.Services.Project;
using BugTracker.Models.Project;

public interface IProjectService
{
    List<Project> GetAllProject();
    Project GetByProjectId(string ProjectId);
    string CreateProject(Project project);
    Project UpdateProjectDetails(Project project, string ProjectId);
    string DeleteProject(string ProjectId);
    List<string> AddUserToProject(string UserId, string ProjectId);
    List<string> RemoveUserFromProject(string UserId, string ProjectId);

}

