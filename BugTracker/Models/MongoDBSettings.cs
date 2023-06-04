namespace BugTracker.Models
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; }
        = string.Empty;
        public List<string> CollectionName { get; set; }
    }
}
