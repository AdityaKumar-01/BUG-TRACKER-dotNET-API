namespace BugTracker.Services.User;
using BugTracker.Models.ServiceResponseType;
using BugTracker.Models.User;


public interface IUserService
{
    Task<ServiceResponseType<List<User>>> GetAllUser();
    Task<ServiceResponseType<User>> GetByUserId(string UserId);
    Task<ServiceResponseType<User>> Join(User user);
    Task<ServiceResponseType<User>> UpdateUser(User user, string UserId);
    Task<ServiceResponseType<User>> DeleteUser(string UserId);
}

