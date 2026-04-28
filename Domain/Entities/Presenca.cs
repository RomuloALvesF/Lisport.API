namespace Lisport.API.Domain.Entities
{
    public class Presenca
    {
        public Guid Id { get; private set; }
        public Guid TurmaId { get; private set; }
        public DateTime Data { get; private set; } // dia da aula (date only)
        public Guid AlunoId { get; private set; }
        public bool Presente { get; private set; }

        public virtual Turma? Turma { get; private set; }
        public virtual Aluno? Aluno { get; private set; }

        private Presenca() { }

        public Presenca(Guid turmaId, DateTime data, Guid alunoId, bool presente)
        {
            Id = Guid.NewGuid();
            TurmaId = turmaId;
            Data = data.Date;
            AlunoId = alunoId;
            Presente = presente;
        }

        public void SetPresente(bool presente)
        {
            Presente = presente;
        }
    }
}
