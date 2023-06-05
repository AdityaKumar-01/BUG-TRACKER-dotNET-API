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
            response = new ServiceResponseType<List<User>>(502);
            return response;
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
            response = new ServiceResponseType<User>(502);
        }
        return response;
    }

    public async Task<ServiceResponseType<User>> Join(User user)
    {
        ServiceResponseType<User> response;
        try
        {
            await _userCollection.InsertOneAsync(user);
            var data = new User(user.UserId);
            response = new ServiceResponseType<User>(201, data);
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<User>(502);
            return response;
        }
        return response;
    }
    public async Task<ServiceResponseType<User>> UpdateUser(User user, string UserId)
    {
        ServiceResponseType<User> response;
        try
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserId", UserId);
            UpdateDefinition<User> update = Builders<User>.Update.Set("UserId", user.UserId).Set("password", user.password);
            await _userCollection.UpdateOneAsync(filter, update);
            filter = Builders<User>.Filter.Eq("UserId", user.UserId);
            var result = await _userCollection.Find(filter).FirstOrDefaultAsync();
            response = new ServiceResponseType<User>(200, result);

        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<User>(502);
            return response;
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
            response = new ServiceResponseType<User>(502);
            return response;
        }
        return response;
    }
}

