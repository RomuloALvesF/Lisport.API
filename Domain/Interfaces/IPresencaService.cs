namespace Lisport.API.Domain.Interfaces
{
    public interface IPresencaService
    {
        IReadOnlyList<PresencaListaItem> GetPresencaLista(Guid turmaId, DateTime data);
        void MarcarPresenca(Guid turmaId, DateTime data, Guid alunoId, bool presente);
        void MarcarPresencaBatch(Guid turmaId, DateTime data, IReadOnlyList<(Guid AlunoId, bool Presente)> itens);
    }

    public class PresencaListaItem
    {
        public Guid AlunoId { get; set; }
        public string AlunoNome { get; set; } = string.Empty;
        public bool Presente { get; set; }
    }
}
