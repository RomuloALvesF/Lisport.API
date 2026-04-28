using Lisport.API.Domain.Entities;

namespace Lisport.API.Domain.Interfaces
{
    public interface ITurmaService
    {
        Turma Create(string nome, string modalidade, string faixaEtaria, string diasHorarios);
        Turma? GetById(Guid id);
        IReadOnlyList<Turma> GetAll();
        Turma? Update(Guid id, string nome, string modalidade, string faixaEtaria, string diasHorarios);
        bool Delete(Guid id);
    }
}
