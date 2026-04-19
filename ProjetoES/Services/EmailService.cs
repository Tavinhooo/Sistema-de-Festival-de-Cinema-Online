using System.Net;
using System.Net.Mail;

namespace ProjetoES.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarEmailAsync(string destino, string assunto, string mensagemHtml)
        {
            var host = _config["EmailSettings:SmtpServer"];
            var port = int.Parse(_config["EmailSettings:Port"]!);
            var username = _config["EmailSettings:SenderEmail"];
            var password = _config["EmailSettings:Password"];
            var senderName = _config["EmailSettings:SenderName"];

            // Configurar o cliente SMTP do Google
            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true // Obrigatório para o Gmail
            };

            // Criar a carta
            var mailMessage = new MailMessage
            {
                From = new MailAddress(username!, senderName),
                Subject = assunto,
                Body = mensagemHtml,
                IsBodyHtml = true // Permite-nos usar HTML para deixar o email bonito!
            };
            
            mailMessage.To.Add(destino);

            // Enviar!
            await client.SendMailAsync(mailMessage);
        }
    }
}