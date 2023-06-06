namespace BugTracker.Services.User;
using BugTracker.Models.ServiceResponseType;
using BugTracker.Models.User;


public interface IUserService
{
    Task<ServiceResponseType<User>> Join(User user);
    Task<ServiceResponseType<List<User>>> GetAllUser();
    Task<ServiceResponseType<User>> GetByUserId(string UserId);
    Task<ServiceResponseType<User>> UpdateUserDetails(User user, string UserId);
    Task<ServiceResponseType<List<string>>> AddIdToProjectList(string UserId, string ProjectId);
    Task<ServiceResponseType<List<string>>> RemoveIdFromProjectList(string UserId, string ProjectId);
    Task<ServiceResponseType<List<string>>> AddIdToIssueList(string UserId, string ProjectId);
    Task<ServiceResponseType<List<string>>> RemoveIdFromIssueList(string UserId, string ProjectId);

    Task<ServiceResponseType<User>> DeleteUser(string UserId);
}

