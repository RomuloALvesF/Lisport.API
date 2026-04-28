namespace Lisport.API.Domain.Entities
{
    public class Aluno
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Responsavel { get; private set; }
        public string? Observacoes { get; private set; }
        public string? FotoUrl { get; private set; }
        public bool TemUniforme { get; private set; }
        public bool? RecebeuUniforme { get; private set; }
        public Guid TurmaId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public virtual Turma? Turma { get; private set; }

        private Aluno() { }

        public Aluno(string nome, DateTime dataNascimento, string responsavel, Guid turmaId, string? observacoes = null, string? fotoUrl = null, bool temUniforme = false, bool? recebeuUniforme = null)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataNascimento = dataNascimento;
            Responsavel = responsavel;
            TurmaId = turmaId;
            Observacoes = observacoes;
            FotoUrl = fotoUrl;
            TemUniforme = temUniforme;
            RecebeuUniforme = recebeuUniforme;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string nome, DateTime dataNascimento, string responsavel, string? observacoes, string? fotoUrl, bool temUniforme, bool? recebeuUniforme)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Responsavel = responsavel;
            Observacoes = observacoes;
            FotoUrl = fotoUrl;
            TemUniforme = temUniforme;
            RecebeuUniforme = recebeuUniforme;
        }
    }
}
