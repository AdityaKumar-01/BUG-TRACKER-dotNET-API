# BUG TRACKER API 🐛

A .NET API for handling events in a bug tracker project. Supports 3 entity User, Project and Issue.
Have multiple routes for handling data associated with them.
This API uses NoSQL MongoDB Atlas and can be connected to your personal DB on MongoDB Atlas.


## API Reference 📝
This API has 2 versions, the first one i.e., V1 can be accessed on V1 branch. V1 containns all the basic events and less attributes. V2 can be accessed on V2 branch as well as on master branch. The following link is of postman, this contains all neccessary details on how to make api calls with reference example body provided in them.
[API definition](https://www.postman.com/lively-space-116671/workspace/bugtracker-api/collection/16201122-2ba710c1-724a-457c-8d89-68c7eaf2b193?action=share&creator=16201122)

## Connection with MongoDB Atlas 🗃️
- Create a MongoDB Account [here](https://www.mongodb.com/cloud/atlas/register)
- Create your free cluster and remember/download your credential for connection
- Get the connection string from <b>Connect</b> tab of your cluster.
Looks like this 
```bash
mongodb+srv://<your_username>:<your_password>@cluster0.6vz9uxx.mongodb.net/?retryWrites=true&w=majority
```
- in BugTracker/appsettings.json you will find following section 
```bash
"MongoDB": {
    "ConnectionString": "<your_connection_string_with_username_and_password>",
    "DatabaseName": "<your_DB_name>",
    "CollectionName": [ "User", "Project", "Issue" ]
  },
```
## Run locally 💻
Inside BugTracker Folder
- Install .NET SDK 6
- Run follwing commands to find packages and isnatll them using VS Code or cli
```bash
 dotnet list package
 ```
 - To run project 
 ```bash
 dotnet run
 ```

