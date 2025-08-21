using System.Text;
using ApiSindisure.Domain.Interfaces.Apps.EmailReportsApp;
using ApiSindisure.Domain.ViewModel.EmailViewModel;
using ApiSindisure.Services.Supabase;
using Resend;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ApiSindisure.Apps.EmailReportsApp
{
    public class EmailReportsApp : IEmailReportsApp
    {
        private readonly SupabaseService _supabaseService;
        private readonly ResendClient _resendClient;
        private readonly ILogger<EmailReportsApp> _logger;
        public string LogId { get; set; }

        public EmailReportsApp(SupabaseService supabaseService, ResendClient resendClient, ILogger<EmailReportsApp> logger)
        {
            _supabaseService = supabaseService;
            _resendClient = resendClient;
            _logger = logger;
            LogId = Guid.NewGuid().ToString();
        }

        public async Task SendMonthlyReportAsync(
            EmailViewModel.Request request,
            CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();

                // Obter dados do último mês
                var now = DateTime.UtcNow;
                var lastMonthStart = new DateTime(now.Year, now.Month - 1, 1);
                var lastMonthEnd = new DateTime(now.Year, now.Month, 1).AddDays(-1);

                // Buscar contas a pagar
                var payablesResult = await client
                    .From<AccountsPayableModel>()
                    .Select("*")
                    .Filter("created_by", Supabase.Postgrest.Constants.Operator.Equals, request.User.Id)
                    .Filter("due_date", Supabase.Postgrest.Constants.Operator.GreaterThanOrEqual, lastMonthStart.ToString("yyyy-MM-dd"))
                    .Filter("due_date", Supabase.Postgrest.Constants.Operator.LessThanOrEqual, lastMonthEnd.ToString("yyyy-MM-dd"))
                    .Get();

                var payables = payablesResult.Models.Where(p => p.Status == "paid").ToList();

                // Buscar contas a receber
                var receivablesResult = await client
                    .From<AccountsReceivableModel>()
                    .Select("*")
                    .Filter("created_by", Supabase.Postgrest.Constants.Operator.Equals, request.User.Id)
                    .Filter("due_date", Supabase.Postgrest.Constants.Operator.GreaterThanOrEqual, lastMonthStart.ToString("yyyy-MM-dd"))
                    .Filter("due_date", Supabase.Postgrest.Constants.Operator.LessThanOrEqual, lastMonthEnd.ToString("yyyy-MM-dd"))
                    .Get();

                var receivables = receivablesResult.Models.Where(r => r.Status == "received").ToList();

                // Gerar relatório
                string monthName = lastMonthStart.ToString("MMMM");
                int year = lastMonthStart.Year;

                var reportContent = GenerateCashFlowReport(payables, receivables, monthName, year, request, request.Condominium.Name);

                // Criar PDF básico
                var pdfBytes = CreateBasicPDF(reportContent);

                // Salvar histórico
                var emailHistory = new EmailReportsHistoryModel
                {
                    Id = Guid.NewGuid().ToString(),
                    AutomationId = request.Automation.Id,
                    UserId = request.User.Id,
                    RecipientEmail = request.Automation.Email,
                    ReportType = "cash_flow",
                    FileName = $"relatorio_fluxo_caixa_{monthName}_{year}_{request.Condominium.Name.Replace(" ", "_")}.pdf",
                    FileUrl = null,
                    SentAt = DateTime.UtcNow,
                    Status = "sent",
                    CreatedAt = DateTime.UtcNow                   
                };

                await client.From<EmailReportsHistoryModel>().Insert(emailHistory);

                // Enviar email usando Resend
                await _resendClient.EmailSendAsync(new EmailMessage
                {
                    From = "SINDISURE <noreply@sindisure.com.br>",
                    To = request.Automation?.Email,
                    Subject = $"Relatório {monthName}/{year} - {request.Condominium.Name}",
                    HtmlBody = $@"
                        <div>
                            <p>Olá {request.Automation.ResponsibleName},</p>
                            <p>Segue relatório de fluxo de caixa referente ao mês de {monthName}/{year}.</p>
                        </div>",
                    Attachments = new List<EmailAttachment>
                    {
                        new EmailAttachment
                        {
                            Filename = emailHistory.FileName,
                            Content = Convert.ToBase64String(pdfBytes),
                            ContentType = "application/pdf"
                        }
                    }
                });

            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmailReportsApp)} - Erro ao acessar o banco de dados: {LogId}" + ex.Message, ex);
                throw new Exception("Erro ao enviar relatório mensal", ex);
            }
        }

        private string GenerateCashFlowReport(
            List<AccountsPayableModel> payables,
            List<AccountsReceivableModel> receivables,
            string monthName,
            int year,
            EmailViewModel.Request user,
            string condominiumName)
        {
            var allTransactions = new List<(DateTime date, string type, decimal amount, string description)>();

            allTransactions.AddRange(payables.Select(p => (p.DueDate, "Pago", p.Amount, p.Description)));
            allTransactions.AddRange(receivables.Select(r => (r.DueDate, "Recebido", r.Amount, r.Description)));

            allTransactions = allTransactions.OrderBy(t => t.date).ToList();

            decimal runningTotal = 0;
            var reportLines = allTransactions.Select(t =>
            {
                runningTotal += t.type == "Recebido" ? t.amount : -t.amount;
                return $"{t.date:dd/MM/yyyy} | {t.type} | {(t.type == "Recebido" ? "+" : "-")}R$ {t.amount:0.00} | {t.description} | Subtotal: R$ {runningTotal:0.00}";
            });

            var report = new StringBuilder();
            report.AppendLine($"RELATORIO DE FLUXO DE CAIXA - {condominiumName}");
            report.AppendLine($"Periodo: {monthName} {year}");
            report.AppendLine($"Usuario: {user.User.Name}");
            report.AppendLine();
            report.AppendLine("MOVIMENTACOES FINANCEIRAS:");
            report.AppendLine(string.Join("\n", reportLines));
            report.AppendLine();
            report.AppendLine($"Saldo final: R$ {runningTotal:0.00}");

            return report.ToString();
        }

        private byte[] CreateBasicPDF(string content)
        {
            using var stream = new MemoryStream();

            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Item().Text(content);
                        });
                });
            })
            .GeneratePdf(stream);

            return stream.ToArray();
        }
    }    
}
