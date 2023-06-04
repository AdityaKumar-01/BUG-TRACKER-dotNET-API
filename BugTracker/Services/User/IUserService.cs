namespace BugTracker.Services.User;
using BugTracker.Models.User;

public interface IUserService
{
    Task<List<User>> GetAllUser();
    Task<User> GetByUserId(string UserId);
    Task<string> Join(User user);
    Task<User> UpdateUser(User user, string UserId);
    Task DeleteUser(string UserId);
}

