namespace Lisport.API.Domain.Entities
{
    public class ObservacaoGeral
    {
        public Guid Id { get; private set; }
        public Guid? TurmaId { get; private set; }
        public Guid? AlunoId { get; private set; }
        public string Texto { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid CreatedByUserId { get; private set; }

        public virtual Turma? Turma { get; private set; }
        public virtual Aluno? Aluno { get; private set; }
        public virtual User? CreatedByUser { get; private set; }

        private ObservacaoGeral() { Texto = string.Empty; }

        public ObservacaoGeral(string texto, Guid createdByUserId, Guid? turmaId = null, Guid? alunoId = null)
        {
            Id = Guid.NewGuid();
            Texto = texto;
            CreatedByUserId = createdByUserId;
            TurmaId = turmaId;
            AlunoId = alunoId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
