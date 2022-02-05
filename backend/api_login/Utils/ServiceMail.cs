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
        public static void Send(string nome, string email, string tipo, string code = "")
        {
            MailMessage emailMessage = new MailMessage();

            string titulo = tipo == "cadastro" ? "Usuário Cadastrado" : "Recuperação de Senha";

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 60 * 60;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("xxx@gmail.com", "xxx"); // alterar aq

                emailMessage.From = new MailAddress("xxx@gmail.com", "Sistema Login Screen");
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
                return $"<!DOCTYPE html><body>Olá<b> {nome}</b>!<br><a href='localhost:4200/recovery?{code}'>Clique aqui</a> para redefinir sua senha.</body></html>";
            }
        }
    }
}
