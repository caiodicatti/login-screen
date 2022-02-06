using api_login.Repository.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace api_login.Utils
{
    public class ServiceMail
    {

        public static void Send(IConfiguration configuration, string nome, string email, string tipo, string code = "")
        {
            string emailFrom = configuration["EmailConfig:Email"].ToString();
            string emailPassword = configuration["EmailConfig:Password"].ToString();            
            string smtp = configuration["EmailConfig:SMPT"].ToString();            
            int port = Int32.Parse(configuration["EmailConfig:Port"]);

            MailMessage emailMessage = new MailMessage();

            string titulo = tipo == "cadastro" ? "Usuário Cadastrado" : "Recuperação de Senha";

            try
            {
                var smtpClient = new SmtpClient(smtp, port);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 60 * 60;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailFrom, emailPassword); 

                emailMessage.From = new MailAddress(emailFrom, "Sistema Login Screen");
                emailMessage.Body = BodyEmail(nome, tipo, code);
                emailMessage.Subject = titulo;
                emailMessage.IsBodyHtml = true;
                emailMessage.Priority = MailPriority.Normal;
                emailMessage.To.Add(email);

                smtpClient.Send(emailMessage);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string BodyEmail(string nome, string tipo, string code)
        {
            if(tipo.ToLower() == "cadastro")
            {
                return $"<!DOCTYPE html><body>Olá<b> {nome}</b>!<br>Seu e-mail foi cadastrado com sucesso!(;<br><br>Obrigado.</body></html>";
            }
            else
            {
                string email = $"<!DOCTYPE html>";
                email += $"<html lang='pt-br'>";
                email += $"<head>";
                email += $"<meta charset='UTF-8'>";
                email += $"<meta http-equiv='X-UA-Compatible' content='IE=edge'>";
                email += $"<meta name='viewport' content='width=device-width, initial-scale=1.0'>";
                email += $"</head>";
                email += $"<body>";
                email += $"Olá<b> {nome}</b>!<br><a href='http://localhost:4200/recovery/{code}'>Clique aqui</a> para redefinir sua senha.";
                email += $"</body>";
                email += $"</html>";

                return email;
            }
        }
    }
}
