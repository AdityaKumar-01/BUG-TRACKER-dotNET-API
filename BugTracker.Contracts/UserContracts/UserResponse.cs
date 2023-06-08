namespace BugTracker.Contracts.UserContracts
{
    public record UserResponse
    (
        string UserId,
        string Name,
        string Email
    );
}