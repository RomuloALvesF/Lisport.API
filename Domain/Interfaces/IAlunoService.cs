using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IAlunoService
    {
        Aluno Create(string nome, DateTime dataNascimento, string responsavel, Guid turmaId, string? observacoes = null, string? fotoUrl = null, bool temUniforme = false, bool? recebeuUniforme = null);
        Aluno? GetById(Guid id);
        IReadOnlyList<Aluno> GetByTurmaId(Guid turmaId);
        Aluno? Update(Guid id, string nome, DateTime dataNascimento, string responsavel, string? observacoes, string? fotoUrl, bool temUniforme, bool? recebeuUniforme);
        bool Delete(Guid id);
    }
}
