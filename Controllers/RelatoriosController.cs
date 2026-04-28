using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("relatorios")]
    [Authorize(Roles = "Gestor,Professor")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioPatrocinadorService _relatorioService;

        public RelatoriosController(IRelatorioPatrocinadorService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("patrocinador")]
        public IActionResult GetRelatorioPatrocinador([FromQuery] DateTime? de, [FromQuery] DateTime? ate)
        {
            var fim = ate ?? DateTime.UtcNow.Date;
            var inicio = de ?? fim.AddMonths(-1);
            if (inicio > fim) (inicio, fim) = (fim, inicio);
            var pdf = _relatorioService.GerarPdf(inicio, fim);
            var fileName = $"relatorio-patrocinador-{inicio:yyyy-MM-dd}-{fim:yyyy-MM-dd}.pdf";
            return File(pdf, "application/pdf", fileName);
        }
    }
}
