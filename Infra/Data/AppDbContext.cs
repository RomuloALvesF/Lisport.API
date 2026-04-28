using Lisport.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Lisport.API.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Presenca> Presencas { get; set; }
        public DbSet<Evolucao> Evolucoes { get; set; }
        public DbSet<ObservacaoGeral> ObservacoesGerais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Presenca>(e =>
            {
                e.HasIndex(p => new { p.TurmaId, p.Data, p.AlunoId }).IsUnique();
            });
        }
    }
}
