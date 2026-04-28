using Lisport.API.Application.DTOs.Observacao;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("observacoes")]
    [Authorize(Roles = "Professor,Gestor")]
    public class ObservacoesController : ControllerBase
    {
        private readonly IObservacaoGeralService _observacaoService;

        public ObservacoesController(IObservacaoGeralService observacaoService)
        {
            _observacaoService = observacaoService;
        }

        [HttpGet]
        public IActionResult GetFiltered([FromQuery] Guid? turmaId, [FromQuery] Guid? alunoId)
        {
            var list = _observacaoService.GetFiltered(turmaId, alunoId);
            return Ok(list.Select(o => new ObservacaoGeralResponseDto
            {
                Id = o.Id,
                TurmaId = o.TurmaId,
                AlunoId = o.AlunoId,
                Texto = o.Texto,
                CreatedAt = o.CreatedAt
            }).ToList());
        }

        [HttpPost]
        public IActionResult Create([FromBody] ObservacaoGeralRequestDto request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();
            var obs = _observacaoService.Create(request.Texto, userId, request.TurmaId, request.AlunoId);
            var response = new ObservacaoGeralResponseDto
            {
                Id = obs.Id,
                TurmaId = obs.TurmaId,
                AlunoId = obs.AlunoId,
                Texto = obs.Texto,
                CreatedAt = obs.CreatedAt
            };
            return CreatedAtAction(nameof(GetFiltered), response);
        }
    }
}
