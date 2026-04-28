using Lisport.API.Domain.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Lisport.API.Application.Services
{
    public class RelatorioPatrocinadorService : IRelatorioPatrocinadorService
    {
        private readonly IDashboardService _dashboardService;

        public RelatorioPatrocinadorService(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GerarPdf(DateTime de, DateTime ate)
        {
            var impacto = _dashboardService.GetImpacto(de, ate);
            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.Header().Element(c => ComposeHeader(c, de, ate));
                    page.Content().Element(c => ComposeContent(c, impacto));
                    page.Footer().AlignCenter().Text("Relatório Lisport - Gerado automaticamente");
                });
            });
            return doc.GeneratePdf();
        }

        private static void ComposeHeader(IContainer container, DateTime de, DateTime ate)
        {
            container.Column(column =>
            {
                column.Item().AlignCenter().Text("Relatório de Impacto - Projeto Esportivo").FontSize(18).Bold();
                column.Item().AlignCenter().Text($"Período: {de:dd/MM/yyyy} a {ate:dd/MM/yyyy}").FontSize(12);
                column.Item().PaddingVertical(10);
            });
        }

        private static void ComposeContent(IContainer container, DashboardImpactoDto impacto)
        {
            container.Column(column =>
            {
                column.Item().Text("Indicadores").FontSize(14).Bold();
                column.Item().PaddingBottom(8);
                column.Item().Row(row =>
                {
                    row.RelativeItem().Background(Colors.Grey.Lighten3).Padding(10).Column(c =>
                    {
                        c.Item().Text("Alunos atendidos").FontSize(10);
                        c.Item().Text(impacto.TotalAlunosAtivos.ToString()).FontSize(20).Bold();
                    });
                    row.RelativeItem().PaddingLeft(10).Background(Colors.Grey.Lighten3).Padding(10).Column(c =>
                    {
                        c.Item().Text("Turmas").FontSize(10);
                        c.Item().Text(impacto.TotalTurmas.ToString()).FontSize(20).Bold();
                    });
                    row.RelativeItem().PaddingLeft(10).Background(Colors.Grey.Lighten3).Padding(10).Column(c =>
                    {
                        c.Item().Text("Frequência média (%)").FontSize(10);
                        c.Item().Text(impacto.FrequenciaMediaPercentual.ToString("F1") + "%").FontSize(20).Bold();
                    });
                });
                column.Item().PaddingVertical(15);

                if (impacto.EvolucaoGeral != null)
                {
                    column.Item().Text("Evolução geral (último período)").FontSize(14).Bold();
                    column.Item().PaddingBottom(6);
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();
                            c.ConstantColumn(80);
                            c.ConstantColumn(80);
                            c.ConstantColumn(80);
                        });
                        table.Header(h =>
                        {
                            h.Cell().Element(CellStyle).Text("Indicador");
                            h.Cell().Element(CellStyle).Text("Melhorou");
                            h.Cell().Element(CellStyle).Text("Manteve");
                            h.Cell().Element(CellStyle).Text("Reduziu");
                        });
                        table.Cell().Element(CellStyle).Text("Evolução física");
                        table.Cell().Element(CellStyle).Text(impacto.EvolucaoGeral.MelhorouFisica.ToString());
                        table.Cell().Element(CellStyle).Text(impacto.EvolucaoGeral.ManteveFisica.ToString());
                        table.Cell().Element(CellStyle).Text(impacto.EvolucaoGeral.ReduziuFisica.ToString());
                        table.Cell().Element(CellStyle).Text("Evolução técnica");
                        table.Cell().Element(CellStyle).Text(impacto.EvolucaoGeral.MelhorouTecnica.ToString());
                        table.Cell().Element(CellStyle).Text(impacto.EvolucaoGeral.ManteveTecnica.ToString());
                        table.Cell().Element(CellStyle).Text(impacto.EvolucaoGeral.ReduziuTecnica.ToString());
                        table.Cell().Element(CellStyle).Text("Comportamento");
                        table.Cell().Element(CellStyle).Text(impacto.EvolucaoGeral.MelhorouComportamento.ToString());
                        table.Cell().Element(CellStyle).Text(impacto.EvolucaoGeral.ManteveComportamento.ToString());
                        table.Cell().Element(CellStyle).Text(impacto.EvolucaoGeral.ReduziuComportamento.ToString());
                    });
                    column.Item().PaddingVertical(15);
                }

                column.Item().Text("Impacto social").FontSize(14).Bold();
                column.Item().PaddingBottom(6);
                column.Item().Background(Colors.Grey.Lighten4).Padding(12).Column(c =>
                {
                    c.Item().Text($"O projeto atendeu {impacto.TotalAlunosAtivos} alunos em {impacto.TotalTurmas} turma(s), com frequência média de {impacto.FrequenciaMediaPercentual:F1}% no período. " +
                        "Os indicadores de evolução física, técnica e comportamental refletem o acompanhamento contínuo e o desenvolvimento dos participantes.");
                });
            });
        }

        private static IContainer CellStyle(IContainer c)
        {
            return c.BorderBottom(0.5f).BorderColor(Colors.Grey.Medium).Padding(6);
        }
    }
}
