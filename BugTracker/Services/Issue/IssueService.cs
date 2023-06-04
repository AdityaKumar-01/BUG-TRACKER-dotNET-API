namespace BugTracker.Services.Issue;

using BugTracker.Models;
using BugTracker.Models.Issue;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

public class IssueService:IIssueService
{
    private readonly IMongoCollection<Issue> _issueCollection;
    public IssueService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _issueCollection = database.GetCollection<Issue>(mongoDBSettings.Value.CollectionName[2]);
    }
    public async Task<List<string>> AddUserToIssue(string UserId, string IssueId)
    {
        FilterDefinition<Issue> filter = Builders<Issue>.Filter.Eq("IssueId", IssueId);
        UpdateDefinition<Issue> update = Builders<Issue>.Update.AddToSet("AssignedTo", UserId);

        await _issueCollection.UpdateOneAsync(filter, update);

        var updatedProject = await _issueCollection.Find(filter).FirstOrDefaultAsync();
        var updatedList = updatedProject.AssignedTo;
        return updatedList;
    }

    public async Task<List<string>> RemoveUserFromIssue(string UserId, string IssueId)
    {
        FilterDefinition<Issue> filter = Builders<Issue>.Filter.Eq("IssueId", IssueId);
        var updatedIssue = await _issueCollection.Find(filter).FirstOrDefaultAsync();
        var updatedList = updatedIssue.AssignedTo;
        updatedList.Remove(UserId);

        UpdateDefinition<Issue> update = Builders<Issue>.Update.Set("AssignedTo", updatedList);

        await _issueCollection.UpdateOneAsync(filter, update);

        return updatedList;
    }
    public async Task<string> CreateIssue(Issue issue)
    {
        try
        {
            await _issueCollection.InsertOneAsync(issue);

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return issue.IssueId;
    }

    public async Task<string> DeleteIssue(string IssueId)
    {
        try
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.Eq("IssueId", IssueId);
            await _issueCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return IssueId;
    }

    public async Task<List<Issue>> GetAllIssue()
    {
        return await _issueCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Issue> GetByIssueId(string IssueId)
    {
        FilterDefinition<Issue> filter = Builders<Issue>.Filter.Eq("IssueId", IssueId);
        return await _issueCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Issue> UpdateIssueDetails(Issue issue, string IssueId)
    {

        FilterDefinition<Issue> filter = Builders<Issue>.Filter.Eq("IssueId", IssueId);
        UpdateDefinition<Issue> update = Builders<Issue>.Update
            .Set("IssueName", issue.IssueName)
            .Set("IssueDescription", issue.IssueDescription)
            .Set("IssueType", issue.IssueType)
            .Set("UpdatedAt", issue.UpdatedAt);

        await _issueCollection.UpdateOneAsync(filter, update);
        var updatedIssue = await _issueCollection.Find(filter).FirstOrDefaultAsync();

        return updatedIssue;
    }
}


