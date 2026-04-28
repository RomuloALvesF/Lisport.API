namespace Lisport.API.Domain.Interfaces
{
    public interface IDashboardService
    {
        DashboardImpactoDto GetImpacto(DateTime de, DateTime ate);
    }

    public class DashboardImpactoDto
    {
        public int TotalAlunosAtivos { get; set; }
        public int TotalTurmas { get; set; }
        public double FrequenciaMediaPercentual { get; set; }
        public EvolucaoGeralDto? EvolucaoGeral { get; set; }
        public FrequenciaComparativoDto? ComparativoPeriodo { get; set; }
    }

    public class EvolucaoGeralDto
    {
        public int MelhorouFisica { get; set; }
        public int ManteveFisica { get; set; }
        public int ReduziuFisica { get; set; }
        public int MelhorouTecnica { get; set; }
        public int ManteveTecnica { get; set; }
        public int ReduziuTecnica { get; set; }
        public int MelhorouComportamento { get; set; }
        public int ManteveComportamento { get; set; }
        public int ReduziuComportamento { get; set; }
    }

    public class FrequenciaComparativoDto
    {
        public double FrequenciaPeriodoAtual { get; set; }
        public double FrequenciaPeriodoAnterior { get; set; }
    }
}
