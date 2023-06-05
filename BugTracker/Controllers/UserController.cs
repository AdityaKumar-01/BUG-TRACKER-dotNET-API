namespace BugTracker.Controllers;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Models.User;
using BugTracker.Services.User;
using BugTracker.Contracts.UserContracts;
using BugTracker.Models.ServiceResponseType;


[ApiController]
[Route("api/v2/[controller]")]
public class UserController : HelperAndBaseController
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    // GET: api/v1/User
    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        ServiceResponseType<List<User>> response = await _userService.GetAllUser();
        return ControllerResponse(response.StatusCode, response.Payload);
    }

    // GET: api/v1/User/:UserId
    [HttpGet("{UserId}")]
    public async Task<IActionResult> GetByUserId(string UserId)
    {
        ServiceResponseType<User> response = await _userService.GetByUserId(UserId);

        return ControllerResponse(response.StatusCode, response.Payload);
    }

    // POST: api/v1/User
    [HttpPost]
    public async Task<IActionResult> Join(JoinUserRequest request)
    {
        var user = new User(RandomString(), request.password);
        ServiceResponseType<User> response = await _userService.Join(user);
        return ControllerResponse(response.StatusCode, response.Payload, nameof(Join), user.UserId);
    }

    // PUT: api/v1/User/
    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpsertUserRequest request)
    {
        var user = new User(request.UserId, request.password);

        ServiceResponseType<User> response = await _userService.UpdateUser(user, user.UserId);

        return ControllerResponse(response.StatusCode, response.Payload);
    }

    // DELETE: api/v1/User
    [HttpDelete("{UserId}")]
    public async Task<IActionResult> DeleteUser(string UserId)
    {
        ServiceResponseType<User> response = await _userService.DeleteUser(UserId);
        return ControllerResponse(response.StatusCode, response.Payload);

    }

}
