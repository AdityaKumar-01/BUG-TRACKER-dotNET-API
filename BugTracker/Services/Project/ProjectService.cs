namespace BugTracker.Services.Project;

using BugTracker.Models;
using BugTracker.Models.Project;
using BugTracker.Models.ServiceResponseType;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

public class ProjectService : IProjectService
{
    private readonly IMongoCollection<Project> _projectCollection;
    public ProjectService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _projectCollection = database.GetCollection<Project>(mongoDBSettings.Value.CollectionName[1]);
    }
    public async Task<ServiceResponseType<Project>> CreateProject(Project project)
    {
        ServiceResponseType<Project> response;
        try
        {
            await _projectCollection.InsertOneAsync(project);
            response = new ServiceResponseType<Project>(201, project);
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<Project>(502, ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponseType<List<Project>>> GetAllProject()
    {
        ServiceResponseType<List<Project>> response;
        try
        {
            var result = await _projectCollection.Find(new BsonDocument()).ToListAsync();
            if (result.Count == 0)
            {
                response = new ServiceResponseType<List<Project>>(204);
            }
            else
            {
                response = new ServiceResponseType<List<Project>>(200, result);
            }
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<List<Project>>(502, ex.Message);
        }

        return response;
    }

    public async Task<ServiceResponseType<Project>> GetByProjectId(string ProjectId)
    {
        ServiceResponseType<Project> response;
        try
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
            var result = await _projectCollection.Find(filter).FirstOrDefaultAsync();
            if (result == null)
            {
                response = new ServiceResponseType<Project>(404);
            }
            else
            {
                response = new ServiceResponseType<Project>(200, result);
            }
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<Project>(502, ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponseType<Project>> UpdateProjectDetails(Project project, string ProjectId)
    {
        ServiceResponseType<Project> response;

        try
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
            UpdateDefinition<Project> update = Builders<Project>.Update
                .Set("Name", project.Name)
                .Set("Description", project.Description)
                .Set("Tags", project.Tags)
                .Set("Version", project.Version)
                .Set("UpdatedAt", project.UpdatedAt);
            var result = await _projectCollection.UpdateOneAsync(filter, update);


            if (result.MatchedCount == 0)
            {
                response = new ServiceResponseType<Project>(404);
            }
            else if (result.ModifiedCount == 0)
            {
                response = new ServiceResponseType<Project>(502);
            }
            else
            {
                var updatedProject = await _projectCollection.Find(filter).FirstOrDefaultAsync();
                response = new ServiceResponseType<Project>(200, updatedProject);
            }
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<Project>(502, ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponseType<Dictionary<string, Dictionary<string, string>>>> AddUserToProject(string UserId, string ProjectId, Dictionary<string, string> updatePayload)
    {
        ServiceResponseType<Dictionary<string, Dictionary<string, string>>> response;
        try
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
            var ProjectToBeUpdated = await _projectCollection.Find(filter).FirstOrDefaultAsync();
            var ContributorDictionary = ProjectToBeUpdated.Contributors;
            ContributorDictionary[UserId] = updatePayload;
            UpdateDefinition<Project> update = Builders<Project>.Update.Set("Contributors", ContributorDictionary);

            var result = await _projectCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                response = new ServiceResponseType<Dictionary<string, Dictionary<string, string>>>(404);
            }
            else if (result.ModifiedCount == 0)
            {
                response = new ServiceResponseType<Dictionary<string, Dictionary<string, string>>>(502);
            }
            else
            {
                response = new ServiceResponseType<Dictionary<string, Dictionary<string, string>>>(200, ContributorDictionary);
            }
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<Dictionary<string, Dictionary<string, string>>>(502, ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponseType<Dictionary<string, Dictionary<string, string>>>> RemoveUserFromProject(string UserId, string ProjectId)
    {
        ServiceResponseType<Dictionary<string, Dictionary<string, string>>> response;

        try
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
            var requiredProject = await _projectCollection.Find(filter).FirstOrDefaultAsync();
            if (requiredProject != null)
            {
                var updatedAttribute = requiredProject.Contributors;
                updatedAttribute.Remove(UserId);
                var updatedList = updatedAttribute.Keys.ToList();
                UpdateDefinition<Project> update = Builders<Project>.Update.Set("Contributors", updatedList);
                var result = await _projectCollection.UpdateOneAsync(filter, update);
                if (result.IsAcknowledged == false)
                {
                    response = new ServiceResponseType<Dictionary<string, Dictionary<string, string>>>(502);
                }
                else
                {
                    response = new ServiceResponseType<Dictionary<string, Dictionary<string, string>>>(200, updatedAttribute);
                }
            }
            else
            {
                response = new ServiceResponseType<Dictionary<string, Dictionary<string, string>>>(404);
            }
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<Dictionary<string, Dictionary<string, string>>>(502, ex.Message);
        }

        return response;
    }

    public async Task<ServiceResponseType<List<string>>> AddIssueToProject(string IssueId, string ProjectId)
    {
        ServiceResponseType<List<string>> response;
        try
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
            UpdateDefinition<Project> update = Builders<Project>.Update.AddToSet("HasIssue", IssueId);

            var result = await _projectCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                response = new ServiceResponseType<List<string>>(404);
            }
            else
            {
                var updatedProject = await _projectCollection.Find(filter).FirstOrDefaultAsync();
                var updatedList = updatedProject.HasIssue;
                response = new ServiceResponseType<List<string>>(200, updatedList);
            }
        }
        catch (Exception e)
        {
            response = new ServiceResponseType<List<string>>(502, e.Message);
        }
        return response;
    }

    public async Task<ServiceResponseType<List<string>>> RemoveIssueFromProject(string IssueId, string ProjectId)
    {
        ServiceResponseType<List<string>> response;

        try
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
            var requiredProject = await _projectCollection.Find(filter).FirstOrDefaultAsync();
            if (requiredProject != null)
            {
                var updatedList = requiredProject.HasIssue;
                updatedList.Remove(IssueId);
                UpdateDefinition<Project> update = Builders<Project>.Update.Set("HasIssue", updatedList);
                var result = await _projectCollection.UpdateOneAsync(filter, update);
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
            response = new ServiceResponseType<List<string>>(502, e.Message);
        }

        return response;
    }

    public async Task<ServiceResponseType<string>> DeleteProject(string ProjectId)
    {
        ServiceResponseType<string> response;

        try
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
            await _projectCollection.DeleteOneAsync(filter);
            response = new ServiceResponseType<string>(200);
        }
        catch (Exception ex)
        {
            response = new ServiceResponseType<string>(502, ex.Message);
        }
        return response;
    }
}

