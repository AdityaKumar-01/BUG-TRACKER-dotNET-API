namespace BugTracker.Services.User;
using BugTracker.Models.User;

public interface IUserService
{
    List<User> GetAllUser();
    User GetByUserId(string UserId);
    string Join(User user);
    User UpdateUser(User user, string UserId);
    void DeleteUser(string UserId);
}

