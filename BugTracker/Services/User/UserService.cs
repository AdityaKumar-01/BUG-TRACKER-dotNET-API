namespace BugTracker.Services.User;
using BugTracker.Models.User;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BugTracker.Contracts;

public class UserService : IUserService
{

    private static Dictionary<string, User> _users = new Dictionary<string, User>();
    public void DeleteUser(string UserId)
    {
        _users.Remove(UserId);
    }

    public List<User> GetAllUser()
    {
        var response = new List<User>(_users.Values);
        return response;
    }

    public User GetByUserId(string UserId)
    {
        return _users[UserId];
    }

    public string Join(User user)
    {
        _users[user.UserId] = user;
        return _users[user.UserId].UserId;
    }
    public User UpdateUser(User user, string UserId)
    {
        _users[UserId] = user;
        return _users[UserId];
    }
}

