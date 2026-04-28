namespace Lisport.API.Domain.Interfaces
{
    public interface IRelatorioPatrocinadorService
    {
        byte[] GerarPdf(DateTime de, DateTime ate);
    }
}
