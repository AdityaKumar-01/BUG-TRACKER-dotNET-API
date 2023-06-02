namespace BugTracker.Models.User
{
    public class User
    {
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
