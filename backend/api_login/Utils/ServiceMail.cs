using api_login.Repository.Model;
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
        public static void Send(string nome, string email)
        {
            MailMessage emailMessage = new MailMessage();

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 60 * 60;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("xxx@gmail.com", "xxx"); // alterar aq

                emailMessage.From = new MailAddress("xxx@gmail.com", "Sistema Login Screen");
                emailMessage.Body = BodyEmail(nome);
                emailMessage.Subject = "Usuário Cadastrado";
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

        public static string BodyEmail(string nome)
        {
            return $"<!DOCTYPE html><body>Olá<b>{nome}</b>!<br>Seu e-mail foi cadastrado com sucesso!(;<br><br>Obrigado.</body></html>";
        }
    }
}
