using api_login.Model;
using api_login.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_login.Utils
{
    public class Functions
    {
        public dynamic HideParamsUser(User user)
        {

            dynamic ret = new
            {
                idUsuario = user.idUsuario,
                nome = user.nome,
                email = user.email,
            };

            return ret;
        }

        public EntityVerify VerifyUser(User user)
        {
            EntityVerify retorno = new EntityVerify();
            retorno.Verifica = true;
            DateTime dateValue;
            if (user.nome == "" || user.nome == null)
            {
                retorno.Verifica = false;
                retorno.Mensagem = "Nome não pode ser vazio";
            }
            else if (!IsValidEmail(user.email))
            {
                retorno.Verifica = false;
                retorno.Mensagem = "E-mail inválido";
            }
            else if (user.senha.Length < 8)
            {
                retorno.Verifica = false;
                retorno.Mensagem = "Senha inválida. Minimo de 8 caracteres";
            }            

            return retorno;
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
