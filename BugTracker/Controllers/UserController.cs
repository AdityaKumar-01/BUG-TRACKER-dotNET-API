namespace BugTracker.Controllers;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Models.User;
using BugTracker.Services.User;
using BugTracker.Contracts.UserContracts;
using BugTracker.Models.ServiceResponseType;


[ApiController]
//[Route("api/v2/[controller]")]
public class UserController : HelperAndBaseController
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // POST: api/v1/User
    [HttpPost("api/v2/user/sign-up")]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        User user = new User(RandomString(), request.Name, request.Password, request.Email, request.ContributorOfProject, request.AssignedIssue);
        ServiceResponseType<User> response = await _userService.SignUp(user);
        return ControllerResponse(response.StatusCode, response.Payload, nameof(SignUp), user.UserId);
    }

    [HttpPost("api/v2/user/sign-in")]
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
        
        ServiceResponseType<User> response = await _userService.SignIn(request.Email, request.Password);
        UserResponse ControllerPayload = new UserResponse(response.Payload.UserId, response.Payload.Name, response.Payload.Email);
        return ControllerResponse(response.StatusCode, ControllerPayload);
    }

    // GET: api/v1/User
    [HttpGet("api/v2/user")]
    public async Task<IActionResult> GetAllUser()
    {
        ServiceResponseType<List<User>> response = await _userService.GetAllUser();
        return ControllerResponse(response.StatusCode, response.Payload);
    }

    // GET: api/v1/User/:UserId
    [HttpGet("api/v2/user/{UserId}")]
    public async Task<IActionResult> GetByUserId(string UserId)
    {
        ServiceResponseType<User> response = await _userService.GetByUserId(UserId);

        return ControllerResponse(response.StatusCode, response.Payload);
    }

    // PUT: api/v1/User/
    [HttpPatch("api/v2/user/{UserId}")]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsRequest request, string UserId)
    {
        User user = new User(request.Name, request.Password);
        ServiceResponseType<User> response = await _userService.UpdateUserDetails(user,UserId);
        return ControllerResponse(response.StatusCode, response.Payload.Name);
    }
    
    //PATCH: api/v2/user/addtoproject
    [HttpPatch("api/v2/user/addtoproject")]
    public async Task<IActionResult> AddToProject(UpdateProjectListRequest request)
    {
        ServiceResponseType<List<string>> response  = await _userService.AddIdToProjectList(request.UserId, request.ProjectId);
        return ControllerResponse(response.StatusCode, response.Payload);

    }

    //PATCH: api/v2/user/removefromproject
    [HttpPatch("api/v2/user/removefromproject")]
    public async Task<IActionResult> RemoveFromProject(UpdateProjectListRequest request)
    {
        ServiceResponseType<List<string>> response = await _userService.RemoveIdFromProjectList(request.UserId, request.ProjectId);
        return ControllerResponse(response.StatusCode, response.Payload);

    }

    //PATCH: api/v2/user/addissue
    [HttpPatch("api/v2/user/addissue")]
    public async Task<IActionResult> AddIssue(UpdateIssueListRequest request)
    {
        ServiceResponseType<List<string>> response = await _userService.AddIdToIssueList(request.UserId, request.IssueId);
        return ControllerResponse(response.StatusCode, response.Payload);

    }
    
    //PATCH: api/v2/user/removefromproject
    [HttpPatch("api/v2/user/removefromissue")]
    public async Task<IActionResult> RemoveFromIssue(UpdateIssueListRequest request)
    {
        ServiceResponseType<List<string>> response = await _userService.RemoveIdFromIssueList(request.UserId, request.IssueId);
        return ControllerResponse(response.StatusCode, response.Payload);

    }
    
    // DELETE: api/v1/User
    [HttpDelete("api/v2/user/{UserId}")]
    public async Task<IActionResult> DeleteUser(string UserId)
    {
        ServiceResponseType<User> response = await _userService.DeleteUser(UserId);
        return ControllerResponse(response.StatusCode, response.Payload);

    }

}
