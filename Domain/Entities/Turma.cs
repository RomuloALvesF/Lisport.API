namespace Lisport.API.Domain.Entities
{
    public class Turma
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Modalidade { get; private set; }
        public string FaixaEtaria { get; private set; }
        public string DiasHorarios { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Turma() { }

        public Turma(string nome, string modalidade, string faixaEtaria, string diasHorarios)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Modalidade = modalidade;
            FaixaEtaria = faixaEtaria;
            DiasHorarios = diasHorarios;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string nome, string modalidade, string faixaEtaria, string diasHorarios)
        {
            Nome = nome;
            Modalidade = modalidade;
            FaixaEtaria = faixaEtaria;
            DiasHorarios = diasHorarios;
        }
    }
}
