namespace BugTracker.Services.Project;

using BugTracker.Models;
using BugTracker.Models.Project;
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

 
    public async Task<string> CreateProject(Project project)
    {
        try
        {
            await _projectCollection.InsertOneAsync(project);

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return project.ProjectId;
    }

    public async Task<string> DeleteProject(string ProjectId)
    {
        try
        {
            FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
            await _projectCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return ProjectId;
    }

    public async Task<List<Project>> GetAllProject()
    {
        return await _projectCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Project> GetByProjectId(string ProjectId)
    {
        FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
        return await _projectCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Project> UpdateProjectDetails(Project project, string ProjectId)
    {
        FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
        UpdateDefinition<Project> update = Builders<Project>.Update
            .Set("ProjectName", project.ProjectName)
            .Set("Description",project.Description)
            .Set("Tags", project.Tags)
            .Set("Version", project.Version)
            .Set("UpdatedAt", project.UpdatedAt);

        await _projectCollection.UpdateOneAsync(filter, update);
        var updatedProject = await _projectCollection.Find(filter).FirstOrDefaultAsync();

        return updatedProject;
    }
    public async Task<List<string>> AddUserToProject(string UserId, string ProjectId)
    {
        FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
        UpdateDefinition<Project> update = Builders<Project>.Update.AddToSet("Contributors", UserId);

        await _projectCollection.UpdateOneAsync(filter, update);

        var updatedProject = await _projectCollection.Find(filter).FirstOrDefaultAsync();
        var updatedList = updatedProject.Contributors;
        return updatedList;
    }

    public async Task<List<string>> RemoveUserFromProject(string UserId, string ProjectId)
    {
        FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("ProjectId", ProjectId);
        var updatedProject = await _projectCollection.Find(filter).FirstOrDefaultAsync();
        var updatedList = updatedProject.Contributors;
        updatedList.Remove(UserId);

        UpdateDefinition<Project> update = Builders<Project>.Update.Set("Contributors", updatedList);

        await _projectCollection.UpdateOneAsync(filter, update);

        return updatedList;
    }
}

