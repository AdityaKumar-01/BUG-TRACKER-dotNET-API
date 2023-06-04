using BugTracker.Services.User;
using BugTracker.Services.Project;
using BugTracker.Services.Issue;
using BugTracker.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
    builder.Services.AddSingleton<UserService>();

    builder.Services.AddControllers();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<IIssueService, IssueService>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}