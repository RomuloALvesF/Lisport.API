using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface ITurmaRepository
    {
        void Add(Turma turma);
        Turma? GetById(Guid id);
        IReadOnlyList<Turma> GetAll();
        void Update(Turma turma);
        void Delete(Turma turma);
    }
}
