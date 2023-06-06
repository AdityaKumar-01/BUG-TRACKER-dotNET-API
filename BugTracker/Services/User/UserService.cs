namespace BugTracker.Services.User;
using BugTracker.Models.User;
using System;
using System.Collections.Generic;
using BugTracker.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using BugTracker.Models.ServiceResponseType;

public class UserService : IUserService
{
    private readonly IMongoCollection<User> _userCollection;
    public UserService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _userCollection = database.GetCollection<User>(mongoDBSettings.Value.CollectionName[0]);
    }
    public async Task<ServiceResponseType<User>> Join(User user)
    {
        ServiceResponseType<User> response;
        try
        {
            await _userCollection.InsertOneAsync(user);
            var data = new User(user.UserId, user.Name, user.ContributorOfProject, user.AssginedIssue);
            response = new ServiceResponseType<User>(201, data);
        }
        catch (Exception ex)
        {   
            response = new ServiceResponseType<User>(502,ex.Message);
        }
        return response;
    }
    
    public async Task<ServiceResponseType<List<User>>> GetAllUser()
    {
        ServiceResponseType<List<User>> response;
        try
        {
            var result = await _userCollection.Find(new BsonDocument()).ToListAsync();
            if (result.Count == 0)
            {
                response = new ServiceResponseType<List<User>>(204);
            }
            else
            {
                response = new ServiceResponseType<List<User>>(200, result);
            }
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<List<User>>(502, ex.Message);
        }
        return response;

    }

    public async Task<ServiceResponseType<User>> GetByUserId(string UserId)
    {
        ServiceResponseType<User> response;
        try
        {
            FilterDefinition<User> filterDefinition = Builders<User>.Filter.Eq("UserId", UserId);
            var result = await _userCollection.Find(filterDefinition).FirstOrDefaultAsync();
            if (result == null)
            {
                response = new ServiceResponseType<User>(404);
            }
            else
            {
                response = new ServiceResponseType<User>(200, result);
            }
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<User>(502, ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponseType<User>> UpdateUserDetails(User user, string UserId)
    {
        ServiceResponseType<User> response;
        try
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserId", UserId);
            UpdateDefinition<User> update = Builders<User>.Update.Set("Name", user.Name).Set("Password", user.Password);
            var result = await _userCollection.UpdateOneAsync(filter, update);
            if (result.MatchedCount == 0)
            {
                response = new ServiceResponseType<User>(404);
            }
            else
            {
                var searchResult = await _userCollection.Find(filter).FirstOrDefaultAsync();
                response = new ServiceResponseType<User>(200, searchResult);
            }
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<User>(502, ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponseType<List<string>>> AddIdToProjectList(string UserId, string ProjectId)
    {
        ServiceResponseType<List<string>> response;
        try
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserId", UserId);
            UpdateDefinition<User> update = Builders<User>.Update.AddToSet("ContributorOfProject", ProjectId);

            var result = await _userCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                response = new ServiceResponseType<List<string>>(404);
            }
            else
            {
                var updatedUser = await _userCollection.Find(filter).FirstOrDefaultAsync();
                var updatedList = updatedUser.ContributorOfProject;
                response = new ServiceResponseType<List<string>>(200, updatedList);
            }
        }
        catch (Exception e)
        {
            response = new ServiceResponseType<List<string>>(502,e.Message);
        }
        return response;
    }

    public async Task<ServiceResponseType<List<string>>> RemoveIdFromProjectList(string UserId, string ProjectId)
    {
        ServiceResponseType<List<string>> response;

        try
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserId", UserId);
            var requiredProject = await _userCollection.Find(filter).FirstOrDefaultAsync();
            if (requiredProject != null)
            {
                var updatedList = requiredProject.ContributorOfProject;
                updatedList.Remove(ProjectId);
                UpdateDefinition<User> update = Builders<User>.Update.Set("ContributorOfProject", updatedList);
                var result = await _userCollection.UpdateOneAsync(filter, update);
                if (result.ModifiedCount > 0)
                {
                    response = new ServiceResponseType<List<string>>(200, updatedList);
                }
                else
                {
                    response = new ServiceResponseType<List<string>>(502);
                }
            }
            else
            {
                response = new ServiceResponseType<List<string>>(404);
            }
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<List<string>>(502, ex.Message);
        }

        return response;
    }
    
    public async Task<ServiceResponseType<List<string>>> AddIdToIssueList(string UserId, string IssueId)
    {
        ServiceResponseType<List<string>> response;
        try
        {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserId", UserId);
        UpdateDefinition<User> update = Builders<User>.Update.AddToSet("AssginedIssue", IssueId);

        var result = await _userCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                response = new ServiceResponseType<List<string>>(404);
            }
            else
            {
                var updatedUser = await _userCollection.Find(filter).FirstOrDefaultAsync();
                var updatedList = updatedUser.AssginedIssue;
                response = new ServiceResponseType<List<string>>(200, updatedList);
            }
        }catch (Exception e)
        {
            response = new ServiceResponseType<List<string>>(502,e.Message);
        }
        return response;
    }

    public async Task<ServiceResponseType<List<string>>> RemoveIdFromIssueList(string UserId, string IssueId)
    {
        ServiceResponseType<List<string>> response;

        try
        {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserId", UserId);
        var requiredProject = await _userCollection.Find(filter).FirstOrDefaultAsync();
        if (requiredProject != null)
            {
                var updatedList = requiredProject.AssginedIssue;
                updatedList.Remove(IssueId);
                UpdateDefinition<User> update = Builders<User>.Update.Set("AssginedIssue", updatedList);
                var result = await _userCollection.UpdateOneAsync(filter, update);
                if(result.ModifiedCount > 0)
                {
                    response = new ServiceResponseType<List<string>>(200, updatedList);
                }
                else
                {
                    response = new ServiceResponseType<List<string>>(502);
                }
            }
            else
            {
                response = new ServiceResponseType<List<string>>(404);
            }
        }catch(Exception e)
        {
            response = new ServiceResponseType<List<string>>(502, e.Message);
        }

        return response;
    }
   
    public async Task<ServiceResponseType<User>> DeleteUser(string UserId)
    {
        ServiceResponseType<User> response;
        try
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserId", UserId);
            await _userCollection.DeleteOneAsync(filter);
            response = new ServiceResponseType<User>(204);
        }catch(Exception ex)
        {
            response = new ServiceResponseType<User>(502, ex.Message);
        }
        return response;
    }
}

