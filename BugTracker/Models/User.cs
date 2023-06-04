
using MongoDB.Bson.Serialization.Attributes;
namespace BugTracker.Models.User
{
    public class User
    {
        [BsonId]
        public string UserId { get; }
        public string password { get; }
        //public List<string> ContributorOf { get; }

        public User(string userId, string password)
        {
            this.UserId = userId;
            this.password = password;
        }
    }
}
