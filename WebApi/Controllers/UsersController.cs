using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager _userManager;

        public UsersController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRequest req)
        {
            var userProfile = await _userManager.CreateUserAsync(req);
            if (userProfile != null)
                return new OkObjectResult(userProfile);

            return new BadRequestResult();
        }
    }
}
