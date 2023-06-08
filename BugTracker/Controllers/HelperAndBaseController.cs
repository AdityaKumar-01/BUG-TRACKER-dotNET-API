using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [ApiController]
    public class HelperAndBaseController : Controller
    {
        private static Random random = new Random();
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 24)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public IActionResult ControllerResponse<T>(int statusCode, T payload, string name = "", string id = "")
        {
            if (statusCode == 200)
            {
                return Ok(payload);
            }
            else if (statusCode == 201)
            {
                return CreatedAtAction(actionName: name, routeValues: id, value: payload);
            }
            else if (statusCode == 204)
            {
                return NoContent();
            }
            else if (statusCode == 404)
            {
                return NotFound();
            }
            else if(statusCode == 502)
            {
                return StatusCode(502, "Bad Gateway");
            }else if(statusCode == 409)
            {
                return StatusCode(409, "Conflict with requested data");
            }else if (statusCode == 401)
            {
                return StatusCode(401, "User is unauthorized");
            }
            else{
                return StatusCode(200);
            }
        }
    }
}
