namespace BugTracker.Services.Project;
using BugTracker.Models.Project;
using BugTracker.Models.ServiceResponseType;

public interface IProjectService
{
    Task<ServiceResponseType<List<Project>>> GetAllProject();
    Task<ServiceResponseType<Project>> GetByProjectId(string ProjectId);
    Task<ServiceResponseType<Project>> CreateProject(Project project);
    Task<ServiceResponseType<Project>> UpdateProjectDetails(Project project, string ProjectId);
    Task<ServiceResponseType<List<string>>> AddUserToProject(string UserId, string ProjectId, Dictionary<string, string> updatePayload);
    Task<ServiceResponseType<List<string>>> RemoveUserFromProject(string UserId, string ProjectId);
    Task<ServiceResponseType<List<string>>> AddIssueToProject(string IssueId, string ProjectId);
    Task<ServiceResponseType<List<string>>> RemoveIssueFromProject(string IssueId, string ProjectId);
    Task<ServiceResponseType<string>> DeleteProject(string ProjectId);
}

