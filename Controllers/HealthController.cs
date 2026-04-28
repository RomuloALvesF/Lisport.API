using Lisport.API.Application.DTOs;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        private readonly IHealthService _healthService;

        public HealthController(IHealthService healthService)
        {
            _healthService = healthService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = new HealthResponseDto
            {
                Status = _healthService.GetStatus(),
                Timestamp = DateTime.UtcNow
            };
            return Ok(response);
        }
    }
}
