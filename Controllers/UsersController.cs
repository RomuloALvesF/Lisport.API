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
    }
}
