using Main.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using System;
using System.Threading.Tasks;

namespace Main.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Save(UserDto dto)
        {
            _logger.LogInformation("Add User = {user}.", dto);

            dto.Validate();

            var id = await _userService.UpSert(dto.ToModel());

            _logger.LogInformation("User = {user} updated successfully.", dto);

            return CreatedAtAction(nameof(Save), id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            _logger.LogInformation("Get User for Id = {id}.", id);

            Guard.Id(id);

            var user = await _userService.Find(id);

            if (user == null)
            {
                _logger.LogInformation("Did not find the User for Id = {id}.", id);
                return NotFound();
            }

            _logger.LogInformation("Find the User = {user} for Id = {id}.", user, id);
            return Ok(user);
        }

        [HttpGet("Find/{username}")]
        public async Task<ActionResult> Get(string username)
        {
            _logger.LogInformation("Get User for UserName = {username}.", username);

            Guard.UserName(username);

            var user = await _userService.Find(username);

            if (user == null)
            {
                _logger.LogInformation("Did not find the User for UserName = {userName}.", username);
                return NotFound();
            }

            _logger.LogInformation("Find the User = {user} for UserName = {userName}.", user, username);
            return Ok(user);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Delete User for Id = {id}.", id);

            Guard.Id(id);

            if (await _userService.Delete(id))
            {
                _logger.LogInformation("Deleted the user for Id = {id}.", id);
                return Ok();
            }

            _logger.LogInformation("Did not find the User for Id = {id}.", id);
            return new ObjectResult($"Failed to delete User for Id = {id}")
            {
                StatusCode = StatusCodes.Status417ExpectationFailed
            };
        }
    }
}