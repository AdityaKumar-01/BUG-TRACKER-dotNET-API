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
            response = new ServiceResponseType<Project>(502);
            return response;
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
            response = new ServiceResponseType<string>(502);
            return response;
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
            response = new ServiceResponseType<List<Project>>(502);
            return response;
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
            response = new ServiceResponseType<Project>(502);
            return response;
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
                .Set("ProjectName", project.ProjectName)
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
        catch (Exception e)
        {
            response = new ServiceResponseType<Project>(502);
            return response;
        }
        return response;
    }
    public async Task<ServiceResponseType<List<string>>> AddUserToProject(string UserId, string ProjectId)
    {
        ServiceResponseType<List<string>> response;
        try
        {
        FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
        UpdateDefinition<Project> update = Builders<Project>.Update.AddToSet("Contributors", UserId);

        var result = await _projectCollection.UpdateOneAsync(filter, update);

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
                var updatedProject = await _projectCollection.Find(filter).FirstOrDefaultAsync();
                var updatedList = updatedProject.Contributors;
                response = new ServiceResponseType<List<string>>(200, updatedList);
            }
        }catch (Exception e)
        {
            response = new ServiceResponseType<List<string>>(502);
            return response;
        }
        return response;
    }

    public async Task<ServiceResponseType<List<string>>> RemoveUserFromProject(string UserId, string ProjectId)
    {
        ServiceResponseType<List<string>> response;

        try
        {
        FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
        var requiredProject = await _projectCollection.Find(filter).FirstOrDefaultAsync();
        if (requiredProject != null)
            {
                var updatedList = requiredProject.Contributors;
                updatedList.Remove(UserId);
                UpdateDefinition<Project> update = Builders<Project>.Update.Set("Contributors", updatedList);
                var result = await _projectCollection.UpdateOneAsync(filter, update);
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
            response = new ServiceResponseType<List<string>>(502);
            return response;
        }

        return response;
    }
}

