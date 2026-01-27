using Lisport.API.Application.DTOs;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }    

        public ActionResult Create([FromBody] CreateUserRequestDto request)
        {
            var user = _userService.Create(request.Name, request.Email);

            var response = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
            };

            return Created("", response);
        }
        [HttpGet("{id:guid}")] //??

        public ActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);

            if (user == null) return NotFound();

            var response = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
            };

            return Ok(response);
            
        }

        [HttpPatch("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UpdateUserRequestDto request)
        {
            var user = _userService.Update(id, request.name, request.email);

            if (user == null) return NotFound();

            var response = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
            };

            return Ok(response);
        }
    }
}
