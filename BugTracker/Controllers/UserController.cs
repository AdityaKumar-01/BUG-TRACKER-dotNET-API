using Microsoft.AspNetCore.Mvc;
using BugTracker.Models.User;
using BugTracker.Services.User;
using BugTracker.Contracts.UserContracts;

namespace BugTracker.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/v1/User
        [HttpGet]
        public List<User> GetAllUser()
        {
            return _userService.GetAllUser();
        }

        // GET: api/v1/User/:UserId
        [HttpGet("{UserId}")]
        public IActionResult GetByUserId(string UserId)
        {
            var user = _userService.GetByUserId(UserId);
            var response = new UserResponse(
                user.UserId);

            return Ok(response);
        }

        // POST: api/v1/User
        [HttpPost]
        public IActionResult Join(JoinUserRequest request)
        {
            var user = new User(request.UserId, request.password);
            var response = new UserResponse(_userService.Join(user));

            return Ok(response);
        }

        // PUT: api/v1/User/
        [HttpPut]
        public IActionResult UpdateUser(UpsertUserRequest request)
        {
            var user = new User(request.UserId, request.password);

            var response = _userService.UpdateUser(user, user.UserId);

            return Ok(response.UserId);
        }

        // DELETE: api/v1/User
        [HttpDelete("{UserId}")]
        public IActionResult DeleteUser(string UserId)
        {
            _userService.DeleteUser(UserId);
            return Ok();

        }

    }
}
