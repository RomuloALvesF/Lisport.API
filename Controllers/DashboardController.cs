using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("dashboard")]
    [Authorize(Roles = "Gestor,VisualizadorExterno,Professor")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("impacto")]
        public IActionResult GetImpacto([FromQuery] DateTime? de, [FromQuery] DateTime? ate)
        {
            var fim = ate ?? DateTime.UtcNow.Date;
            var inicio = de ?? fim.AddMonths(-1);
            if (inicio > fim) (inicio, fim) = (fim, inicio);
            var result = _dashboardService.GetImpacto(inicio, fim);
            return Ok(result);
        }
    }
}
