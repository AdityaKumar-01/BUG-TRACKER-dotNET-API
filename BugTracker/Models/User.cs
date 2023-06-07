
using MongoDB.Bson.Serialization.Attributes;

namespace BugTracker.Models.User
{
    public class User
    {
        [BsonId]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<string> ContributorOfProject { get; set; }
        public List<string> AssginedIssue { get; set; }

        // For creating User
        public User(string userId, string name, string password, string email, List<string> contributorOfProject, List<string> assginedIssue)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            ContributorOfProject = contributorOfProject ?? throw new ArgumentNullException(nameof(contributorOfProject));
            AssginedIssue = assginedIssue ?? throw new ArgumentNullException(nameof(assginedIssue));
        }

        // For updating user details
        public User(string name, string password)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
