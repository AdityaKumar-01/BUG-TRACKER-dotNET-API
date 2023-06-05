namespace BugTracker.Services.Issue;

using BugTracker.Models;
using BugTracker.Models.Issue;
using BugTracker.Models.ServiceResponseType;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

public class IssueService : IIssueService
{
    private readonly IMongoCollection<Issue> _issueCollection;
    public IssueService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _issueCollection = database.GetCollection<Issue>(mongoDBSettings.Value.CollectionName[2]);
    }
    public async Task<ServiceResponseType<List<string>>> AddUserToIssue(string UserId, string IssueId)
    {
        ServiceResponseType<List<string>> response;
        try
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.Eq("IssueId", IssueId);
            UpdateDefinition<Issue> update = Builders<Issue>.Update.AddToSet("AssignedTo", UserId);

            var result = await _issueCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                response = new ServiceResponseType<List<string>>(404);
            }
            else if (result.ModifiedCount == 0)
            {
                response = new ServiceResponseType<List<string>>(502);
            }
            else
            {
                var updatedIssue = await _issueCollection.Find(filter).FirstOrDefaultAsync();
                var updatedList = updatedIssue.AssignedTo;
                response = new ServiceResponseType<List<string>>(200, updatedList);
            }
        }
        catch (Exception e)
        {
            response = new ServiceResponseType<List<string>>(502);
            return response;
        }
        return response;
    }

    public async Task<ServiceResponseType<List<string>>> RemoveUserFromIssue(string UserId, string IssueId)
    {
        ServiceResponseType<List<string>> response;

        try
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.Eq("IssueId", IssueId);
            var requiredIssue = await _issueCollection.Find(filter).FirstOrDefaultAsync();
            if (requiredIssue != null)
            {
                var updatedList = requiredIssue.AssignedTo;
                updatedList.Remove(UserId);
                UpdateDefinition<Issue> update = Builders<Issue>.Update.Set("AssignedTo", updatedList);
                var result = await _issueCollection.UpdateOneAsync(filter, update);
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
        catch (Exception e)
        {
            response = new ServiceResponseType<List<string>>(502);
            return response;
        }

        return response;
    }
    public async Task<ServiceResponseType<Issue>> CreateIssue(Issue issue)
    {
        ServiceResponseType<Issue> response;
        try
        {
            await _issueCollection.InsertOneAsync(issue);
            response = new ServiceResponseType<Issue>(201, issue);
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<Issue>(502);
            return response;
        }
        return response;
    }

    public async Task<ServiceResponseType<string>> DeleteIssue(string IssueId)
    {
        ServiceResponseType<string> response;

        try
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.Eq("IssueId", IssueId);
            await _issueCollection.DeleteOneAsync(filter);
            response = new ServiceResponseType<string>(200);
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<string>(502);
            return response;
        }
        return response;
    }

    public async Task<ServiceResponseType<List<Issue>>> GetAllIssue()
    {
        ServiceResponseType<List<Issue>> response;
        try
        {
            var result = await _issueCollection.Find(new BsonDocument()).ToListAsync();
            if (result.Count == 0)
            {
                response = new ServiceResponseType<List<Issue>>(204);
            }
            else
            {
                response = new ServiceResponseType<List<Issue>>(200, result);
            }
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<List<Issue>>(502);
            return response;
        }

        return response;
    }

    public async Task<ServiceResponseType<Issue>> GetByIssueId(string IssueId)
    {
        ServiceResponseType<Issue> response;
        try
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.Eq("IssueId", IssueId);
            var result = await _issueCollection.Find(filter).FirstOrDefaultAsync();
            if (result == null)
            {
                response = new ServiceResponseType<Issue>(404);
            }
            else
            {
                response = new ServiceResponseType<Issue>(200, result);
            }
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<Issue>(502);
            return response;
        }
        return response;
    }

    public async Task<ServiceResponseType<Issue>> UpdateIssueDetails(Issue issue, string IssueId)
    {
        ServiceResponseType<Issue> response;

        try
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.Eq("IssueId", IssueId);
            UpdateDefinition<Issue> update = Builders<Issue>.Update
                .Set("IssueName", issue.IssueName)
                .Set("IssueDescription", issue.IssueDescription)
                .Set("IssueType", issue.IssueType)
                .Set("UpdatedAt", issue.UpdatedAt);

            var result = await _issueCollection.UpdateOneAsync(filter, update);
           


            if (result.MatchedCount == 0)
            {
                response = new ServiceResponseType<Issue>(404);
            }
            else if (result.ModifiedCount == 0)
            {
                response = new ServiceResponseType<Issue>(502);
            }
            else
            {
                var updatedIssue = await _issueCollection.Find(filter).FirstOrDefaultAsync();
                response = new ServiceResponseType<Issue>(200, updatedIssue);
            }
        }
        catch (Exception e)
        {
            response = new ServiceResponseType<Issue>(502);
            return response;
        }
        return response;
    }
}


