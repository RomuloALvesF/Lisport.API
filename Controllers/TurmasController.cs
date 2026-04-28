using Lisport.API.Application.DTOs.Aluno;
using Lisport.API.Application.DTOs.Presenca;
using Lisport.API.Application.DTOs.Turma;
using Lisport.API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lisport.API.Controllers
{
    [ApiController]
    [Route("turmas")]
    [Authorize]
    public class TurmasController : ControllerBase
    {
        private readonly ITurmaService _turmaService;
        private readonly IAlunoService _alunoService;
        private readonly IPresencaService _presencaService;

        public TurmasController(ITurmaService turmaService, IAlunoService alunoService, IPresencaService presencaService)
        {
            _turmaService = turmaService;
            _alunoService = alunoService;
            _presencaService = presencaService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateTurmaRequestDto request)
        {
            var turma = _turmaService.Create(
                request.Nome,
                request.Modalidade,
                request.FaixaEtaria,
                request.DiasHorarios);
            var response = new TurmaResponseDto
            {
                Id = turma.Id,
                Nome = turma.Nome,
                Modalidade = turma.Modalidade,
                FaixaEtaria = turma.FaixaEtaria,
                DiasHorarios = turma.DiasHorarios,
                CreatedAt = turma.CreatedAt
            };
            return CreatedAtAction(nameof(GetById), new { id = turma.Id }, response);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var turma = _turmaService.GetById(id);
            if (turma == null) return NotFound();
            return Ok(new TurmaResponseDto
            {
                Id = turma.Id,
                Nome = turma.Nome,
                Modalidade = turma.Modalidade,
                FaixaEtaria = turma.FaixaEtaria,
                DiasHorarios = turma.DiasHorarios,
                CreatedAt = turma.CreatedAt
            });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var turmas = _turmaService.GetAll();
            var response = turmas.Select(t => new TurmaResponseDto
            {
                Id = t.Id,
                Nome = t.Nome,
                Modalidade = t.Modalidade,
                FaixaEtaria = t.FaixaEtaria,
                DiasHorarios = t.DiasHorarios,
                CreatedAt = t.CreatedAt
            }).ToList();
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UpdateTurmaRequestDto request)
        {
            var turma = _turmaService.Update(id, request.Nome, request.Modalidade, request.FaixaEtaria, request.DiasHorarios);
            if (turma == null) return NotFound();
            return Ok(new TurmaResponseDto
            {
                Id = turma.Id,
                Nome = turma.Nome,
                Modalidade = turma.Modalidade,
                FaixaEtaria = turma.FaixaEtaria,
                DiasHorarios = turma.DiasHorarios,
                CreatedAt = turma.CreatedAt
            });
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            if (!_turmaService.Delete(id)) return NotFound();
            return NoContent();
        }

        [HttpGet("{id:guid}/alunos")]
        public IActionResult GetAlunos(Guid id)
        {
            if (_turmaService.GetById(id) == null) return NotFound();
            var alunos = _alunoService.GetByTurmaId(id);
            return Ok(alunos.Select(a => new AlunoResponseDto
            {
                Id = a.Id,
                Nome = a.Nome,
                DataNascimento = a.DataNascimento,
                Responsavel = a.Responsavel,
                Observacoes = a.Observacoes,
                FotoUrl = a.FotoUrl,
                TurmaId = a.TurmaId,
                CreatedAt = a.CreatedAt
            }).ToList());
        }

        [HttpGet("{id:guid}/presenca")]
        public IActionResult GetPresenca(Guid id, [FromQuery] DateTime data)
        {
            if (_turmaService.GetById(id) == null) return NotFound();
            var lista = _presencaService.GetPresencaLista(id, data);
            return Ok(lista.Select(x => new PresencaListaItemDto { AlunoId = x.AlunoId, AlunoNome = x.AlunoNome, Presente = x.Presente }).ToList());
        }

        [HttpPost("{id:guid}/presenca")]
        public IActionResult MarcarPresenca(Guid id, [FromBody] MarcarPresencaRequestDto request)
        {
            if (_turmaService.GetById(id) == null) return NotFound();
            try
            {
                _presencaService.MarcarPresenca(id, request.Data, request.AlunoId, request.Presente);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id:guid}/presenca/batch")]
        public IActionResult MarcarPresencaBatch(Guid id, [FromBody] MarcarPresencaBatchRequestDto request)
        {
            if (_turmaService.GetById(id) == null) return NotFound();
            try
            {
                var itens = request.Itens.Select(i => (i.AlunoId, i.Presente)).ToList();
                _presencaService.MarcarPresencaBatch(id, request.Data, itens);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
