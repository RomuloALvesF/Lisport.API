using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface IAlunoRepository
    {
        void Add(Aluno aluno);
        Aluno? GetById(Guid id);
        IReadOnlyList<Aluno> GetByTurmaId(Guid turmaId);
        void Update(Aluno aluno);
        void Delete(Aluno aluno);
    }
}
