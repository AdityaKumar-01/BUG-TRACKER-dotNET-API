namespace BugTracker.Services.User;
using BugTracker.Models.User;
using System;
using System.Collections.Generic;
using BugTracker.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

public class UserService : IUserService
{
    private readonly IMongoCollection<User> _userCollection;
    public UserService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _userCollection = database.GetCollection<User>(mongoDBSettings.Value.CollectionName[0]);
    }

    

    public async Task<List<User>> GetAllUser()
    {
        //var response = new List<User>(_users.Values);
        //return response;
        return await _userCollection.Find(new BsonDocument()).ToListAsync();
        
    }

    public async Task<User> GetByUserId(string UserId)
    {
        FilterDefinition<User> filterDefinition = Builders<User>.Filter.Eq("UserId",UserId);
        return await _userCollection.Find(filterDefinition).FirstOrDefaultAsync();
    }

    public async Task<string> Join(User user)
    {
        try
        {
            await _userCollection.InsertOneAsync(user);
        }catch (Exception ex)
        {
            return ex.Message;
        }
        return user.UserId;
    }
    public async Task<User> UpdateUser(User user, string UserId)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserId",UserId);
        UpdateDefinition<User> update = Builders<User>.Update.Set("UserId",user.UserId).Set("password",user.password);
        await _userCollection.UpdateOneAsync(filter,update);
        filter = Builders<User>.Filter.Eq("UserId", user.UserId);
        var response= await _userCollection.Find(filter).FirstOrDefaultAsync();
        return response;
    }
    public async Task DeleteUser(string UserId)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserId", UserId);
        await _userCollection.DeleteOneAsync(filter);
    }
}

