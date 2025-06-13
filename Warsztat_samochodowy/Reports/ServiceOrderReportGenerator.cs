using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.Reports
{
    public static class ServiceOrderReportGenerator
    {
        public static byte[] Generate(ServiceOrderModel order)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);

                    page.Header().Text($"Raport zlecenia: {order.Id}")
                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                    page.Content().Column(column =>
                    {
                        column.Item().Text($"Data utworzenia: {order.CreatedAt:yyyy-MM-dd HH:mm}");
                        column.Item().Text($"Status: {order.Status}");
                        column.Item().Text($"Mechanik: {order.AssignedMechanic ?? "(nieprzypisany)"}");
                        column.Item().Text($"Pojazd: {order.Vehicle?.Make ?? "?"} {order.Vehicle?.Model ?? "?"}");

                        if (order.Tasks?.Any() == true)
                        {
                            column.Item().Text("Zadania serwisowe:").Bold();
                            foreach (var task in order.Tasks)
                            {
                                column.Item().Text($"• {task.Description}");
                            }
                        }

                        if (order.Comments?.Any() == true)
                        {
                            column.Item().Text("Komentarze:").Bold();
                            foreach (var comment in order.Comments)
                            {
                                column.Item().Text($"{comment.Timestamp:yyyy-MM-dd HH:mm} - {comment.Author}: {comment.Content}");
                            }
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
