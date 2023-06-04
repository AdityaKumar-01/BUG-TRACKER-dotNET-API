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
        private static Random random = new Random();
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 24)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // GET: api/v1/User
        [HttpGet]
        public Task<List<User>> GetAllUser()
        {
            return _userService.GetAllUser();
        }

        // GET: api/v1/User/:UserId
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetByUserId(string UserId)
        {
            var response = await _userService.GetByUserId(UserId);

            return Ok(response);
        }

        // POST: api/v1/User
        [HttpPost]
        public async Task<IActionResult> Join(JoinUserRequest request)
        {
            var user = new User(RandomString(), request.password);
            var response = await _userService.Join(user);

            return CreatedAtAction(nameof(Join), new {id= request.UserId}, response);
        }

        // PUT: api/v1/User/
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpsertUserRequest request)
        {
            var user = new User(request.UserId, request.password);

            var response = await _userService.UpdateUser(user, user.UserId);

            return Ok(response);
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
