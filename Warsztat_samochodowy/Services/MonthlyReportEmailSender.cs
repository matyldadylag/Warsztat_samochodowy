using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Mail;
using Warsztat_samochodowy.Data;
using Warsztat_samochodowy.Reports;

namespace Warsztat_samochodowy.Services
{
    public class MonthlyReportEmailSender : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public MonthlyReportEmailSender(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(10000, stoppingToken);

            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<WorkshopDbContext>();

            var currentMonth = DateTime.UtcNow.Month;
            var currentYear = DateTime.UtcNow.Year;

            var orders = await db.ServiceOrders
                .Include(o => o.Vehicle)
                .Where(o => o.CreatedAt.Month == currentMonth && o.CreatedAt.Year == currentYear)
                .ToListAsync(stoppingToken);

            var pdf = MonthlySummaryReportGenerator.Generate(orders);

            var message = new MailMessage
            {
                From = new MailAddress("matylda.dylag@outlook.com", "Warsztat"),
                Subject = "Miesięczny raport zleceń",
                Body = $"Załączony raport zawiera podsumowanie zleceń serwisowych za {DateTime.Now:MMMM yyyy}.",
                IsBodyHtml = false
            };

            message.To.Add("matylda.dylag@student.pk.edu.pl");
            message.Attachments.Add(new Attachment(new MemoryStream(pdf), "RaportZlecen_" + DateTime.Now.ToString("yyyy_MM") + ".pdf"));

            using var smtp = new SmtpClient("smtp.office365.com", 587)
            {
                Credentials = new NetworkCredential("matylda.dylag@outlook.com", "xhndjwcrkexpioff"),
                EnableSsl = true
            };

            try
            {
                await smtp.SendMailAsync(message, stoppingToken);
                Console.WriteLine("Raport wysłany.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd przy wysyłce maila: " + ex.Message);
            }
        }
    }
}