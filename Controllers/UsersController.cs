using Lisport.API.Application.DTOs;
using Lisport.API.Domain.Entities;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }    

        [HttpGet("{id:guid}")]

        public ActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);

            if (user == null) return NotFound();

            var response = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
            };

            return Ok(response);
            
        }

        [HttpPatch("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UpdateUserRequestDto request)
        {
            User? user;
            try
            {
                user = _userService.Update(id, request.Name, request.Email, request.Role);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            if (user == null) return NotFound();

            var response = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
            };

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var deleted = _userService.Delete(id);

            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
