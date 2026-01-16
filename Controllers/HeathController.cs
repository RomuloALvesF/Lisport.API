using Lisport.API.Application.DTOs;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("health")]
    public class HeathController : ControllerBase
    {
        private readonly IHealthService _healthService;

        public HeathController(IHealthService healthService) 
        {
            _healthService = healthService;
        }

        [HttpGet]
        public IActionResult Get() 
        {

            var response = new HealthResponseDto
            {
                Status = _healthService.GetStatus(),
                Timestamp = DateTime.Now
            };

            return Ok(response);
        }

    }
}
