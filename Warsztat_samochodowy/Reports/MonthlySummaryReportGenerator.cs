using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.Reports
{
    public static class MonthlySummaryReportGenerator
    {
        public static byte[] Generate(List<ServiceOrderModel> orders)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);

                    page.Header().Text("Miesięczny raport zleceń")
                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                    page.Content().Column(column =>
                    {
                        column.Item().Text($"Data wygenerowania: {DateTime.Now:yyyy-MM-dd HH:mm}");

                        column.Item().Text($"Liczba zleceń: {orders.Count}").Bold();

                        foreach (var order in orders)
                        {
                            column.Item().PaddingTop(10).Text(text =>
                            {
                                text.Span("Zlecenie ").SemiBold();
                                text.Span(order.Id.ToString());
                                text.Span(" | ").FontColor(Colors.Grey.Darken2);
                                text.Span($"{order.CreatedAt:yyyy-MM-dd} | {order.Status} | {order.Vehicle.Make} {order.Vehicle.Model} | {order.AssignedMechanic ?? "Brak"}");
                            });
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Warsztat samochodowy - ").FontSize(10);
                        x.Span(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm")).FontSize(10);
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}