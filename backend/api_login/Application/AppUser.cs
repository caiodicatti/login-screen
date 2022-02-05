using api_login.Application.Interface;
using api_login.Model;
using api_login.Model.Tables;
using api_login.Repository;
using api_login.Repository.Interface;
using api_login.Repository.Model;
using api_login.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace api_login.Application
{
    public class AppUser : IAppUser
    {
        //private readonly UserRepository repository;
        public readonly IUserRepository userRepository;
        public readonly IRecoverPasswordRepository recoverPasswordRepository;
        private readonly IConfiguration configuration;

        public AppUser(IUserRepository _userRepository, IRecoverPasswordRepository _recoverPasswordRepository, IConfiguration _configuration)
        {
            userRepository = _userRepository;
            recoverPasswordRepository = _recoverPasswordRepository;
            configuration = _configuration;
        }

        public Response Cadastrar(User user)
        {
            Functions f = new Functions();

            EntityVerify verifica = f.VerifyUser(user);

            if (verifica.Verifica)
            {
                Encrypt encript = new Encrypt(SHA512.Create());
                user.senha = encript.CriptografarSenha(user.senha);

                var retorno = f.HideParamsUser(userRepository.Cadastro(user));

                ServiceMail.Send(configuration, retorno.nome, retorno.email, "cadastro");

                Response response = new Response();
                response.success = true;
                response.statusCode = 200;
                response.result = retorno;
                response.message = "Usário cadastrado com sucesso";

                return response;

            }
            else
            {
                Response error = new Response();
                error.success = false;
                error.statusCode = 422;
                error.result = "";
                error.message = verifica.Mensagem;

                return error;
            }
        }

        public Response Login(Authentication authentication)
        {
            Functions f = new Functions();

            Encrypt encript = new Encrypt(SHA512.Create());

            User user = userRepository.GetUserByEmail(authentication.Email);

            bool verifica = false;

            if (user != null)
            {
                verifica = encript.VerificarSenha(authentication.Senha, user.senha);

                if (verifica)
                {
                    Response response = new Response();
                    response.success = true;
                    response.statusCode = 200;
                    response.result = f.HideParamsUser(user);
                    response.message = "Usário autenticado";

                    return response;
                }
                else
                {
                    Response error = new Response();
                    error.success = false;
                    error.statusCode = 422;
                    error.result = "";
                    error.message = "Senha ou e-mail incorreto";

                    return error;
                }
            }
            else
            {
                Response error = new Response();
                error.success = false;
                error.statusCode = 422;
                error.result = "";
                error.message = "Usuário não encontrado";

                return error;
            }
        }

        public Response RecuperarSenha(String email)
        {
            Functions f = new Functions();
            User user = userRepository.GetUserByEmail(email);

            String code = f.GeneratorPassword();

            if(user != null)
            {
                //gerar senha e salvar na tabela
                RecoverPassword recover = new RecoverPassword
                {
                    email = user.email,
                    codigo = code,
                    validado = "N",
                    dataCadastro = DateTime.Now
            };

                recoverPasswordRepository.InsertData(recover);

                ServiceMail.Send(configuration, user.nome, user.email, "recuperação senha", code);

                Response response = new Response();
                response.success = true;
                response.statusCode = 200;
                response.result = "{}";
                response.message = "Foi enviado um e-mail com link de redefinição de senha.";

                return response;
            }
            else
            {
                Response error = new Response();
                error.success = false;
                error.statusCode = 422;
                error.result = "";
                error.message = "Usuário não encontrado";

                return error;
            }
        }

        public Response AlteraSenha(RecoverPasswordLink recoverPasswordLink)
        {
            bool verificaLink = recoverPasswordRepository.UpdateValidade(recoverPasswordLink.Email);

            if (verificaLink)
            {
                Encrypt encript = new Encrypt(SHA512.Create());
                recoverPasswordLink.Senha = encript.CriptografarSenha(recoverPasswordLink.Senha);

                bool verificaTrocaSenha = userRepository.AlterPassword(recoverPasswordLink);

                if (verificaTrocaSenha)
                {
                    Response response = new Response();
                    response.success = true;
                    response.statusCode = 200;
                    response.result = "{}";
                    response.message = "Senha alterada com sucesso.";

                    return response;
                }
                else
                {
                    Response error = new Response();
                    error.success = false;
                    error.statusCode = 422;
                    error.result = "";
                    error.message = "Erro, não foi possivel realizar a troca de senha";

                    return error;
                }

            }
            else
            {
                Response error = new Response();
                error.success = false;
                error.statusCode = 422;
                error.result = "";
                error.message = "O link já foi utilizado, não foi possivel dar procedimento na troca de senha";

                return error;
            }
        }
    }
}
