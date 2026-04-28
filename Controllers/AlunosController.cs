using Lisport.API.Application.DTOs.Aluno;
using Lisport.API.Application.DTOs.Evolucao;
using Lisport.API.Domain.Enums;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("alunos")]
    [Authorize]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;
        private readonly IEvolucaoService _evolucaoService;

        public AlunosController(IAlunoService alunoService, IEvolucaoService evolucaoService)
        {
            _alunoService = alunoService;
            _evolucaoService = evolucaoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAlunoRequestDto request)
        {
            try
            {
                var aluno = _alunoService.Create(
                    request.Nome,
                    request.DataNascimento,
                    request.Responsavel,
                    request.TurmaId,
                    request.Observacoes,
                    request.FotoUrl,
                    request.TemUniforme,
                    request.RecebeuUniforme);
                var response = MapToDto(aluno);
                return CreatedAtAction(nameof(GetById), new { id = aluno.Id }, response);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Turma"))
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var aluno = _alunoService.GetById(id);
            if (aluno == null) return NotFound();
            return Ok(MapToDto(aluno));
        }

        [HttpGet]
        public IActionResult GetByTurma([FromQuery] Guid turmaId)
        {
            if (turmaId == Guid.Empty) return BadRequest("turmaId é obrigatório.");
            var alunos = _alunoService.GetByTurmaId(turmaId);
            return Ok(alunos.Select(MapToDto).ToList());
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UpdateAlunoRequestDto request)
        {
            var aluno = _alunoService.Update(id, request.Nome, request.DataNascimento, request.Responsavel, request.Observacoes, request.FotoUrl, request.TemUniforme, request.RecebeuUniforme);
            if (aluno == null) return NotFound();
            return Ok(MapToDto(aluno));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            if (!_alunoService.Delete(id)) return NotFound();
            return NoContent();
        }

        [HttpGet("{id:guid}/evolucoes")]
        public IActionResult GetEvolucoes(Guid id)
        {
            if (_alunoService.GetById(id) == null) return NotFound();
            var list = _evolucaoService.GetByAlunoId(id);
            return Ok(list.Select(MapEvolucaoToDto).ToList());
        }

        [HttpGet("{id:guid}/evolucoes/{periodo}")]
        public IActionResult GetEvolucaoPorPeriodo(Guid id, string periodo)
        {
            if (_alunoService.GetById(id) == null) return NotFound();
            var ev = _evolucaoService.GetByAlunoAndPeriodo(id, periodo);
            if (ev == null) return NotFound();
            return Ok(MapEvolucaoToDto(ev));
        }

        [HttpPost("{id:guid}/evolucoes")]
        public IActionResult UpsertEvolucao(Guid id, [FromBody] EvolucaoRequestDto request)
        {
            if (_alunoService.GetById(id) == null) return NotFound();
            var (fisica, tecnica, comportamento) = (ParseNivel(request.EvolucaoFisica), ParseNivel(request.EvolucaoTecnica), ParseNivel(request.Comportamento));
            try
            {
                var ev = _evolucaoService.Upsert(id, request.Periodo, fisica, tecnica, comportamento, request.Observacao);
                return Ok(MapEvolucaoToDto(ev));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}/evolucoes/{periodo}")]
        public IActionResult UpdateEvolucao(Guid id, string periodo, [FromBody] EvolucaoRequestDto request)
        {
            if (_alunoService.GetById(id) == null) return NotFound();
            var (fisica, tecnica, comportamento) = (ParseNivel(request.EvolucaoFisica), ParseNivel(request.EvolucaoTecnica), ParseNivel(request.Comportamento));
            try
            {
                var ev = _evolucaoService.Upsert(id, periodo, fisica, tecnica, comportamento, request.Observacao);
                return Ok(MapEvolucaoToDto(ev));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static NivelEvolucao ParseNivel(int value)
        {
            return value switch { -1 => NivelEvolucao.Reduziu, 0 => NivelEvolucao.Manteve, 1 => NivelEvolucao.Melhorou, _ => NivelEvolucao.Manteve };
        }

        private static EvolucaoResponseDto MapEvolucaoToDto(Domain.Entities.Evolucao e)
        {
            return new EvolucaoResponseDto
            {
                Id = e.Id,
                AlunoId = e.AlunoId,
                Periodo = e.Periodo,
                EvolucaoFisica = (int)e.EvolucaoFisica,
                EvolucaoTecnica = (int)e.EvolucaoTecnica,
                Comportamento = (int)e.Comportamento,
                Observacao = e.Observacao,
                CreatedAt = e.CreatedAt
            };
        }

        private static AlunoResponseDto MapToDto(Domain.Entities.Aluno a)
        {
            return new AlunoResponseDto
            {
                Id = a.Id,
                Nome = a.Nome,
                DataNascimento = a.DataNascimento,
                Responsavel = a.Responsavel,
                Observacoes = a.Observacoes,
                FotoUrl = a.FotoUrl,
                TemUniforme = a.TemUniforme,
                RecebeuUniforme = a.RecebeuUniforme,
                TurmaId = a.TurmaId,
                CreatedAt = a.CreatedAt
            };
        }
    }
}
